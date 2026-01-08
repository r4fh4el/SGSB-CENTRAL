#!/bin/bash
# Script completo para corrigir cultura, IPs e fazer redeploy

echo "========================================"
echo "Correção Completa: Cultura + IPs + Deploy"
echo "========================================"
echo ""

SERVER_IP="72.60.57.220"
PROJECT_PATH="/var/www/sgsb/SIstemaGestaoSegurancaBarragem-master"

# 1. Atualizar código
echo "[1/5] Atualizando código do GitHub..."
cd /var/www/sgsb
git pull origin main

# 2. Configurar variáveis
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true

# 3. Verificar se as correções já foram aplicadas (se não, aplicar)
echo ""
echo "[2/5] Verificando correções de IP..."

# Verificar se SGSB.Web/Infra/Constantes.cs já tem o IP correto
if grep -q "localhost:7042" "$PROJECT_PATH/SGSB.Web/Infra/Constantes.cs" 2>/dev/null; then
    echo "Corrigindo SGSB.Web/Infra/Constantes.cs..."
    sed -i "s|https://localhost:7042|http://$SERVER_IP|g" "$PROJECT_PATH/SGSB.Web/Infra/Constantes.cs"
    sed -i "s|http://localhost:7042|http://$SERVER_IP|g" "$PROJECT_PATH/SGSB.Web/Infra/Constantes.cs"
fi

# Verificar se Infraestrutura/Configuracoes/Constantes.cs já tem o IP correto
if grep -q "localhost\|api.sgsb.com.br" "$PROJECT_PATH/Infraestrutura/Configuracoes/Constantes.cs" 2>/dev/null; then
    echo "Corrigindo Infraestrutura/Configuracoes/Constantes.cs..."
    sed -i "s|https://api.sgsb.com.br|http://$SERVER_IP|g" "$PROJECT_PATH/Infraestrutura/Configuracoes/Constantes.cs"
    sed -i "s|https://localhost|http://$SERVER_IP|g" "$PROJECT_PATH/Infraestrutura/Configuracoes/Constantes.cs"
fi

# Verificar se WebAPI/Program.cs tem configuração de cultura (já deve estar comentada)
if grep -q "new CultureInfo\|RequestLocalizationOptions" "$PROJECT_PATH/WebAPI/Program.cs" 2>/dev/null && ! grep -q "//.*CultureInfo" "$PROJECT_PATH/WebAPI/Program.cs" 2>/dev/null; then
    echo "AVISO: WebAPI/Program.cs ainda tem configuração de cultura ativa"
    echo "Verifique manualmente se está comentada"
fi

# 4. Build e Publish WebAPI
echo ""
echo "[3/5] Build e Publish WebAPI..."
cd $PROJECT_PATH/WebAPI
dotnet restore
dotnet build --configuration Release
dotnet publish --configuration Release --output /var/www/sgsb/publish

# Configurar runtimeconfig.json
cat > /var/www/sgsb/publish/WebAPI.runtimeconfig.json << 'EOF'
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

# 5. Build e Publish SGSB.Web
echo ""
echo "[4/5] Build e Publish SGSB.Web..."
cd $PROJECT_PATH/SGSB.Web
dotnet restore
dotnet build --configuration Release
dotnet publish --configuration Release --output /var/www/sgsb/publish-web

# Configurar runtimeconfig.json
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

# 6. Reiniciar serviços
echo ""
echo "[5/5] Reiniciando serviços..."
systemctl restart sgsb-api.service
systemctl restart sgsb-web.service

# Aguardar
sleep 5

# Verificar status
echo ""
echo "========================================"
echo "Status dos serviços:"
echo "========================================"
systemctl status sgsb-api.service --no-pager -l | head -10
echo ""
systemctl status sgsb-web.service --no-pager -l | head -10

echo ""
echo "========================================"
echo "Correção e Deploy concluídos!"
echo "========================================"
echo ""
echo "Verifique se os erros de cultura foram resolvidos:"
echo "  journalctl -u sgsb-api.service -n 50 --no-pager | grep -i culture"
echo "  journalctl -u sgsb-web.service -n 50 --no-pager | grep -i culture"
echo ""

