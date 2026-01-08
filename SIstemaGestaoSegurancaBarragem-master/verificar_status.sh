#!/bin/bash
# Script para verificar status do serviço SGSB

echo "========================================"
echo "Verificando Status do Serviço SGSB"
echo "========================================"
echo ""

# 1. Status do serviço
echo "[1/3] Status do serviço systemd:"
systemctl status sgsb-api.service --no-pager -l

echo ""
echo "[2/3] Verificando se está escutando na porta 5000:"
netstat -tlnp | grep 5000 || lsof -i:5000

echo ""
echo "[3/3] Testando conexão HTTP:"
curl -s -o /dev/null -w "Status HTTP: %{http_code}\n" http://localhost:5000 || echo "Erro ao conectar"

echo ""
echo "========================================"
echo "Serviço está rodando!"
echo "========================================"
echo ""
echo "API disponível em:"
echo "  http://72.60.57.220:5000"
echo "  http://72.60.57.220:5000/swagger"
echo ""
echo "Para ver logs em tempo real:"
echo "  journalctl -u sgsb-api.service -f"
echo ""
echo "Para reiniciar:"
echo "  systemctl restart sgsb-api.service"
echo ""

