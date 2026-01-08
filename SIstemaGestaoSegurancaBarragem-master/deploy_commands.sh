#!/bin/bash
# Script de deploy para o servidor SGSB
# Execute este script no servidor após fazer upload

set -e

SERVER_IP="72.60.57.220"
GITHUB_REPO="https://github.com/r4fh4el/SGSB-CENTRAL.git"
PROJECT_PATH="/var/www/sgsb"
PORT="5000"

echo "========================================"
echo "Deploy do Sistema SGSB"
echo "========================================"
echo ""

# Atualizar sistema
echo "[1/8] Atualizando sistema..."
apt-get update -y

# Instalar dependencias necessarias
echo "[2/8] Instalando dependencias..."
apt-get install -y git curl wget libicu-dev libicu70 libssl-dev libkrb5-dev zlib1g libgssapi-krb5-2 libc6-dev

# Instalar .NET SDK 7.0 ou superior
echo "[3/8] Verificando .NET SDK..."
if ! command -v dotnet &> /dev/null; then
    echo "Instalando .NET SDK 7.0..."
    wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
    chmod +x dotnet-install.sh
    ./dotnet-install.sh --channel 7.0
    export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
    echo 'export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools' >> ~/.bashrc
    source ~/.bashrc
else
    echo ".NET SDK ja esta instalado"
    dotnet --version
fi

# Criar diretorio do projeto
echo "[4/8] Criando diretorio do projeto..."
mkdir -p $PROJECT_PATH
cd $PROJECT_PATH

# Clonar ou atualizar repositorio
echo "[5/8] Clonando/Atualizando repositorio..."
if [ -d ".git" ]; then
    echo "Atualizando repositorio existente..."
    git pull origin main
else
    echo "Clonando repositorio..."
    git clone $GITHUB_REPO .
fi

# Navegar para o diretorio do projeto
cd SIstemaGestaoSegurancaBarragem-master

# Restaurar dependencias
echo "[6/8] Restaurando dependencias do .NET..."
dotnet restore

# Compilar o projeto WebAPI
echo "[7/8] Compilando projeto WebAPI..."
cd WebAPI
dotnet build --configuration Release

# Publicar o projeto
echo "Publicando projeto..."
dotnet publish --configuration Release --output /var/www/sgsb/publish

# Criar arquivo de servico systemd
echo "[8/8] Criando servico systemd..."
cat > /etc/systemd/system/sgsb-api.service << EOF
[Unit]
Description=SGSB Web API
After=network.target

[Service]
Type=notify
ExecStart=/usr/bin/dotnet /var/www/sgsb/publish/WebAPI.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=sgsb-api
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://0.0.0.0:$PORT

[Install]
WantedBy=multi-user.target
EOF

# Recarregar systemd
systemctl daemon-reload

# Habilitar servico
systemctl enable sgsb-api.service

# Iniciar servico
echo "Iniciando servico..."
systemctl start sgsb-api.service

# Verificar status
echo "Verificando status do servico..."
sleep 3
systemctl status sgsb-api.service --no-pager

echo ""
echo "========================================"
echo "Deploy concluido!"
echo "========================================"
echo "API rodando em: http://$SERVER_IP:$PORT"
echo "Swagger disponivel em: http://$SERVER_IP:$PORT/swagger"
echo ""
echo "Para verificar logs:"
echo "  journalctl -u sgsb-api.service -f"
echo ""

