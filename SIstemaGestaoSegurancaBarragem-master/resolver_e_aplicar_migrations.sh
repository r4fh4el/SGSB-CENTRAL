#!/bin/bash

echo "========================================"
echo "Resolver Conflitos e Aplicar Migrations"
echo "========================================"

# Diretorio base
BASE_DIR="/var/www/sgsb/SIstemaGestaoSegurancaBarragem-master"
cd "$BASE_DIR"

# 1. Fazer stash das mudancas locais
echo "[1/4] Fazendo stash das mudancas locais..."
echo "----------------------------------------"
git stash
if [ $? -ne 0 ]; then
    echo "AVISO: Nao havia mudancas locais ou outro problema"
fi
echo ""

# 2. Fazer pull
echo "[2/4] Fazendo pull do repositorio..."
echo "----------------------------------------"
git pull origin main
if [ $? -ne 0 ]; then
    echo "ERRO: Falha ao fazer pull"
    exit 1
fi
echo ""

# 3. Dar permissao ao script
echo "[3/4] Configurando permissoes..."
echo "----------------------------------------"
chmod +x aplicar_migrations_identity_final.sh
chmod +x deploy_completo_com_migrations.sh
echo ""

# 4. Executar script de migrations
echo "[4/4] Executando script de migrations..."
echo "----------------------------------------"
./aplicar_migrations_identity_final.sh

echo ""
echo "========================================"
echo "Processo concluido!"
echo "========================================"

