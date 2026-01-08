#!/bin/bash
# Script para corrigir o serviço systemd (sem WorkingDirectory)

echo "========================================"
echo "Corrigindo serviço systemd"
echo "========================================"
echo ""

# Verificar se o arquivo DLL existe
if [ ! -f "/var/www/sgsb/publish/WebAPI.dll" ]; then
    echo "ERRO: WebAPI.dll não encontrado em /var/www/sgsb/publish/"
    echo "Execute primeiro: ./fix_publish_directory.sh"
    exit 1
fi

# Criar serviço sem WorkingDirectory (ou usar diretório que existe)
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
SyslogIdentifier=sgsb-api
User=root
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://0.0.0.0:5000
Environment="PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin:/root/.dotnet:/root/.dotnet/tools"
Environment="DOTNET_ROOT=/root/.dotnet"
Environment="DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false"
Environment="LC_ALL=en_US.UTF-8"
Environment="LANG=en_US.UTF-8"

[Install]
WantedBy=multi-user.target
EOF

# Recarregar e reiniciar
systemctl daemon-reload
systemctl restart sgsb-api.service

# Aguardar e verificar
sleep 5
echo ""
echo "Status do serviço:"
systemctl status sgsb-api.service --no-pager -l

echo ""
echo "Se estiver rodando, acesse:"
echo "  http://72.60.57.220:5000/swagger"
echo ""
echo "Para ver logs em tempo real:"
echo "  journalctl -u sgsb-api.service -f"

