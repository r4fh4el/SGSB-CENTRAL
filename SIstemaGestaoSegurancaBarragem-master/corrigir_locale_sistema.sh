#!/bin/bash
# Script para corrigir locale do sistema e forçar C/Invariant

echo "========================================"
echo "Corrigindo Locale do Sistema"
echo "========================================"
echo ""

# 1. Verificar locale atual
echo "[1/4] Locale atual do sistema:"
locale
echo ""

# 2. Verificar locale do processo
echo "[2/4] Locale do processo sgsb-web:"
PID=$(systemctl show sgsb-web.service -p MainPID --value)
if [ ! -z "$PID" ] && [ "$PID" != "0" ]; then
    echo "PID: $PID"
    cat /proc/$PID/environ | tr '\0' '\n' | grep -i "lang\|lc" || echo "Nenhuma variável encontrada"
else
    echo "Serviço não está rodando"
fi

# 3. Configurar locale globalmente (se possível)
echo ""
echo "[3/4] Configurando locale para C (invariante)..."
export LC_ALL=C
export LANG=C
export LANGUAGE=C

# Verificar se /etc/default/locale existe e configurar
if [ -f /etc/default/locale ]; then
    echo "Arquivo /etc/default/locale encontrado"
    sudo sed -i 's/^LANG=.*/LANG=C/' /etc/default/locale 2>/dev/null || echo "Não foi possível modificar /etc/default/locale"
    sudo sed -i 's/^LC_ALL=.*/LC_ALL=C/' /etc/default/locale 2>/dev/null || echo "Não foi possível modificar /etc/default/locale"
fi

# 4. Atualizar serviço systemd com locale explícito
echo ""
echo "[4/4] Atualizando serviço systemd..."
cat > /etc/systemd/system/sgsb-web.service << 'EOF'
[Unit]
Description=SGSB Web Application
After=network.target

[Service]
Type=simple
ExecStart=/root/.dotnet/dotnet /var/www/sgsb/publish-web/SGSB.Web.dll
Restart=always
RestartSec=10
User=root
WorkingDirectory=/var/www/sgsb/publish-web
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://0.0.0.0:8080
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
systemctl restart sgsb-web.service

# Aguardar e verificar
sleep 5
echo ""
echo "Status do serviço:"
systemctl status sgsb-web.service --no-pager -l | head -15

echo ""
echo "Verificando locale do processo após reinício:"
NEW_PID=$(systemctl show sgsb-web.service -p MainPID --value)
if [ ! -z "$NEW_PID" ] && [ "$NEW_PID" != "0" ]; then
    echo "Novo PID: $NEW_PID"
    cat /proc/$NEW_PID/environ | tr '\0' '\n' | grep -i "lang\|lc" || echo "Nenhuma variável encontrada"
fi

echo ""
echo "========================================"
echo "Correção aplicada!"
echo "========================================"
echo ""
echo "Teste o login novamente. Se o erro persistir,"
echo "pode ser necessário usar uma workaround no código."
echo ""

