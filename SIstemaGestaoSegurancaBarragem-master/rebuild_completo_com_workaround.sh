#!/bin/bash
# Script para rebuild completo com workaround de cultura

echo "========================================"
echo "Rebuild Completo com Workaround"
echo "========================================"
echo ""

# Configurar variáveis
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
export LC_ALL=C
export LANG=C
export LANGUAGE=C

PROJECT_PATH="/var/www/sgsb/SIstemaGestaoSegurancaBarragem-master"

# 1. Limpar builds anteriores
echo "[1/5] Limpando builds anteriores..."
cd $PROJECT_PATH/SGSB.Web
dotnet clean --configuration Release

# 2. Restore
echo ""
echo "[2/5] Restaurando pacotes..."
dotnet restore

# 3. Build
echo ""
echo "[3/5] Compilando..."
dotnet build --configuration Release

# 4. Publish
echo ""
echo "[4/5] Publicando..."
dotnet publish --configuration Release --output /var/www/sgsb/publish-web

# 5. Configurar runtimeconfig.json
echo ""
echo "[5/5] Configurando runtimeconfig.json..."
cat > /var/www/sgsb/publish-web/SGSB.Web.runtimeconfig.json << 'EOF'
{
  "runtimeOptions": {
    "tfm": "net7.0",
    "framework": {
      "name": "Microsoft.AspNetCore.App",
      "version": "7.0.0"
    },
    "configProperties": {
      "System.Globalization.Invariant": true
    }
  }
}
EOF

# 6. Reiniciar serviço
echo ""
echo "Reiniciando serviço..."
systemctl restart sgsb-web.service

# Aguardar
sleep 5

# Verificar
echo ""
echo "Status do serviço:"
systemctl status sgsb-web.service --no-pager -l | head -15

echo ""
echo "Verificando locale do processo:"
PID=$(systemctl show sgsb-web.service -p MainPID --value)
if [ ! -z "$PID" ] && [ "$PID" != "0" ]; then
    echo "PID: $PID"
    cat /proc/$PID/environ | tr '\0' '\n' | grep -i "lang\|lc" | head -10
fi

echo ""
echo "========================================"
echo "Rebuild concluído!"
echo "========================================"
echo ""
echo "Teste o login novamente."
echo "Se o erro persistir, verifique os logs:"
echo "  journalctl -u sgsb-web.service -n 50 --no-pager | grep -i culture"
echo ""

