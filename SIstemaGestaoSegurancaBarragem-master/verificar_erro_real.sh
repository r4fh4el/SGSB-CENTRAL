#!/bin/bash
# Script para verificar o erro real que está acontecendo

echo "========================================"
echo "Verificando Erro Real"
echo "========================================"
echo ""

# 1. Ver logs completos recentes
echo "[1/3] Últimos 100 logs do SGSB.Web:"
journalctl -u sgsb-web.service -n 100 --no-pager

# 2. Ver apenas erros e exceções
echo ""
echo "[2/3] Erros e Exceções:"
journalctl -u sgsb-web.service -n 200 --no-pager | grep -A 10 -i "error\|exception\|fail" | head -50

# 3. Verificar status do serviço
echo ""
echo "[3/3] Status do serviço:"
systemctl status sgsb-web.service --no-pager -l | head -20

echo ""
echo "========================================"
echo "Para ver logs em tempo real:"
echo "  journalctl -u sgsb-web.service -f"
echo "========================================"

