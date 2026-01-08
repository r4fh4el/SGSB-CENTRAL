#!/bin/bash
# Script para fazer deploy completo de API e WEB

echo "========================================"
echo "Deploy Completo: API + WEB"
echo "========================================"
echo ""

# Configurar variáveis
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
export LC_ALL=C
export LANG=C
export LANGUAGE=C

PROJECT_PATH="/var/www/sgsb/SIstemaGestaoSegurancaBarragem-master"

# ============================================
# PARTE 1: DEPLOY DA API
# ============================================
echo "========================================"
echo "PARTE 1: Deploy da API (Porta 80)"
echo "========================================"
echo ""

# 1. Parar serviço
echo "[API 1/6] Parando serviço da API..."
systemctl stop sgsb-api.service 2>/dev/null || echo "Serviço não estava rodando"

# 2. Limpar
echo "[API 2/6] Limpando builds anteriores..."
cd $PROJECT_PATH/WebAPI
dotnet clean --configuration Release

# 3. Restore
echo "[API 3/6] Restaurando pacotes..."
dotnet restore

# 4. Build
echo "[API 4/6] Compilando..."
dotnet build --configuration Release

# 5. Publish
echo "[API 5/6] Publicando..."
dotnet publish --configuration Release --output /var/www/sgsb/publish

# 6. Configurar runtimeconfig.json
echo "[API 6/6] Configurando runtimeconfig.json..."
cat > /var/www/sgsb/publish/WebAPI.runtimeconfig.json << 'EOF'
{
  "runtimeOptions": {
    "tfm": "net7.0",
    "framework": {
      "name": "Microsoft.AspNetCore.App",
      "version": "7.0.0"
    },
    "configProperties": {
      "System.Globalization.Invariant": true
    }
  }
}
EOF

# 7. Configurar serviço
cat > /etc/systemd/system/sgsb-api.service << 'EOF'
[Unit]
Description=SGSB Web API
After=network.target

[Service]
Type=simple
ExecStart=/root/.dotnet/dotnet /var/www/sgsb/publish/WebAPI.dll
Restart=always
RestartSec=10
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
[Install]
WantedBy=multi-user.target
EOF

systemctl daemon-reload
systemctl enable sgsb-api.service
systemctl start sgsb-api.service

echo ""
echo "API iniciada. Aguardando 5 segundos..."
sleep 5

# ============================================
# PARTE 2: DEPLOY DA WEB
# ============================================
echo ""
echo "========================================"
echo "PARTE 2: Deploy da WEB (Porta 8080)"
echo "========================================"
echo ""

# 1. Parar serviço
echo "[WEB 1/6] Parando serviço da WEB..."
systemctl stop sgsb-web.service 2>/dev/null || echo "Serviço não estava rodando"

# 2. Limpar
echo "[WEB 2/6] Limpando builds anteriores..."
cd $PROJECT_PATH/SGSB.Web
dotnet clean --configuration Release

# 3. Restore
echo "[WEB 3/6] Restaurando pacotes..."
dotnet restore

# 4. Build
echo "[WEB 4/6] Compilando..."
dotnet build --configuration Release

# 5. Publish
echo "[WEB 5/6] Publicando..."
dotnet publish --configuration Release --output /var/www/sgsb/publish-web

# 6. Configurar runtimeconfig.json
echo "[WEB 6/6] Configurando runtimeconfig.json..."
cat > /var/www/sgsb/publish-web/SGSB.Web.runtimeconfig.json << 'EOF'
{
  "runtimeOptions": {
    "tfm": "net7.0",
    "framework": {
      "name": "Microsoft.AspNetCore.App",
      "version": "7.0.0"
    },
    "configProperties": {
      "System.Globalization.Invariant": true
    }
  }
}
EOF

# 7. Configurar serviço
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
[Install]
WantedBy=multi-user.target
EOF

systemctl daemon-reload
systemctl enable sgsb-web.service
systemctl start sgsb-web.service

echo ""
echo "WEB iniciada. Aguardando 5 segundos..."
sleep 5

# ============================================
# VERIFICAÇÃO FINAL
# ============================================
echo ""
echo "========================================"
echo "Verificação Final"
echo "========================================"
echo ""

echo "Status da API:"
systemctl status sgsb-api.service --no-pager -l | head -10

echo ""
echo "Status da WEB:"
systemctl status sgsb-web.service --no-pager -l | head -10

echo ""
echo "Portas abertas:"
netstat -tlnp | grep -E ":80|:8080"

echo ""
echo "Testando API:"
curl -I http://localhost:80 2>/dev/null | head -3 || echo "API não respondeu"

echo ""
echo "Testando WEB:"
curl -I http://localhost:8080 2>/dev/null | head -3 || echo "WEB não respondeu"

echo ""
echo "========================================"
echo "Deploy Completo Concluído!"
echo "========================================"
echo ""
echo "API: http://72.60.57.220:80"
echo "WEB: http://72.60.57.220:8080"
echo ""
echo "Verifique os logs se houver problemas:"
echo "  journalctl -u sgsb-api.service -n 50 --no-pager"
echo "  journalctl -u sgsb-web.service -n 50 --no-pager"
echo ""

