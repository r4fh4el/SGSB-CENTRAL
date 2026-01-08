#!/bin/bash

echo "========================================"
echo "Instalando dotnet-ef versão correta (7.0.20)"
echo "========================================"

# Configurar variáveis
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet

echo ""
echo "[1/3] Removendo versões antigas do dotnet-ef..."
echo "----------------------------------------"
dotnet tool list -g | grep dotnet-ef
if [ $? -eq 0 ]; then
    echo "Removendo dotnet-ef existente..."
    dotnet tool uninstall --global dotnet-ef 2>&1
fi

echo ""
echo "[2/3] Instalando dotnet-ef versão 7.0.20..."
echo "----------------------------------------"
dotnet tool install --global dotnet-ef --version 7.0.20 2>&1

if [ $? -eq 0 ]; then
    echo ""
    echo "[3/3] ✓ dotnet-ef 7.0.20 instalado com sucesso!"
    dotnet tool list -g | grep dotnet-ef
else
    echo ""
    echo "[3/3] Tentando versão alternativa 7.0.0..."
    dotnet tool install --global dotnet-ef --version 7.0.0 2>&1
    if [ $? -eq 0 ]; then
        echo "✓ dotnet-ef 7.0.0 instalado com sucesso!"
        dotnet tool list -g | grep dotnet-ef
    else
        echo "✗ Erro ao instalar dotnet-ef"
        exit 1
    fi
fi

echo ""
echo "========================================"
echo "Concluído!"
echo "========================================"

