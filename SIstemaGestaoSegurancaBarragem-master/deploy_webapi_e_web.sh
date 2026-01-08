#!/bin/bash
# Script para fazer deploy do WebAPI e SGSB.Web

echo "========================================"
echo "Deploy WebAPI e SGSB.Web"
echo "========================================"
echo ""

# Configurar variáveis
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true

PROJECT_PATH="/var/www/sgsb"
WEBAPI_PORT="80"
WEB_PORT="8080"

# ========================================
# 1. WEBAPI
# ========================================
echo "[1/2] Fazendo deploy do WebAPI..."
echo ""

cd $PROJECT_PATH/SIstemaGestaoSegurancaBarragem-master/WebAPI

# Build e Publish WebAPI
echo "Build WebAPI..."
dotnet restore
dotnet build --configuration Release
dotnet publish --configuration Release --output $PROJECT_PATH/publish

# Configurar runtimeconfig.json para WebAPI
cat > $PROJECT_PATH/publish/WebAPI.runtimeconfig.json << 'EOF'
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

# Configurar serviço WebAPI
cat > /etc/systemd/system/sgsb-api.service << EOF
[Unit]
Description=SGSB Web API
After=network.target

[Service]
Type=simple
ExecStart=/root/.dotnet/dotnet $PROJECT_PATH/publish/WebAPI.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
TimeoutStartSec=0
TimeoutStopSec=30
SyslogIdentifier=sgsb-api
User=root
WorkingDirectory=$PROJECT_PATH/publish
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://0.0.0.0:$WEBAPI_PORT
Environment="PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin:/root/.dotnet:/root/.dotnet/tools"
Environment="DOTNET_ROOT=/root/.dotnet"
Environment="DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true"
Environment="LC_ALL=C"
Environment="LANG=C"
StandardOutput=journal
StandardError=journal

[Install]
WantedBy=multi-user.target
EOF

# Iniciar WebAPI
systemctl daemon-reload
systemctl enable sgsb-api.service
systemctl restart sgsb-api.service

echo "✓ WebAPI configurado na porta $WEBAPI_PORT"

# ========================================
# 2. SGSB.WEB
# ========================================
echo ""
echo "[2/2] Fazendo deploy do SGSB.Web..."
echo ""

cd $PROJECT_PATH/SIstemaGestaoSegurancaBarragem-master/SGSB.Web

# Build e Publish SGSB.Web
echo "Build SGSB.Web..."
dotnet restore
dotnet build --configuration Release
dotnet publish --configuration Release --output $PROJECT_PATH/publish-web

# Configurar runtimeconfig.json para SGSB.Web
cat > $PROJECT_PATH/publish-web/SGSB.Web.runtimeconfig.json << 'EOF'
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

# Liberar porta do Web
lsof -ti:$WEB_PORT | xargs kill -9 2>/dev/null || true

# Configurar serviço SGSB.Web
cat > /etc/systemd/system/sgsb-web.service << EOF
[Unit]
Description=SGSB Web Application
After=network.target

[Service]
Type=simple
ExecStart=/root/.dotnet/dotnet $PROJECT_PATH/publish-web/SGSB.Web.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
TimeoutStartSec=0
TimeoutStopSec=30
SyslogIdentifier=sgsb-web
User=root
WorkingDirectory=$PROJECT_PATH/publish-web
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://0.0.0.0:$WEB_PORT
Environment="PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin:/root/.dotnet:/root/.dotnet/tools"
Environment="DOTNET_ROOT=/root/.dotnet"
Environment="DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true"
Environment="LC_ALL=C"
Environment="LANG=C"
StandardOutput=journal
StandardError=journal

[Install]
WantedBy=multi-user.target
EOF

# Abrir firewall
ufw allow $WEB_PORT/tcp 2>/dev/null || iptables -A INPUT -p tcp --dport $WEB_PORT -j ACCEPT 2>/dev/null || true

# Iniciar SGSB.Web
systemctl daemon-reload
systemctl enable sgsb-web.service
systemctl restart sgsb-web.service

echo "✓ SGSB.Web configurado na porta $WEB_PORT"

# ========================================
# Verificar status
# ========================================
echo ""
echo "========================================"
echo "Status dos serviços:"
echo "========================================"
echo ""
echo "WebAPI:"
systemctl status sgsb-api.service --no-pager -l | head -10

echo ""
echo "SGSB.Web:"
systemctl status sgsb-web.service --no-pager -l | head -10

echo ""
echo "========================================"
echo "Deploy concluído!"
echo "========================================"
echo ""
echo "WebAPI disponível em:"
echo "  http://72.60.57.220"
echo "  http://72.60.57.220/swagger"
echo ""
echo "SGSB.Web disponível em:"
echo "  http://72.60.57.220:$WEB_PORT"
echo ""
echo "Comandos úteis:"
echo "  Ver logs WebAPI: journalctl -u sgsb-api.service -f"
echo "  Ver logs Web: journalctl -u sgsb-web.service -f"
echo "  Reiniciar WebAPI: systemctl restart sgsb-api.service"
echo "  Reiniciar Web: systemctl restart sgsb-web.service"
echo ""

