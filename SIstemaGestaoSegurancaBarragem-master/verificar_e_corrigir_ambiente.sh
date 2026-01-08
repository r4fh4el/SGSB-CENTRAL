#!/bin/bash
# Script para verificar e corrigir ambiente de produção

echo "========================================"
echo "Verificando e Corrigindo Ambiente"
echo "========================================"
echo ""

# 1. Verificar variável de ambiente atual do serviço
echo "[1/4] Verificando configuração atual do serviço..."
systemctl show sgsb-web.service | grep -i environment

# 2. Verificar se appsettings.Development.json está sendo usado
echo ""
echo "[2/4] Verificando arquivos de configuração..."
ls -la /var/www/sgsb/publish-web/appsettings*.json

# 3. Verificar logs para ver qual ambiente está sendo usado
echo ""
echo "[3/4] Verificando logs para ambiente atual..."
journalctl -u sgsb-web.service -n 50 --no-pager | grep -i "environment\|development\|production" | head -10

# 4. Corrigir serviço systemd
echo ""
echo "[4/4] Corrigindo serviço systemd..."
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

# 5. Remover ou renomear appsettings.Development.json se existir no publish
if [ -f "/var/www/sgsb/publish-web/appsettings.Development.json" ]; then
    echo "Renomeando appsettings.Development.json para evitar uso..."
    mv /var/www/sgsb/publish-web/appsettings.Development.json /var/www/sgsb/publish-web/appsettings.Development.json.bak
fi

# 6. Recarregar e reiniciar
systemctl daemon-reload
systemctl restart sgsb-web.service

# 7. Aguardar e verificar
sleep 5
echo ""
echo "Status do serviço:"
systemctl status sgsb-web.service --no-pager -l | head -15

echo ""
echo "Verificando ambiente nos logs:"
journalctl -u sgsb-web.service -n 20 --no-pager | grep -i "environment\|hosting environment" || echo "Verifique manualmente os logs"

echo ""
echo "========================================"
echo "Correção aplicada!"
echo "========================================"
echo ""
echo "Se ainda aparecer Development Mode, verifique:"
echo "  1. systemctl show sgsb-web.service | grep ASPNETCORE_ENVIRONMENT"
echo "  2. journalctl -u sgsb-web.service -f (para ver logs em tempo real)"
echo ""

