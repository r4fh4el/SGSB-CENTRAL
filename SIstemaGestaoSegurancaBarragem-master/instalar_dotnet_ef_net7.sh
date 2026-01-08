#!/bin/bash

echo "========================================"
echo "Instalando dotnet-ef compativel com .NET 7.0"
echo "========================================"

# Configurar variaveis de ambiente para .NET
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet

# 1. Verificar versao do .NET instalada
echo "[1/5] Verificando versao do .NET instalada..."
echo "----------------------------------------"
dotnet --version
if [ $? -ne 0 ]; then
    echo "ERRO: .NET nao esta instalado ou nao esta no PATH"
    exit 1
fi
echo ""

# 2. Listar versoes do dotnet-ef instaladas
echo "[2/5] Verificando versoes do dotnet-ef instaladas..."
echo "----------------------------------------"
dotnet tool list --global | grep dotnet-ef || echo "Nenhuma versao do dotnet-ef encontrada"
echo ""

# 3. Desinstalar TODAS as versoes do dotnet-ef (para evitar conflitos)
echo "[3/5] Desinstalando versoes antigas do dotnet-ef..."
echo "----------------------------------------"
dotnet tool uninstall --global dotnet-ef 2>&1 | grep -v "No tool" || echo "Nenhuma versao para desinstalar"
echo ""

# 4. Instalar dotnet-ef versao 7.0.20 (compativel com .NET 7.0)
echo "[4/5] Instalando dotnet-ef versao 7.0.20..."
echo "----------------------------------------"
dotnet tool install --global dotnet-ef --version 7.0.20
if [ $? -ne 0 ]; then
    echo "ERRO: Falha ao instalar dotnet-ef 7.0.20"
    echo "Tentando instalar a versao mais recente da serie 7.0.x..."
    dotnet tool install --global dotnet-ef --version 7.0.*
    if [ $? -ne 0 ]; then
        echo "ERRO: Falha ao instalar dotnet-ef"
        exit 1
    fi
fi
echo ""

# 5. Verificar instalacao
echo "[5/5] Verificando instalacao do dotnet-ef..."
echo "----------------------------------------"
dotnet ef --version
if [ $? -ne 0 ]; then
    echo "ERRO: dotnet-ef nao esta funcionando corretamente"
    echo "Verifique se o PATH inclui $HOME/.dotnet/tools"
    exit 1
fi
echo ""

# 6. Verificar versao instalada
echo "========================================"
echo "Versao instalada:"
echo "----------------------------------------"
dotnet tool list --global | grep dotnet-ef
echo ""

echo "========================================"
echo "Instalacao concluida com sucesso!"
echo "========================================"
echo ""
echo "Para usar o dotnet-ef, certifique-se de que o PATH inclui:"
echo "  export PATH=\$PATH:\$HOME/.dotnet:\$HOME/.dotnet/tools"
echo ""

