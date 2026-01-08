#!/bin/bash

echo "========================================"
echo "Verificar Configuracao de Cultura Invariante"
echo "========================================"

# 1. Verificar variaveis do servico systemd
echo "[1/4] Variaveis de ambiente do servico systemd:"
echo "----------------------------------------"
systemctl show sgsb-web.service | grep -E 'LC_|LANG|DOTNET' | sort
echo ""

# 2. Verificar variaveis do processo em execucao
echo "[2/4] Variaveis de ambiente do processo dotnet:"
echo "----------------------------------------"
PID=$(systemctl show sgsb-web.service -p MainPID --value)
if [ -n "$PID" ] && [ "$PID" != "0" ]; then
    echo "PID do processo: $PID"
    echo ""
    echo "Variaveis de cultura:"
    cat /proc/$PID/environ 2>/dev/null | tr '\0' '\n' | grep -E 'LC_|LANG|DOTNET' | sort
else
    echo "Processo nao encontrado ou nao esta rodando"
fi
echo ""

# 3. Verificar runtimeconfig.json
echo "[3/4] Verificando runtimeconfig.json:"
echo "----------------------------------------"
if [ -f "/var/www/sgsb/publish-web/SGSB.Web.runtimeconfig.json" ]; then
    cat /var/www/sgsb/publish-web/SGSB.Web.runtimeconfig.json | python3 -m json.tool 2>/dev/null || cat /var/www/sgsb/publish-web/SGSB.Web.runtimeconfig.json
else
    echo "ERRO: runtimeconfig.json nao encontrado!"
fi
echo ""

# 4. Verificar logs recentes para erros de cultura
echo "[4/4] Verificando logs recentes para erros de cultura:"
echo "----------------------------------------"
journalctl -u sgsb-web.service -n 50 --no-pager | grep -i -E "culture|en-us|globalization|invariant" | tail -10
if [ $? -ne 0 ]; then
    echo "Nenhum erro de cultura encontrado nos logs recentes (BOM SINAL!)"
fi
echo ""

echo "========================================"
echo "Verificacao concluida!"
echo "========================================"
echo ""
echo "Se todas as variaveis LC_* e LANG estao como 'C' e DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true,"
echo "e nao ha erros de cultura nos logs, entao esta CORRETO!"
echo ""

