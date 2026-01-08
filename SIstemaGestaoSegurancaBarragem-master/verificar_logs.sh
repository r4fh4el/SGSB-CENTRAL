#!/bin/bash
# Script para verificar logs detalhados do serviço

echo "========================================"
echo "Verificando logs do serviço SGSB"
echo "========================================"
echo ""

# Ver logs recentes
echo "Últimos 50 logs do serviço:"
journalctl -u sgsb-api.service -n 50 --no-pager

echo ""
echo "========================================"
echo "Testando execução manual:"
echo "========================================"

# Testar execução manual
cd /var/www/sgsb/publish
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
export ASPNETCORE_ENVIRONMENT=Production
export ASPNETCORE_URLS=http://0.0.0.0:5000

echo "Executando manualmente para ver erro completo..."
/root/.dotnet/dotnet WebAPI.dll

