#!/bin/bash

echo "========================================"
echo "Resolvendo conflitos e executando correção"
echo "========================================"

cd /var/www/sgsb
cd SIstemaGestaoSegurancaBarragem-master

echo ""
echo "[1/3] Fazendo stash das mudanças locais..."
echo "----------------------------------------"
git stash

echo ""
echo "[2/3] Fazendo pull do repositório..."
echo "----------------------------------------"
git pull origin main

echo ""
echo "[3/3] Executando script de correção..."
echo "----------------------------------------"
chmod +x corrigir_erro_500_completo.sh
./corrigir_erro_500_completo.sh

