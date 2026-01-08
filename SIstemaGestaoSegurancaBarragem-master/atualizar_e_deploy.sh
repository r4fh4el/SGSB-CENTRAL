#!/bin/bash
# Script para atualizar código e fazer deploy completo

echo "========================================"
echo "Atualizando e Fazendo Deploy"
echo "========================================"
echo ""

# 1. Atualizar código do GitHub
echo "[1/3] Atualizando código do GitHub..."
cd /var/www/sgsb
git pull origin main

if [ $? -ne 0 ]; then
    echo "ERRO: Falha ao atualizar código!"
    exit 1
fi

echo "✓ Código atualizado"

# 2. Executar script de deploy
echo ""
echo "[2/3] Executando deploy..."
cd SIstemaGestaoSegurancaBarragem-master

# Verificar se o script existe
if [ -f "deploy_webapi_e_web.sh" ]; then
    chmod +x deploy_webapi_e_web.sh
    ./deploy_webapi_e_web.sh
else
    echo "Script deploy_webapi_e_web.sh não encontrado!"
    echo "Executando comandos manualmente..."
    
    # Executar comandos de deploy diretamente
    export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
    export DOTNET_ROOT=$HOME/.dotnet
    export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
    
    # WebAPI
    cd WebAPI
    dotnet restore
    dotnet build --configuration Release
    dotnet publish --configuration Release --output /var/www/sgsb/publish
    
    # SGSB.Web
    cd ../SGSB.Web
    dotnet restore
    dotnet build --configuration Release
    dotnet publish --configuration Release --output /var/www/sgsb/publish-web
    
    # Reiniciar serviços
    systemctl restart sgsb-api.service
    systemctl restart sgsb-web.service
fi

# 3. Verificar status
echo ""
echo "[3/3] Verificando status dos serviços..."
systemctl status sgsb-api.service --no-pager -l | head -10
echo ""
systemctl status sgsb-web.service --no-pager -l | head -10

echo ""
echo "========================================"
echo "Processo concluído!"
echo "========================================"

