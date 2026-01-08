#!/bin/bash
# Script para corrigir timeout do serviço

echo "========================================"
echo "Corrigindo timeout do serviço"
echo "========================================"
echo ""

# Ver logs recentes
echo "Últimos logs do serviço:"
journalctl -u sgsb-api.service -n 100 --no-pager | tail -50

echo ""
echo "Atualizando serviço com timeout maior e melhor logging..."

# Atualizar serviço com timeout maior
cat > /etc/systemd/system/sgsb-api.service << 'EOF'
[Unit]
Description=SGSB Web API
After=network.target

[Service]
Type=notify
ExecStart=/root/.dotnet/dotnet /var/www/sgsb/publish/WebAPI.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
TimeoutStartSec=300
TimeoutStopSec=30
SyslogIdentifier=sgsb-api
User=root
WorkingDirectory=/var/www/sgsb/publish
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://0.0.0.0:5000
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

# Aguardar e verificar
sleep 10
echo ""
echo "Status do serviço:"
systemctl status sgsb-api.service --no-pager -l

echo ""
echo "Se ainda falhar, verifique:"
echo "  1. Conexão com banco de dados"
echo "  2. Logs completos: journalctl -u sgsb-api.service -f"
echo "  3. Teste manual: cd /var/www/sgsb/publish && /root/.dotnet/dotnet WebAPI.dll"

