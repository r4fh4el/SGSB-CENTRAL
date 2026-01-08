#!/bin/bash

echo "========================================"
echo "Aplicar Migrations do Identity (Manual)"
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
echo "[1/3] Verificando se dotnet-ef está instalado..."
echo "----------------------------------------"
dotnet ef --version 2>&1

if [ $? -ne 0 ]; then
    echo "dotnet-ef não encontrado. Execute primeiro:"
    echo "cd /var/www/sgsb/SIstemaGestaoSegurancaBarragem-master"
    echo "chmod +x instalar_dotnet_ef_correto.sh"
    echo "./instalar_dotnet_ef_correto.sh"
    exit 1
fi

echo ""
echo "[2/3] Listando migrations disponíveis..."
echo "----------------------------------------"
dotnet ef migrations list --context ApplicationDbContext 2>&1

echo ""
echo "[3/3] Aplicando migrations ao banco de dados..."
echo "----------------------------------------"
dotnet ef database update --context ApplicationDbContext 2>&1

if [ $? -eq 0 ]; then
    echo ""
    echo "✓ Migrations aplicadas com sucesso!"
else
    echo ""
    echo "✗ Erro ao aplicar migrations. Verifique a conexão com o banco de dados."
fi

echo ""
echo "========================================"
echo "Concluído!"
echo "========================================"

