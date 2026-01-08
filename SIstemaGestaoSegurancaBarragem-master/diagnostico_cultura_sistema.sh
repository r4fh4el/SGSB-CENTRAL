#!/bin/bash
# Script para diagnosticar de onde vem a cultura "en-us"

echo "========================================"
echo "Diagnóstico de Cultura do Sistema"
echo "========================================"
echo ""

# 1. Verificar variáveis de ambiente do sistema
echo "[1/6] Variáveis de ambiente relacionadas a cultura:"
env | grep -i "lang\|lc_all\|culture" || echo "Nenhuma variável encontrada"

# 2. Verificar locale do sistema
echo ""
echo "[2/6] Locale do sistema:"
locale
echo ""
locale -a | grep -i "en\|us" | head -10

# 3. Verificar variáveis do processo do serviço
echo ""
echo "[3/6] Variáveis de ambiente do processo sgsb-web:"
PID=$(systemctl show sgsb-web.service -p MainPID --value)
if [ ! -z "$PID" ] && [ "$PID" != "0" ]; then
    echo "PID: $PID"
    cat /proc/$PID/environ | tr '\0' '\n' | grep -i "lang\|lc\|culture" || echo "Nenhuma variável encontrada no processo"
else
    echo "Serviço não está rodando"
fi

# 4. Verificar configuração do .NET
echo ""
echo "[4/6] Verificando runtimeconfig.json:"
cat /var/www/sgsb/publish-web/SGSB.Web.runtimeconfig.json 2>/dev/null || echo "Arquivo não encontrado"

# 5. Verificar se há ICU instalado
echo ""
echo "[5/6] Verificando bibliotecas ICU:"
ldconfig -p | grep -i icu || echo "Nenhuma biblioteca ICU encontrada"

# 6. Testar execução do .NET com cultura
echo ""
echo "[6/6] Testando cultura no .NET:"
cd /var/www/sgsb/publish-web
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
export LC_ALL=C
export LANG=C
/root/.dotnet/dotnet --version
echo ""
echo "Tentando obter cultura do sistema:"
/root/.dotnet/dotnet --info | grep -i "culture\|locale" || echo "Informação não disponível"

echo ""
echo "========================================"
echo "Diagnóstico concluído!"
echo "========================================"

