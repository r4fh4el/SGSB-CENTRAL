#!/bin/bash
# Script de deploy usando modo invariante (sem ICU)
# Esta é a solução definitiva para sistemas sem ICU compatível

set -e

echo "========================================"
echo "Deploy SGSB - Modo Invariante (Sem ICU)"
echo "========================================"
echo ""

# 1. Usar .NET já instalado via script (não via apt)
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true

# 2. Verificar .NET
if ! command -v dotnet &> /dev/null; then
    echo "Instalando .NET SDK 7.0 via script..."
    wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
    chmod +x dotnet-install.sh
    ./dotnet-install.sh --channel 7.0
    export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
    echo 'export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools' >> ~/.bashrc
    echo 'export DOTNET_ROOT=$HOME/.dotnet' >> ~/.bashrc
    echo 'export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true' >> ~/.bashrc
fi

echo "Versão do .NET:"
dotnet --version

# 3. Clonar/Atualizar repositório
echo ""
echo "Atualizando repositório..."
cd /var/www/sgsb
if [ -d ".git" ]; then
    git pull origin main
else
    git clone https://github.com/r4fh4el/SGSB-CENTRAL.git .
fi

# 4. Publicar projeto
echo ""
echo "Publicando projeto..."
cd SIstemaGestaoSegurancaBarragem-master/WebAPI
dotnet restore
mkdir -p /var/www/sgsb/publish
dotnet publish --configuration Release --output /var/www/sgsb/publish

# 5. Criar arquivo de configuração runtimeconfig.json para forçar modo invariante
echo ""
echo "Configurando modo invariante no runtime..."
cat > /var/www/sgsb/publish/WebAPI.runtimeconfig.json << 'EOF'
{
  "runtimeOptions": {
    "configProperties": {
      "System.Globalization.Invariant": true
    }
  }
}
EOF

# 6. Criar serviço systemd
echo ""
echo "Configurando serviço systemd..."
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

# 7. Recarregar e iniciar
systemctl daemon-reload
systemctl enable sgsb-api.service
systemctl restart sgsb-api.service

# 8. Aguardar e verificar
sleep 5
echo ""
echo "========================================"
echo "Status do serviço:"
echo "========================================"
systemctl status sgsb-api.service --no-pager -l

echo ""
echo "========================================"
echo "Deploy concluído!"
echo "========================================"
echo "API disponível em: http://72.60.57.220:5000"
echo "Swagger: http://72.60.57.220:5000/swagger"
echo ""
echo "Para ver logs: journalctl -u sgsb-api.service -f"
echo ""

