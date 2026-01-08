#!/bin/bash

echo "========================================"
echo "Instalar ICU e Verificar .NET 7.0 SDK"
echo "========================================"

# 1. Instalar ICU
echo "[1/4] Instalando bibliotecas ICU..."
echo "----------------------------------------"
apt-get update
apt-get install -y libicu-dev libicu72
if [ $? -eq 0 ]; then
    echo "ICU instalado com sucesso!"
else
    echo "ERRO ao instalar ICU"
    exit 1
fi
echo ""

# 2. Verificar se .NET SDK 7.0 esta instalado
echo "[2/4] Verificando .NET SDK 7.0..."
echo "----------------------------------------"
# Configurar variaveis temporariamente para verificar
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
dotnet --list-sdks
echo ""

# Verificar se tem SDK 7.0.x
SDK_VERSION=$(dotnet --list-sdks | grep "7.0")
if [ -z "$SDK_VERSION" ]; then
    echo "AVISO: .NET SDK 7.0 nao encontrado!"
    echo "Instalando .NET SDK 7.0..."
    # Instalar .NET SDK 7.0
    wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
    chmod +x dotnet-install.sh
    ./dotnet-install.sh --version 7.0.410 --channel 7.0
    export PATH=$PATH:$HOME/.dotnet
    export DOTNET_ROOT=$HOME/.dotnet
else
    echo "SDK encontrado: $SDK_VERSION"
fi
echo ""

# 3. Verificar versao do dotnet-ef
echo "[3/4] Verificando dotnet-ef..."
echo "----------------------------------------"
dotnet ef --version
echo ""

# 4. Verificar versoes dos pacotes
echo "[4/4] Verificando versoes dos pacotes no projeto:"
echo "----------------------------------------"
cd /var/www/sgsb/SIstemaGestaoSegurancaBarragem-master/SGSB.Web
if [ -f "SGSB.Web.csproj" ]; then
    echo "TargetFramework:"
    grep "TargetFramework" SGSB.Web.csproj | head -1
    echo ""
    echo "Microsoft.EntityFrameworkCore.SqlServer:"
    grep "Microsoft.EntityFrameworkCore.SqlServer" SGSB.Web.csproj | grep -oP 'Version="\K[^"]+'
    echo ""
    echo "System.Data.SqlClient:"
    grep "System.Data.SqlClient" SGSB.Web.csproj | grep -oP 'Version="\K[^"]+' || echo "Nao encontrado"
fi
echo ""

echo "========================================"
echo "Processo concluido!"
echo "========================================"
echo ""
echo "Agora o .NET deve funcionar sem modo invariante."
echo "Teste: dotnet --version"
echo ""

