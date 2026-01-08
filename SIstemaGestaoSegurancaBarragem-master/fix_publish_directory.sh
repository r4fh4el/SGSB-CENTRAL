#!/bin/bash
# Script para corrigir o problema do diretório publish

echo "========================================"
echo "Corrigindo diretório publish"
echo "========================================"
echo ""

# 1. Verificar se o diretório publish existe
echo "[1/4] Verificando diretório publish..."
if [ ! -d "/var/www/sgsb/publish" ]; then
    echo "Diretório não existe. Criando e publicando..."
    mkdir -p /var/www/sgsb/publish
else
    echo "Diretório existe"
fi

# 2. Configurar PATH
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet

# 3. Navegar e publicar
echo "[2/4] Navegando para o projeto..."
cd /var/www/sgsb/SIstemaGestaoSegurancaBarragem-master/WebAPI

echo "[3/4] Restaurando dependências..."
dotnet restore

echo "[4/4] Publicando projeto..."
dotnet publish --configuration Release --output /var/www/sgsb/publish

# Verificar se foi criado
if [ -f "/var/www/sgsb/publish/WebAPI.dll" ]; then
    echo "✓ Publicação concluída com sucesso!"
    ls -lh /var/www/sgsb/publish/ | head -10
else
    echo "✗ ERRO: Publicação falhou!"
    exit 1
fi

echo ""
echo "Agora atualize o serviço systemd sem WorkingDirectory:"
echo "  (Execute o próximo script ou os comandos manualmente)"

