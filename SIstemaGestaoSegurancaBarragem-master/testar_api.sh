#!/bin/bash
# Script para testar a API

echo "========================================"
echo "Testando API SGSB"
echo "========================================"
echo ""

# 1. Testar raiz
echo "[1/4] Testando raiz (/)..."
curl -I http://localhost:80 2>&1 | head -3

# 2. Testar Swagger
echo ""
echo "[2/4] Testando Swagger (/swagger)..."
curl -I http://localhost:80/swagger 2>&1 | head -3

# 3. Testar endpoint de health/status se existir
echo ""
echo "[3/4] Testando outros endpoints comuns..."
curl -I http://localhost:80/api 2>&1 | head -3
curl -I http://localhost:80/health 2>&1 | head -3

# 4. Verificar processos
echo ""
echo "[4/4] Verificando processo dotnet..."
ps aux | grep dotnet | grep -v grep

echo ""
echo "========================================"
echo "Status do serviço:"
echo "========================================"
systemctl status sgsb-api.service --no-pager -l | head -15

echo ""
echo "========================================"
echo "API está rodando!"
echo "========================================"
echo ""
echo "Acesse no navegador:"
echo "  http://72.60.57.220/swagger"
echo "  http://72.60.57.220/api"
echo ""
echo "Para ver logs em tempo real:"
echo "  journalctl -u sgsb-api.service -f"
echo ""

