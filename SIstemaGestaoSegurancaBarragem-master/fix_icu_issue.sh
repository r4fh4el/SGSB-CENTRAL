#!/bin/bash
# Script para corrigir problema do ICU no servidor

echo "========================================"
echo "Corrigindo problema do ICU"
echo "========================================"
echo ""

# 1. Verificar logs detalhados
echo "[1/6] Verificando logs do serviço..."
journalctl -u sgsb-api.service -n 50 --no-pager

# 2. Verificar se libicu está instalado
echo ""
echo "[2/6] Verificando pacotes ICU instalados..."
dpkg -l | grep -i icu

# 3. Instalar todas as dependências necessárias
echo ""
echo "[3/6] Instalando todas as dependências do .NET..."
apt-get update -y
apt-get install -y \
    libicu-dev \
    libicu70 \
    libicu71 \
    libssl3 \
    libssl-dev \
    libkrb5-3 \
    libkrb5-dev \
    zlib1g \
    libgssapi-krb5-2 \
    libc6 \
    libc6-dev \
    libgcc-s1 \
    libstdc++6

# 4. Verificar instalação do .NET
echo ""
echo "[4/6] Verificando instalação do .NET..."
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
if command -v dotnet &> /dev/null; then
    echo "Dotnet encontrado em: $(which dotnet)"
    dotnet --version
    dotnet --info | head -20
else
    echo "ERRO: .NET não encontrado no PATH"
    exit 1
fi

# 5. Testar execução direta do dotnet
echo ""
echo "[5/6] Testando execução do dotnet..."
cd /var/www/sgsb/SIstemaGestaoSegurancaBarragem-master/WebAPI
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
dotnet --version

# 6. Atualizar serviço systemd com configurações corretas
echo ""
echo "[6/6] Atualizando serviço systemd..."
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
WorkingDirectory=/var/www/sgsb/publish
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

# Aguardar um pouco e verificar status
sleep 3
echo ""
echo "Status do serviço:"
systemctl status sgsb-api.service --no-pager -l

echo ""
echo "========================================"
echo "Se ainda houver erro, verifique os logs:"
echo "  journalctl -u sgsb-api.service -f"
echo "========================================"

