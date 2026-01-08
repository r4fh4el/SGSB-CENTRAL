#!/bin/bash
# Script para parar todos os serviços e processos do SGSB

echo "========================================"
echo "Parando todos os serviços SGSB"
echo "========================================"
echo ""

# 1. Parar serviço systemd
echo "[1/4] Parando serviço systemd..."
systemctl stop sgsb-api.service 2>/dev/null || echo "Serviço não estava rodando"
systemctl disable sgsb-api.service 2>/dev/null || echo "Serviço já estava desabilitado"

# 2. Matar processos dotnet relacionados
echo "[2/4] Matando processos dotnet..."
pkill -f "dotnet.*WebAPI.dll" 2>/dev/null || echo "Nenhum processo WebAPI encontrado"
pkill -f "dotnet.*SGSB" 2>/dev/null || echo "Nenhum processo SGSB encontrado"

# 3. Matar processos na porta 5000
echo "[3/4] Liberando porta 5000..."
lsof -ti:5000 | xargs kill -9 2>/dev/null || echo "Porta 5000 já está livre"
fuser -k 5000/tcp 2>/dev/null || echo "Nenhum processo na porta 5000"

# 4. Verificar processos restantes
echo "[4/4] Verificando processos restantes..."
sleep 2
ps aux | grep -E "dotnet|WebAPI|SGSB" | grep -v grep || echo "Nenhum processo encontrado"

echo ""
echo "========================================"
echo "Todos os serviços foram parados!"
echo "========================================"
echo ""
echo "Para verificar processos:"
echo "  ps aux | grep dotnet"
echo ""
echo "Para verificar porta 5000:"
echo "  netstat -tlnp | grep 5000"
echo "  ou"
echo "  lsof -i:5000"
echo ""

