#!/bin/bash

echo "========================================"
echo "Remover Modo Invariante e Usar ICU"
echo "========================================"

# 1. Instalar ICU primeiro
echo "[1/5] Instalando ICU..."
echo "----------------------------------------"
apt-get update
apt-get install -y libicu-dev libicu72
echo ""

# 2. Atualizar systemd service para remover modo invariante
echo "[2/5] Atualizando systemd service..."
echo "----------------------------------------"
SERVICE_FILE="/etc/systemd/system/sgsb-web.service"

# Fazer backup
cp "$SERVICE_FILE" "${SERVICE_FILE}.backup.$(date +%Y%m%d_%H%M%S)"

# Criar novo arquivo sem modo invariante, mas com locale en_US.UTF-8
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

# Configuracoes de locale (com ICU instalado)
Environment="LC_ALL=en_US.UTF-8"
Environment="LANG=en_US.UTF-8"
Environment="LANGUAGE=en_US.UTF-8"
Environment="ASPNETCORE_ENVIRONMENT=Production"
Environment="ASPNETCORE_URLS=http://0.0.0.0:8080"

[Install]
WantedBy=multi-user.target
EOF

echo "Arquivo atualizado"
echo ""

# 3. Atualizar runtimeconfig.json para remover modo invariante
echo "[3/5] Atualizando runtimeconfig.json..."
echo "----------------------------------------"
cat > /var/www/sgsb/publish-web/SGSB.Web.runtimeconfig.json << 'EOF'
{
  "runtimeOptions": {
    "tfm": "net7.0",
    "framework": {
      "name": "Microsoft.AspNetCore.App",
      "version": "7.0.0"
    }
  }
}
EOF
echo "runtimeconfig.json atualizado (modo invariante removido)"
echo ""

# 4. Configurar locale do sistema
echo "[4/5] Configurando locale do sistema..."
echo "----------------------------------------"
locale-gen en_US.UTF-8
update-locale LANG=en_US.UTF-8
export LC_ALL=en_US.UTF-8
export LANG=en_US.UTF-8
echo ""

# 5. Recarregar systemd e reiniciar
echo "[5/5] Recarregando systemd e reiniciando servico..."
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
echo "Agora o sistema usa ICU ao inves de modo invariante."
echo "Isso deve resolver o problema do Microsoft.Data.SqlClient."
echo ""

