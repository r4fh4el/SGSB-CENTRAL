#!/bin/bash
# Script para verificar e iniciar a API

echo "========================================"
echo "Verificando e Configurando API"
echo "========================================"
echo ""

# 1. Verificar status da API
echo "[1/4] Status do serviço sgsb-api:"
systemctl status sgsb-api.service --no-pager -l | head -20

# 2. Verificar se está escutando na porta 80
echo ""
echo "[2/4] Verificando porta 80:"
netstat -tlnp | grep ":80" || echo "Nenhum processo escutando na porta 80"

# 3. Verificar logs recentes da API
echo ""
echo "[3/4] Últimos logs da API:"
journalctl -u sgsb-api.service -n 30 --no-pager | tail -20

# 4. Verificar se o serviço existe e está configurado
echo ""
echo "[4/4] Verificando configuração do serviço:"
if [ -f /etc/systemd/system/sgsb-api.service ]; then
    echo "Arquivo de serviço encontrado:"
    cat /etc/systemd/system/sgsb-api.service
else
    echo "Arquivo de serviço NÃO encontrado!"
    echo "Criando serviço..."
    
    # Criar serviço da API
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
Environment="LANGUAGE=C"
Environment="LC_CTYPE=C"
Environment="LC_NUMERIC=C"
Environment="LC_TIME=C"
Environment="LC_COLLATE=C"
Environment="LC_MONETARY=C"
Environment="LC_MESSAGES=C"
StandardOutput=journal
StandardError=journal

[Install]
WantedBy=multi-user.target
EOF

    systemctl daemon-reload
    systemctl enable sgsb-api.service
    systemctl start sgsb-api.service
    
    echo "Serviço criado e iniciado!"
fi

# 5. Verificar se precisa rebuild da API
echo ""
echo "Verificando se a API precisa ser rebuild..."
if [ ! -f "/var/www/sgsb/publish/WebAPI.dll" ]; then
    echo "WebAPI.dll não encontrado! Precisa fazer build da API."
    echo "Execute: cd /var/www/sgsb/SIstemaGestaoSegurancaBarragem-master/WebAPI && dotnet publish --configuration Release --output /var/www/sgsb/publish"
else
    echo "WebAPI.dll encontrado."
fi

# 6. Status final
echo ""
echo "========================================"
echo "Status Final:"
echo "========================================"
systemctl status sgsb-api.service --no-pager -l | head -15
echo ""
echo "Portas abertas:"
netstat -tlnp | grep -E ":80|:8080"

echo ""
echo "========================================"
echo "Para iniciar a API manualmente:"
echo "  systemctl start sgsb-api.service"
echo "========================================"

