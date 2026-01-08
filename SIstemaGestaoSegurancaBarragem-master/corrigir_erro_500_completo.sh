#!/bin/bash

echo "========================================"
echo "Correção Completa do Erro 500 no Register"
echo "========================================"

cd /var/www/sgsb
git pull origin main
cd SIstemaGestaoSegurancaBarragem-master

# Configurar variáveis
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
export LC_ALL=C
export LANG=C
export LANGUAGE=C

echo ""
echo "[1/6] Verificando status dos serviços:"
echo "----------------------------------------"
systemctl status sgsb-api.service --no-pager -l | head -10
echo ""
systemctl status sgsb-web.service --no-pager -l | head -10

echo ""
echo "[2/6] Últimos 30 logs do sgsb-web.service (erros):"
echo "----------------------------------------"
journalctl -u sgsb-web.service -n 30 --no-pager | grep -i -E "error|exception|fail|500|Culture" | tail -20

echo ""
echo "[3/6] Instalando dotnet-ef tool (se necessário)..."
echo "----------------------------------------"
dotnet tool list -g | grep dotnet-ef > /dev/null
if [ $? -ne 0 ]; then
    echo "Instalando dotnet-ef..."
    dotnet tool install --global dotnet-ef 2>&1
else
    echo "✓ dotnet-ef já está instalado"
fi

echo ""
echo "[4/6] Aplicando migrations do Identity..."
echo "----------------------------------------"
cd SGSB.Web
dotnet ef database update --context ApplicationDbContext 2>&1

if [ $? -eq 0 ]; then
    echo "✓ Migrations aplicadas com sucesso!"
else
    echo "✗ Erro ao aplicar migrations. Verificando se as tabelas já existem..."
fi

echo ""
echo "[5/6] Rebuild da aplicação..."
echo "----------------------------------------"
dotnet clean --configuration Release
dotnet restore
dotnet build --configuration Release
dotnet publish --configuration Release --output /var/www/sgsb/publish-web

echo ""
echo "[6/6] Reiniciando serviço..."
echo "----------------------------------------"
systemctl restart sgsb-web.service
sleep 5
systemctl status sgsb-web.service --no-pager -l | head -15

echo ""
echo "========================================"
echo "Concluído!"
echo "========================================"
echo ""
echo "Para ver logs em tempo real:"
echo "journalctl -u sgsb-web.service -f"
echo ""
echo "Para testar o Register, acesse:"
echo "http://72.60.57.220:8080/Identity/Account/Register"

