#!/bin/bash
# Script para fazer deploy completo da API

echo "========================================"
echo "Deploy Completo da API"
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

# 1. Parar serviço se estiver rodando
echo "[1/6] Parando serviço da API (se estiver rodando)..."
systemctl stop sgsb-api.service 2>/dev/null || echo "Serviço não estava rodando"

# 2. Limpar builds anteriores
echo ""
echo "[2/6] Limpando builds anteriores..."
cd $PROJECT_PATH/WebAPI
dotnet clean --configuration Release

# 3. Restore
echo ""
echo "[3/6] Restaurando pacotes..."
dotnet restore

# 4. Build
echo ""
echo "[4/6] Compilando..."
dotnet build --configuration Release

# 5. Publish
echo ""
echo "[5/6] Publicando..."
dotnet publish --configuration Release --output /var/www/sgsb/publish

# 6. Configurar runtimeconfig.json
echo ""
echo "[6/6] Configurando runtimeconfig.json..."
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

# 7. Criar/Atualizar serviço systemd
echo ""
echo "Configurando serviço systemd..."
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

# 8. Recarregar e iniciar
systemctl daemon-reload
systemctl enable sgsb-api.service
systemctl start sgsb-api.service

# Aguardar
sleep 5

# Verificar
echo ""
echo "========================================"
echo "Status da API:"
echo "========================================"
systemctl status sgsb-api.service --no-pager -l | head -20

echo ""
echo "Verificando porta 80:"
netstat -tlnp | grep ":80" || echo "Nenhum processo escutando na porta 80"

echo ""
echo "Testando API localmente:"
curl -I http://localhost:80 2>/dev/null | head -5 || echo "API não respondeu"

echo ""
echo "========================================"
echo "Deploy da API concluído!"
echo "========================================"
echo ""
echo "Verifique os logs se houver problemas:"
echo "  journalctl -u sgsb-api.service -n 50 --no-pager"
echo ""

