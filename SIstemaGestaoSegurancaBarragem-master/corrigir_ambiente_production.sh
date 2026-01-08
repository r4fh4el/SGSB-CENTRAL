#!/bin/bash
# Script para corrigir ambiente para Production

echo "========================================"
echo "Corrigindo Ambiente para Production"
echo "========================================"
echo ""

# Atualizar serviço SGSB.Web para usar Production
echo "[1/2] Atualizando serviço SGSB.Web..."
cat > /etc/systemd/system/sgsb-web.service << 'EOF'
[Unit]
Description=SGSB Web Application
After=network.target

[Service]
Type=simple
ExecStart=/root/.dotnet/dotnet /var/www/sgsb/publish-web/SGSB.Web.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
TimeoutStartSec=0
TimeoutStopSec=30
SyslogIdentifier=sgsb-web
User=root
WorkingDirectory=/var/www/sgsb/publish-web
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://0.0.0.0:8080
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

# Atualizar serviço WebAPI também (garantir)
echo "[2/2] Verificando serviço WebAPI..."
cat > /etc/systemd/system/sgsb-api.service << 'EOF'
[Unit]
Description=SGSB Web API
After=network.target

[Service]
Type=simple
ExecStart=/root/.dotnet/dotnet /var/www/sgsb/publish/WebAPI.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
TimeoutStartSec=0
TimeoutStopSec=30
SyslogIdentifier=sgsb-api
User=root
WorkingDirectory=/var/www/sgsb/publish
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://0.0.0.0:80
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

# Recarregar e reiniciar
systemctl daemon-reload
systemctl restart sgsb-api.service
systemctl restart sgsb-web.service

# Aguardar e verificar
sleep 5
echo ""
echo "Status dos serviços:"
systemctl status sgsb-api.service --no-pager -l | head -10
echo ""
systemctl status sgsb-web.service --no-pager -l | head -10

echo ""
echo "========================================"
echo "Ambiente configurado para Production!"
echo "========================================"
echo ""
echo "Verifique os logs se ainda houver erro:"
echo "  journalctl -u sgsb-web.service -n 50 --no-pager"
echo ""

