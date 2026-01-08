#!/bin/bash
# Script para verificar status e continuar o deploy

echo "========================================"
echo "Verificando Status e Continuando Deploy"
echo "========================================"
echo ""

# 1. Verificar se o serviço está rodando
echo "[1/4] Verificando status do serviço..."
systemctl status sgsb-api.service --no-pager -l | head -20

# 2. Verificar se está escutando na porta 80
echo ""
echo "[2/4] Verificando porta 80:"
netstat -tlnp | grep :80 || lsof -i:80 || echo "Nenhum processo na porta 80"

# 3. Ver logs recentes
echo ""
echo "[3/4] Últimos logs do serviço:"
journalctl -u sgsb-api.service -n 30 --no-pager

# 4. Tentar iniciar/reiniciar se necessário
echo ""
echo "[4/4] Tentando iniciar serviço..."
systemctl daemon-reload
systemctl restart sgsb-api.service

# Aguardar
sleep 8

echo ""
echo "Status após reiniciar:"
systemctl status sgsb-api.service --no-pager -l | head -30

echo ""
echo "Verificando porta 80 novamente:"
netstat -tlnp | grep :80 || lsof -i:80

echo ""
echo "Testando conexão HTTP:"
curl -I http://localhost:80 2>&1 | head -5 || echo "Não conseguiu conectar"

echo ""
echo "========================================"
echo "Se o serviço não estiver rodando, verifique os logs:"
echo "  journalctl -u sgsb-api.service -f"
echo "========================================"

