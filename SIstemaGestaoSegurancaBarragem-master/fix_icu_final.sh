#!/bin/bash
# Script para resolver definitivamente o problema do ICU
# Usando modo invariante do .NET

echo "========================================"
echo "Resolvendo problema do ICU (Modo Invariante)"
echo "========================================"
echo ""

# 1. Verificar se o publish foi feito
if [ ! -f "/var/www/sgsb/publish/WebAPI.dll" ]; then
    echo "Publicando projeto primeiro..."
    export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
    export DOTNET_ROOT=$HOME/.dotnet
    cd /var/www/sgsb/SIstemaGestaoSegurancaBarragem-master/WebAPI
    dotnet restore
    dotnet publish --configuration Release --output /var/www/sgsb/publish
fi

# 2. Atualizar serviço systemd com modo invariante
echo "Atualizando serviço systemd com modo invariante..."
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
Environment="DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true"
Environment="LC_ALL=C"
Environment="LANG=C"

[Install]
WantedBy=multi-user.target
EOF

# 3. Recarregar e reiniciar
systemctl daemon-reload
systemctl restart sgsb-api.service

# 4. Aguardar e verificar
sleep 5
echo ""
echo "Status do serviço:"
systemctl status sgsb-api.service --no-pager -l

echo ""
echo "Se ainda falhar, tente executar manualmente para ver o erro:"
echo "  cd /var/www/sgsb/publish"
echo "  DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true /root/.dotnet/dotnet WebAPI.dll"

