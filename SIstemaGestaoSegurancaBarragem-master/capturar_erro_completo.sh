#!/bin/bash

echo "========================================"
echo "Capturar Erro Completo do SGSB.Web"
echo "========================================"

echo "[1/3] Ultimos 200 logs com erros e excecoes:"
echo "----------------------------------------"
journalctl -u sgsb-web.service -n 200 --no-pager | grep -E "Exception|Error|fail:" | tail -30
echo ""

echo "[2/3] Stack trace completo do ultimo erro:"
echo "----------------------------------------"
journalctl -u sgsb-web.service -n 200 --no-pager | grep -A 50 "Exception\|Error\|fail:" | tail -60
echo ""

echo "[3/3] Logs completos das ultimas 50 linhas:"
echo "----------------------------------------"
journalctl -u sgsb-web.service -n 50 --no-pager
echo ""

echo "========================================"
echo "Para ver logs em tempo real:"
echo "  journalctl -u sgsb-web.service -f"
echo "========================================"

