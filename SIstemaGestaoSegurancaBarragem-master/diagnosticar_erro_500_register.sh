#!/bin/bash

echo "========================================"
echo "Diagnóstico do Erro 500 no Register"
echo "========================================"

echo ""
echo "[1/5] Verificando status dos serviços:"
echo "----------------------------------------"
systemctl status sgsb-api.service --no-pager -l | head -15
echo ""
systemctl status sgsb-web.service --no-pager -l | head -15

echo ""
echo "[2/5] Últimos 50 logs do sgsb-web.service (erros):"
echo "----------------------------------------"
journalctl -u sgsb-web.service -n 50 --no-pager | grep -i -E "error|exception|fail|500" | tail -30

echo ""
echo "[3/5] Últimos 30 logs completos do sgsb-web.service:"
echo "----------------------------------------"
journalctl -u sgsb-web.service -n 30 --no-pager | tail -30

echo ""
echo "[4/5] Verificando conexão com banco de dados:"
echo "----------------------------------------"
# Testar conexão SQL Server
timeout 5 bash -c 'cat < /dev/null > /dev/tcp/108.181.193.92/15000' 2>/dev/null && echo "✓ Porta 15000 está acessível" || echo "✗ Porta 15000 NÃO está acessível"

echo ""
echo "[5/5] Verificando variáveis de ambiente do processo:"
echo "----------------------------------------"
PID=$(systemctl show sgsb-web.service -p MainPID --value)
if [ ! -z "$PID" ] && [ "$PID" != "0" ]; then
    echo "PID do processo: $PID"
    cat /proc/$PID/environ 2>/dev/null | tr '\0' '\n' | grep -E "LC_|LANG|DOTNET|ASPNETCORE" | sort
else
    echo "Serviço não está rodando"
fi

echo ""
echo "========================================"
echo "Para ver logs em tempo real:"
echo "journalctl -u sgsb-web.service -f"
echo "========================================"

