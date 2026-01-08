#!/bin/bash

echo "========================================"
echo "Rebuild e Restart SGSB.Web"
echo "========================================"

# Configurar variaveis de ambiente ANTES de qualquer comando dotnet
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
export LC_ALL=C
export LANG=C
export LANGUAGE=C
export LC_CTYPE=C
export LC_NUMERIC=C
export LC_TIME=C
export LC_COLLATE=C
export LC_MONETARY=C
export LC_MESSAGES=C

# Diretorio do projeto
PROJECT_DIR="/var/www/sgsb/SIstemaGestaoSegurancaBarragem-master/SGSB.Web"
cd "$PROJECT_DIR"

# 1. Clean
echo "[1/4] Limpando build anterior..."
echo "----------------------------------------"
dotnet clean --configuration Release
echo ""

# 2. Restore
echo "[2/4] Restaurando pacotes..."
echo "----------------------------------------"
dotnet restore
echo ""

# 3. Build
echo "[3/4] Compilando projeto..."
echo "----------------------------------------"
dotnet build --configuration Release
if [ $? -ne 0 ]; then
    echo "ERRO: Falha na compilacao"
    exit 1
fi
echo ""

# 4. Publish
echo "[4/4] Publicando aplicacao..."
echo "----------------------------------------"
dotnet publish --configuration Release --output /var/www/sgsb/publish-web
if [ $? -ne 0 ]; then
    echo "ERRO: Falha no publish"
    exit 1
fi
echo ""

# 5. Atualizar runtimeconfig.json
echo "[5/5] Atualizando runtimeconfig.json..."
echo "----------------------------------------"
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
echo "runtimeconfig.json atualizado"
echo ""

# 6. Restart service
echo "[6/6] Reiniciando servico..."
echo "----------------------------------------"
systemctl restart sgsb-web.service
sleep 3

# 7. Verificar status
echo "[7/7] Verificando status do servico..."
echo "----------------------------------------"
systemctl status sgsb-web.service --no-pager -l | head -20
echo ""

echo "========================================"
echo "Processo concluido!"
echo "========================================"
echo ""
echo "Para ver logs em tempo real:"
echo "  journalctl -u sgsb-web.service -f"
echo ""

