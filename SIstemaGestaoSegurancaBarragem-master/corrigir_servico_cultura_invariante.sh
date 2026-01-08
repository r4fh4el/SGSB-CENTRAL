#!/bin/bash

echo "========================================"
echo "Corrigir Servico para Cultura Invariante"
echo "========================================"

SERVICE_FILE="/etc/systemd/system/sgsb-web.service"

# 1. Fazer backup
echo "[1/4] Fazendo backup do arquivo de servico..."
echo "----------------------------------------"
cp "$SERVICE_FILE" "${SERVICE_FILE}.backup.$(date +%Y%m%d_%H%M%S)"
echo "Backup criado"
echo ""

# 2. Verificar arquivo atual
echo "[2/4] Verificando arquivo atual..."
echo "----------------------------------------"
cat "$SERVICE_FILE"
echo ""

# 3. Atualizar arquivo com todas as variaveis de cultura
echo "[3/4] Atualizando arquivo de servico..."
echo "----------------------------------------"

# Criar novo arquivo de servico
cat > "$SERVICE_FILE" << 'EOF'
[Unit]
Description=SGSB Web Application
After=network.target

[Service]
Type=simple
User=root
WorkingDirectory=/var/www/sgsb/publish-web
ExecStart=/root/.dotnet/dotnet /var/www/sgsb/publish-web/SGSB.Web.dll
Restart=always
RestartSec=10
TimeoutStartSec=300

# Configuracoes de cultura invariante
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
Environment="ASPNETCORE_ENVIRONMENT=Production"
Environment="ASPNETCORE_URLS=http://0.0.0.0:8080"

[Install]
WantedBy=multi-user.target
EOF

echo "Arquivo atualizado"
echo ""

# 4. Recarregar systemd e reiniciar servico
echo "[4/4] Recarregando systemd e reiniciando servico..."
echo "----------------------------------------"
systemctl daemon-reload
systemctl restart sgsb-web.service
sleep 5

# Verificar status
systemctl status sgsb-web.service --no-pager -l | head -20
echo ""

echo "========================================"
echo "Processo concluido!"
echo "========================================"
echo ""
echo "Para verificar se as variaveis estao corretas:"
echo "  systemctl show sgsb-web.service | grep -E 'LC_|LANG|DOTNET'"
echo ""

