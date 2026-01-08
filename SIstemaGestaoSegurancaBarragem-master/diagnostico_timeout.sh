#!/bin/bash
# Script para diagnosticar problema de timeout

echo "========================================"
echo "Diagnóstico de Timeout do Serviço"
echo "========================================"
echo ""

# 1. Ver logs completos
echo "[1/5] Logs completos do serviço:"
journalctl -u sgsb-api.service -n 100 --no-pager

# 2. Verificar se há erros de conexão com banco
echo ""
echo "[2/5] Verificando erros de banco de dados:"
journalctl -u sgsb-api.service -n 200 --no-pager | grep -i "database\|connection\|sql\|timeout\|exception" | tail -20

# 3. Testar execução manual
echo ""
echo "[3/5] Testando execução manual (vai travar se houver problema):"
cd /var/www/sgsb/publish
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
export ASPNETCORE_ENVIRONMENT=Production
export ASPNETCORE_URLS=http://0.0.0.0:5000

echo "Executando manualmente (pressione Ctrl+C após 10 segundos se travar)..."
timeout 10 /root/.dotnet/dotnet WebAPI.dll 2>&1 || echo "Timeout ou erro na execução manual"

# 4. Verificar appsettings.json
echo ""
echo "[4/5] Verificando configuração do banco de dados:"
if [ -f "/var/www/sgsb/publish/appsettings.json" ]; then
    echo "String de conexão:"
    cat /var/www/sgsb/publish/appsettings.json | grep -A 2 "ConnectionStrings" || echo "ConnectionStrings não encontrado"
else
    echo "✗ appsettings.json não encontrado!"
fi

# 5. Verificar se consegue conectar ao banco
echo ""
echo "[5/5] Testando conectividade com banco de dados:"
echo "Verificando se o servidor SQL está acessível..."
# Tentar ping ou telnet na porta do SQL Server
timeout 5 bash -c "echo > /dev/tcp/108.181.193.92/15000" 2>/dev/null && echo "✓ Porta 15000 está acessível" || echo "✗ Não conseguiu conectar na porta 15000"

echo ""
echo "========================================"
echo "Diagnóstico concluído!"
echo "========================================"
echo ""
echo "Possíveis soluções:"
echo "  1. Se houver erro de banco: Verificar string de conexão"
echo "  2. Se travar na inicialização: Verificar logs acima"
echo "  3. Aumentar timeout do serviço ou usar Type=simple em vez de notify"
echo ""

