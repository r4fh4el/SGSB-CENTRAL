#!/bin/bash
# Script para atualizar API com Swagger habilitado

echo "========================================"
echo "Atualizando API com Swagger"
echo "========================================"
echo ""

# Configurar variáveis
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
export LC_ALL=C
export LANG=C
export LANGUAGE=C

PROJECT_PATH="/var/www/sgsb/SIstemaGestaoSegurancaBarragem-master"

# 1. Atualizar código
echo "[1/5] Atualizando código do GitHub..."
cd /var/www/sgsb
git pull origin main

# 2. Parar serviço
echo ""
echo "[2/5] Parando serviço da API..."
systemctl stop sgsb-api.service 2>/dev/null || echo "Serviço não estava rodando"

# 3. Rebuild
echo ""
echo "[3/5] Fazendo rebuild da API..."
cd $PROJECT_PATH/WebAPI
dotnet clean --configuration Release
dotnet restore
dotnet build --configuration Release
dotnet publish --configuration Release --output /var/www/sgsb/publish

# 4. Configurar runtimeconfig.json
echo ""
echo "[4/5] Configurando runtimeconfig.json..."
cat > /var/www/sgsb/publish/WebAPI.runtimeconfig.json << 'EOF'
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

# 5. Reiniciar serviço
echo ""
echo "[5/5] Reiniciando serviço..."
systemctl start sgsb-api.service

# Aguardar
sleep 5

# Verificar
echo ""
echo "========================================"
echo "Status da API:"
echo "========================================"
systemctl status sgsb-api.service --no-pager -l | head -15

echo ""
echo "Testando Swagger:"
curl -I http://localhost:80/swagger 2>/dev/null | head -5 || echo "Swagger não respondeu"

echo ""
echo "========================================"
echo "API atualizada!"
echo "========================================"
echo ""
echo "Acesse o Swagger em:"
echo "  http://72.60.57.220/swagger"
echo ""
echo "Verifique os logs se houver problemas:"
echo "  journalctl -u sgsb-api.service -n 50 --no-pager"
echo ""

