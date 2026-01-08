#!/bin/bash
# Script COMPLETO de deploy: Build + Publicação + Configuração + Iniciar
# Sistema de Gestão de Segurança de Barragem (SGSB)

set -e

echo "========================================"
echo "Deploy Completo SGSB - Build e Execução"
echo "========================================"
echo ""

# Variáveis
PROJECT_PATH="/var/www/sgsb"
GITHUB_REPO="https://github.com/r4fh4el/SGSB-CENTRAL.git"
PORT="80"

# 1. Configurar .NET
echo "[1/7] Configurando .NET..."
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true

if ! command -v dotnet &> /dev/null; then
    echo "Instalando .NET SDK 7.0..."
    wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
    chmod +x dotnet-install.sh
    ./dotnet-install.sh --channel 7.0
    export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
    echo 'export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools' >> ~/.bashrc
    echo 'export DOTNET_ROOT=$HOME/.dotnet' >> ~/.bashrc
    echo 'export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true' >> ~/.bashrc
fi

echo "Versão do .NET:"
dotnet --version

# 2. Clonar/Atualizar repositório
echo ""
echo "[2/7] Atualizando código fonte..."
mkdir -p $PROJECT_PATH
cd $PROJECT_PATH

if [ -d ".git" ]; then
    echo "Atualizando repositório existente..."
    git pull origin main
else
    echo "Clonando repositório..."
    git clone $GITHUB_REPO .
fi

# 3. Navegar para o projeto WebAPI
echo ""
echo "[3/7] Navegando para o projeto WebAPI..."
cd SIstemaGestaoSegurancaBarragem-master/WebAPI

# Verificar se o projeto existe
if [ ! -f "WebAPI.csproj" ]; then
    echo "ERRO: WebAPI.csproj não encontrado!"
    exit 1
fi

# 4. Restaurar dependências
echo ""
echo "[4/7] Restaurando dependências do NuGet..."
dotnet restore

# 5. Build do projeto
echo ""
echo "[5/7] Compilando projeto (Build)..."
dotnet build --configuration Release

if [ $? -ne 0 ]; then
    echo "ERRO: Build falhou!"
    exit 1
fi

echo "✓ Build concluído com sucesso!"

# 6. Publicar projeto
echo ""
echo "[6/7] Publicando projeto..."
mkdir -p $PROJECT_PATH/publish
dotnet publish --configuration Release --output $PROJECT_PATH/publish

if [ ! -f "$PROJECT_PATH/publish/WebAPI.dll" ]; then
    echo "ERRO: Publicação falhou! WebAPI.dll não encontrado."
    exit 1
fi

echo "✓ Publicação concluída!"

# 7. Configurar runtimeconfig.json
echo ""
echo "Configurando runtimeconfig.json..."
cat > $PROJECT_PATH/publish/WebAPI.runtimeconfig.json << 'EOF'
{
  "runtimeOptions": {
    "tfm": "net7.0",
    "framework": {
      "name": "Microsoft.AspNetCore.App",
      "version": "7.0.0"
    },
    "configProperties": {
      "System.Globalization.Invariant": true
    }
  }
}
EOF

# 8. Verificar porta 80
echo ""
echo "Verificando porta $PORT..."
if lsof -i:$PORT > /dev/null 2>&1; then
    echo "⚠ AVISO: Porta $PORT está em uso!"
    echo "Processos usando porta $PORT:"
    lsof -i:$PORT
    echo ""
    echo "Parando processos na porta $PORT..."
    lsof -ti:$PORT | xargs kill -9 2>/dev/null || true
    sleep 2
fi

# 9. Configurar firewall
echo ""
echo "Configurando firewall..."
ufw allow $PORT/tcp 2>/dev/null || iptables -A INPUT -p tcp --dport $PORT -j ACCEPT 2>/dev/null || echo "Firewall não configurado"

# 10. Parar serviço antigo se existir
echo ""
echo "Parando serviço antigo..."
systemctl stop sgsb-api.service 2>/dev/null || true

# 11. Criar serviço systemd
echo ""
echo "[7/7] Configurando serviço systemd..."
cat > /etc/systemd/system/sgsb-api.service << EOF
[Unit]
Description=SGSB Web API
After=network.target

[Service]
Type=notify
ExecStart=/root/.dotnet/dotnet $PROJECT_PATH/publish/WebAPI.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
TimeoutStartSec=300
TimeoutStopSec=30
SyslogIdentifier=sgsb-api
User=root
WorkingDirectory=$PROJECT_PATH/publish
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://0.0.0.0:$PORT
Environment="PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin:/root/.dotnet:/root/.dotnet/tools"
Environment="DOTNET_ROOT=/root/.dotnet"
Environment="DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true"
Environment="LC_ALL=C"
Environment="LANG=C"
StandardOutput=journal
StandardError=journal

[Install]
WantedBy=multi-user.target
EOF

# 12. Habilitar e iniciar serviço
echo ""
echo "Iniciando serviço..."
systemctl daemon-reload
systemctl enable sgsb-api.service
systemctl restart sgsb-api.service

# 13. Aguardar e verificar
sleep 8
echo ""
echo "========================================"
echo "Status do serviço:"
echo "========================================"
systemctl status sgsb-api.service --no-pager -l

echo ""
echo "========================================"
echo "Verificando porta $PORT:"
echo "========================================"
netstat -tlnp | grep :$PORT || lsof -i:$PORT

echo ""
echo "========================================"
echo "Últimos logs:"
echo "========================================"
journalctl -u sgsb-api.service -n 20 --no-pager

echo ""
echo "========================================"
echo "Deploy concluído!"
echo "========================================"
echo ""
echo "✓ Build realizado"
echo "✓ Publicação realizada"
echo "✓ Serviço configurado e iniciado"
echo ""
echo "API disponível em:"
echo "  http://72.60.57.220"
echo "  http://72.60.57.220/swagger"
echo ""
echo "Comandos úteis:"
echo "  Ver logs: journalctl -u sgsb-api.service -f"
echo "  Reiniciar: systemctl restart sgsb-api.service"
echo "  Status: systemctl status sgsb-api.service"
echo ""

