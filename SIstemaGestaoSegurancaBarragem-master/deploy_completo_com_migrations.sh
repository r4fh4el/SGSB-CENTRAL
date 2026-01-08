#!/bin/bash

echo "========================================"
echo "Deploy Completo do Sistema SGSB"
echo "========================================"

# Configurar variaveis de ambiente
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
export LC_ALL=C
export LANG=C
export LANGUAGE=C

# Diretorio base
BASE_DIR="/var/www/sgsb/SIstemaGestaoSegurancaBarragem-master"
cd "$BASE_DIR"

# 1. Verificar dotnet-ef
echo "[1/7] Verificando dotnet-ef..."
echo "----------------------------------------"
dotnet ef --version
if [ $? -ne 0 ]; then
    echo "ERRO: dotnet-ef nao esta instalado ou nao esta no PATH"
    echo "Execute primeiro: ./instalar_dotnet_ef_net7.sh"
    exit 1
fi
echo ""

# 2. Aplicar migrations do Identity (SGSB.Web)
echo "[2/7] Aplicando migrations do Identity (SGSB.Web)..."
echo "----------------------------------------"
cd "$BASE_DIR/SGSB.Web"
dotnet ef database update --context ApplicationDbContext
if [ $? -ne 0 ]; then
    echo "AVISO: Erro ao aplicar migrations do Identity"
    echo "Verificando se as migrations existem..."
    dotnet ef migrations list
    if [ $? -ne 0 ]; then
        echo "Criando migrations iniciais do Identity..."
        dotnet ef migrations add CreateIdentitySchema --context ApplicationDbContext
        dotnet ef database update --context ApplicationDbContext
    fi
fi
echo ""

# 3. Aplicar migrations do WebAPI (se houver)
echo "[3/7] Verificando migrations do WebAPI..."
echo "----------------------------------------"
cd "$BASE_DIR/WebAPI"
if [ -d "Migrations" ] || [ -f "*.csproj" ]; then
    echo "Aplicando migrations do WebAPI..."
    dotnet ef database update 2>&1 | head -20
    echo ""
else
    echo "Nenhuma migration encontrada no WebAPI"
    echo ""
fi

# 4. Rebuild WebAPI
echo "[4/7] Rebuild do WebAPI..."
echo "----------------------------------------"
cd "$BASE_DIR/WebAPI"
dotnet clean --configuration Release
dotnet restore
dotnet build --configuration Release
if [ $? -ne 0 ]; then
    echo "ERRO: Falha no build do WebAPI"
    exit 1
fi
echo ""

# 5. Publish WebAPI
echo "[5/7] Publicando WebAPI..."
echo "----------------------------------------"
dotnet publish --configuration Release --output /var/www/sgsb/publish
if [ $? -ne 0 ]; then
    echo "ERRO: Falha no publish do WebAPI"
    exit 1
fi
echo ""

# 6. Rebuild SGSB.Web
echo "[6/7] Rebuild do SGSB.Web..."
echo "----------------------------------------"
cd "$BASE_DIR/SGSB.Web"
dotnet clean --configuration Release
dotnet restore
dotnet build --configuration Release
if [ $? -ne 0 ]; then
    echo "ERRO: Falha no build do SGSB.Web"
    exit 1
fi
echo ""

# 7. Publish SGSB.Web
echo "[7/7] Publicando SGSB.Web..."
echo "----------------------------------------"
dotnet publish --configuration Release --output /var/www/sgsb/publish-web
if [ $? -ne 0 ]; then
    echo "ERRO: Falha no publish do SGSB.Web"
    exit 1
fi
echo ""

# 8. Atualizar runtimeconfig.json (se necessario)
echo "[8/8] Atualizando runtimeconfig.json..."
echo "----------------------------------------"
if [ -f "/var/www/sgsb/publish/WebAPI.runtimeconfig.json" ]; then
    # Garantir que o runtimeconfig.json tem as configuracoes corretas
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
    echo "WebAPI.runtimeconfig.json atualizado"
fi

if [ -f "/var/www/sgsb/publish-web/SGSB.Web.runtimeconfig.json" ]; then
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
    echo "SGSB.Web.runtimeconfig.json atualizado"
fi
echo ""

# 9. Reiniciar servicos
echo "[9/9] Reiniciando servicos..."
echo "----------------------------------------"
systemctl restart sgsb-api.service
sleep 3
systemctl restart sgsb-web.service
sleep 3

# 10. Verificar status
echo "[10/10] Verificando status dos servicos..."
echo "----------------------------------------"
echo ""
echo "Status sgsb-api.service:"
systemctl status sgsb-api.service --no-pager -l | head -15
echo ""
echo "Status sgsb-web.service:"
systemctl status sgsb-web.service --no-pager -l | head -15
echo ""

# 11. Verificar portas
echo "[11/11] Verificando portas..."
echo "----------------------------------------"
netstat -tlnp | grep -E ":80|:8080" || echo "Portas nao encontradas"
echo ""

echo "========================================"
echo "Deploy concluido!"
echo "========================================"
echo ""
echo "Aplicacoes disponiveis em:"
echo "  - WebAPI: http://72.60.57.220:80"
echo "  - SGSB.Web: http://72.60.57.220:8080"
echo "  - Swagger: http://72.60.57.220:80/swagger"
echo ""
echo "Para ver logs em tempo real:"
echo "  journalctl -u sgsb-api.service -f"
echo "  journalctl -u sgsb-web.service -f"
echo ""

