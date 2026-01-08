#!/bin/bash
# Script para configurar serviço na porta 80 e verificar problemas

echo "========================================"
echo "Configurando SGSB na Porta 80"
echo "========================================"
echo ""

# 1. Verificar se porta 80 está livre
echo "[1/6] Verificando porta 80..."
if lsof -i:80 > /dev/null 2>&1; then
    echo "⚠ AVISO: Porta 80 já está em uso!"
    echo "Processos usando porta 80:"
    lsof -i:80
    echo ""
    read -p "Deseja continuar mesmo assim? (s/n) " -n 1 -r
    echo
    if [[ ! $REPLY =~ ^[Ss]$ ]]; then
        exit 1
    fi
else
    echo "✓ Porta 80 está livre"
fi

# 2. Verificar firewall
echo ""
echo "[2/6] Verificando firewall..."
if command -v ufw &> /dev/null; then
    echo "UFW encontrado. Verificando regras..."
    ufw status | grep 80 || echo "Porta 80 não está aberta no UFW"
    echo "Para abrir porta 80: ufw allow 80/tcp"
elif command -v iptables &> /dev/null; then
    echo "iptables encontrado"
    iptables -L -n | grep -E "80|ACCEPT" | head -5
fi

# 3. Parar serviço atual
echo ""
echo "[3/6] Parando serviço atual..."
systemctl stop sgsb-api.service

# 4. Atualizar appsettings.json para porta 80
echo ""
echo "[4/6] Atualizando configuração para porta 80..."
if [ -f "/var/www/sgsb/publish/appsettings.json" ]; then
    # Criar backup
    cp /var/www/sgsb/publish/appsettings.json /var/www/sgsb/publish/appsettings.json.bak
    
    # Atualizar (usando jq se disponível, senão sed)
    if command -v jq &> /dev/null; then
        jq '.ASPNETCORE_URLS = "http://0.0.0.0:80"' /var/www/sgsb/publish/appsettings.json > /tmp/appsettings.json && \
        mv /tmp/appsettings.json /var/www/sgsb/publish/appsettings.json
    else
        # Usar sed como fallback
        sed -i 's|"ASPNETCORE_URLS":.*|"ASPNETCORE_URLS": "http://0.0.0.0:80",|' /var/www/sgsb/publish/appsettings.json || true
    fi
    echo "✓ appsettings.json atualizado"
else
    echo "⚠ appsettings.json não encontrado"
fi

# 5. Atualizar serviço systemd para porta 80
echo ""
echo "[5/6] Atualizando serviço systemd..."
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

# 6. Recarregar e iniciar
echo ""
echo "[6/6] Reiniciando serviço..."
systemctl daemon-reload
systemctl restart sgsb-api.service

# Aguardar e verificar
sleep 5
echo ""
echo "========================================"
echo "Status do serviço:"
echo "========================================"
systemctl status sgsb-api.service --no-pager -l

echo ""
echo "========================================"
echo "Verificando porta 80:"
echo "========================================"
netstat -tlnp | grep :80 || lsof -i:80

echo ""
echo "========================================"
echo "Últimos logs:"
echo "========================================"
journalctl -u sgsb-api.service -n 20 --no-pager

echo ""
echo "========================================"
echo "Configuração concluída!"
echo "========================================"
echo ""
echo "API disponível em:"
echo "  http://72.60.57.220"
echo "  http://72.60.57.220/swagger"
echo ""
echo "Se não estiver acessível, verifique:"
echo "  1. Firewall: ufw allow 80/tcp ou iptables -A INPUT -p tcp --dport 80 -j ACCEPT"
echo "  2. Logs: journalctl -u sgsb-api.service -f"
echo "  3. Porta: netstat -tlnp | grep 80"
echo ""

