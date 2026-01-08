#!/bin/bash
# Script de diagnóstico completo do ambiente

echo "========================================"
echo "Diagnóstico Completo do Ambiente"
echo "========================================"
echo ""

# 1. Verificar variável de ambiente do serviço
echo "[1/6] Variável de ambiente configurada:"
systemctl show sgsb-web.service | grep ASPNETCORE_ENVIRONMENT

# 2. Verificar arquivos de configuração no publish
echo ""
echo "[2/6] Arquivos de configuração no publish:"
ls -la /var/www/sgsb/publish-web/appsettings*.json

# 3. Ver conteúdo do appsettings.json
echo ""
echo "[3/6] Conteúdo do appsettings.json:"
cat /var/www/sgsb/publish-web/appsettings.json

# 4. Ver logs completos
echo ""
echo "[4/6] Últimos 100 logs do serviço:"
journalctl -u sgsb-web.service -n 100 --no-pager | grep -i "environment\|development\|production\|error\|exception" | head -30

# 5. Verificar se o processo está rodando com as variáveis corretas
echo ""
echo "[5/6] Processo e variáveis de ambiente:"
PID=$(systemctl show sgsb-web.service -p MainPID --value)
if [ ! -z "$PID" ] && [ "$PID" != "0" ]; then
    echo "PID: $PID"
    cat /proc/$PID/environ | tr '\0' '\n' | grep -i "ASPNETCORE" || echo "Variáveis ASPNETCORE não encontradas no processo"
else
    echo "Serviço não está rodando"
fi

# 6. Testar execução manual com Production
echo ""
echo "[6/6] Testando execução manual com Production:"
cd /var/www/sgsb/publish-web
export ASPNETCORE_ENVIRONMENT=Production
export ASPNETCORE_URLS=http://0.0.0.0:8080
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true

echo "Variáveis configuradas:"
echo "  ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT"
echo ""
echo "Execute manualmente para ver o erro completo:"
echo "  /root/.dotnet/dotnet SGSB.Web.dll"
echo ""
echo "Ou pressione Ctrl+C após alguns segundos se quiser testar agora"
timeout 5 /root/.dotnet/dotnet SGSB.Web.dll 2>&1 | head -20 || echo "Teste interrompido"

echo ""
echo "========================================"
echo "Diagnóstico concluído!"
echo "========================================"

