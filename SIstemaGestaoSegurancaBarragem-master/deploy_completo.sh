#!/bin/bash
# Script COMPLETO de deploy - Execute este no servidor
# Resolve todos os problemas: ICU, diretório publish, serviço systemd

set -e

echo "========================================"
echo "Deploy Completo do Sistema SGSB"
echo "========================================"
echo ""

# 1. Instalar dependências
echo "[1/6] Instalando dependências do sistema..."
apt-get update -y
apt-get install -y \
    git curl wget \
    libicu-dev libicu70 libicu71 \
    libssl3 libssl-dev \
    libkrb5-3 libkrb5-dev \
    zlib1g libgssapi-krb5-2 \
    libc6 libc6-dev libgcc-s1 libstdc++6

# 2. Configurar .NET
echo "[2/6] Configurando .NET..."
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

if ! command -v dotnet &> /dev/null; then
    echo "Instalando .NET SDK 7.0..."
    wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
    chmod +x dotnet-install.sh
    ./dotnet-install.sh --channel 7.0
    export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
    echo 'export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools' >> ~/.bashrc
    echo 'export DOTNET_ROOT=$HOME/.dotnet' >> ~/.bashrc
fi

# 3. Clonar/Atualizar repositório
echo "[3/6] Clonando/Atualizando repositório..."
mkdir -p /var/www/sgsb
cd /var/www/sgsb

if [ -d ".git" ]; then
    echo "Atualizando repositório..."
    git pull origin main
else
    echo "Clonando repositório..."
    git clone https://github.com/r4fh4el/SGSB-CENTRAL.git .
fi

# 4. Navegar e restaurar
echo "[4/6] Restaurando dependências do projeto..."
cd SIstemaGestaoSegurancaBarragem-master/WebAPI
dotnet restore

# 5. Publicar
echo "[5/6] Publicando projeto..."
mkdir -p /var/www/sgsb/publish
dotnet publish --configuration Release --output /var/www/sgsb/publish

# Verificar se foi criado
if [ ! -f "/var/www/sgsb/publish/WebAPI.dll" ]; then
    echo "ERRO: Publicação falhou!"
    exit 1
fi

echo "✓ Publicação concluída!"

# 6. Criar serviço systemd
echo "[6/6] Configurando serviço systemd..."
cat > /etc/systemd/system/sgsb-api.service << 'EOF'
[Unit]
Description=SGSB Web API
After=network.target

[Service]
Type=notify
ExecStart=/root/.dotnet/dotnet /var/www/sgsb/publish/WebAPI.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=sgsb-api
User=root
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://0.0.0.0:5000
Environment="PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin:/root/.dotnet:/root/.dotnet/tools"
Environment="DOTNET_ROOT=/root/.dotnet"
Environment="DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false"
Environment="LC_ALL=en_US.UTF-8"
Environment="LANG=en_US.UTF-8"

[Install]
WantedBy=multi-user.target
EOF

# Recarregar e iniciar
systemctl daemon-reload
systemctl enable sgsb-api.service
systemctl restart sgsb-api.service

# Aguardar e verificar
sleep 5
echo ""
echo "========================================"
echo "Status do serviço:"
echo "========================================"
systemctl status sgsb-api.service --no-pager -l

echo ""
echo "========================================"
echo "Deploy concluído!"
echo "========================================"
echo "API disponível em: http://72.60.57.220:5000"
echo "Swagger disponível em: http://72.60.57.220:5000/swagger"
echo ""
echo "Comandos úteis:"
echo "  Ver logs: journalctl -u sgsb-api.service -f"
echo "  Reiniciar: systemctl restart sgsb-api.service"
echo "  Status: systemctl status sgsb-api.service"
echo ""

