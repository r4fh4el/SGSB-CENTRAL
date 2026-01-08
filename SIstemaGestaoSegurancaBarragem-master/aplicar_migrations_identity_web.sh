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
echo "[1/5] Instalando dotnet-ef tool (versão 7.x para .NET 7.0)..."
echo "----------------------------------------"
dotnet tool list -g | grep dotnet-ef > /dev/null
if [ $? -ne 0 ]; then
    echo "Instalando dotnet-ef versão 7.0.20 (compatível com .NET 7.0)..."
    dotnet tool install --global dotnet-ef --version 7.0.20 2>&1
    if [ $? -ne 0 ]; then
        echo "Tentando versão 7.0.0..."
        dotnet tool install --global dotnet-ef --version 7.0.0 2>&1
    fi
else
    echo "✓ dotnet-ef já está instalado"
    # Verificar versão e reinstalar se necessário
    EF_VERSION=$(dotnet tool list -g | grep dotnet-ef | awk '{print $2}')
    if [[ "$EF_VERSION" == "10."* ]]; then
        echo "Removendo versão incompatível e instalando 7.0.20..."
        dotnet tool uninstall --global dotnet-ef 2>&1
        dotnet tool install --global dotnet-ef --version 7.0.20 2>&1
    fi
fi

echo ""
echo "[2/5] Verificando migrations pendentes..."
echo "----------------------------------------"
dotnet ef migrations list --context ApplicationDbContext 2>&1 | head -20

echo ""
echo "[3/5] Aplicando migrations do Identity..."
echo "----------------------------------------"
dotnet ef database update --context ApplicationDbContext 2>&1

if [ $? -eq 0 ]; then
    echo ""
    echo "[4/5] ✓ Migrations aplicadas com sucesso!"
else
    echo ""
    echo "[4/5] ✗ Erro ao aplicar migrations. Verifique os logs acima."
    echo ""
    echo "Tentando criar migrations se não existirem..."
    dotnet ef migrations add CreateIdentitySchema --context ApplicationDbContext 2>&1
    if [ $? -eq 0 ]; then
        dotnet ef database update --context ApplicationDbContext 2>&1
    fi
fi

echo ""
echo "[5/5] Verificando tabelas do Identity no banco..."
echo "----------------------------------------"
echo "Execute no SQL Server para verificar:"
echo "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE 'AspNet%'"

echo ""
echo "========================================"
echo "Concluído!"
echo "========================================"

