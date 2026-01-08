#!/bin/bash
# Script para testar execução manual do dotnet

echo "Testando execução manual do .NET..."

# Configurar variáveis
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet

# Testar diferentes configurações
echo ""
echo "1. Testando com modo invariante:"
cd /var/www/sgsb/publish
DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true /root/.dotnet/dotnet WebAPI.dll &
PID=$!
sleep 3
if ps -p $PID > /dev/null; then
    echo "✓ Funcionou com modo invariante!"
    kill $PID
else
    echo "✗ Falhou mesmo com modo invariante"
fi

echo ""
echo "2. Verificando se ICU está acessível:"
ldconfig -p | grep icu

echo ""
echo "3. Verificando variáveis de ambiente do .NET:"
/root/.dotnet/dotnet --info | grep -i "runtime\|icu\|globalization" | head -10

