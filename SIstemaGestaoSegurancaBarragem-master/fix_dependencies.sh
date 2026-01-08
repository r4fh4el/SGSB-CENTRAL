#!/bin/bash
# Script para instalar dependências do .NET no servidor

echo "========================================"
echo "Instalando dependências do .NET"
echo "========================================"
echo ""

# Atualizar lista de pacotes
echo "[1/3] Atualizando lista de pacotes..."
apt-get update -y

# Instalar dependências do .NET
echo "[2/3] Instalando dependências do .NET..."
apt-get install -y \
    libicu-dev \
    libicu70 \
    libssl-dev \
    libkrb5-dev \
    zlib1g \
    libgssapi-krb5-2 \
    libc6-dev \
    libgdiplus \
    libx11-dev \
    libfontconfig1 \
    libfreetype6

# Verificar instalação
echo "[3/3] Verificando instalação..."
if command -v dotnet &> /dev/null; then
    echo "Testando .NET..."
    export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
    dotnet --version
    echo ""
    echo "Dependências instaladas com sucesso!"
else
    echo "AVISO: .NET não encontrado no PATH"
    echo "Execute: export PATH=\$PATH:\$HOME/.dotnet:\$HOME/.dotnet/tools"
fi

echo ""
echo "========================================"
echo "Agora você pode executar:"
echo "  cd /var/www/sgsb/SIstemaGestaoSegurancaBarragem-master/WebAPI"
echo "  dotnet restore"
echo "  dotnet build --configuration Release"
echo "  dotnet publish --configuration Release --output /var/www/sgsb/publish"
echo "  systemctl restart sgsb-api.service"
echo "========================================"

