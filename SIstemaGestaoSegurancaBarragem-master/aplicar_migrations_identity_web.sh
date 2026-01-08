#!/bin/bash

echo "========================================"
echo "Aplicar Migrations do Identity (SGSB.Web)"
echo "========================================"

cd /var/www/sgsb/SIstemaGestaoSegurancaBarragem-master/SGSB.Web

# Configurar variáveis
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
export LC_ALL=C
export LANG=C
export LANGUAGE=C

echo ""
echo "[1/4] Verificando migrations pendentes..."
echo "----------------------------------------"
dotnet ef migrations list --context ApplicationDbContext 2>&1 | head -20

echo ""
echo "[2/4] Aplicando migrations do Identity..."
echo "----------------------------------------"
dotnet ef database update --context ApplicationDbContext 2>&1

if [ $? -eq 0 ]; then
    echo ""
    echo "[3/4] ✓ Migrations aplicadas com sucesso!"
else
    echo ""
    echo "[3/4] ✗ Erro ao aplicar migrations. Verifique os logs acima."
    echo ""
    echo "[4/4] Tentando criar migrations se não existirem..."
    dotnet ef migrations add CreateIdentitySchema --context ApplicationDbContext 2>&1
    dotnet ef database update --context ApplicationDbContext 2>&1
fi

echo ""
echo "[4/4] Verificando tabelas do Identity no banco..."
echo "----------------------------------------"
echo "Execute no SQL Server para verificar:"
echo "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE 'AspNet%'"

echo ""
echo "========================================"
echo "Concluído!"
echo "========================================"

