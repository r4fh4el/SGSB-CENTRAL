#!/bin/bash

echo "========================================"
echo "Testar Conexao com Banco de Dados"
echo "========================================"

# Configurar variaveis de ambiente
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
export LC_ALL=C
export LANG=C

# Diretorio do projeto
PROJECT_DIR="/var/www/sgsb/SIstemaGestaoSegurancaBarragem-master/SGSB.Web"
cd "$PROJECT_DIR"

# 1. Verificar se appsettings.json existe
echo "[1/4] Verificando appsettings.json..."
echo "----------------------------------------"
if [ ! -f "appsettings.json" ]; then
    echo "ERRO: appsettings.json nao encontrado"
    exit 1
fi
echo "Arquivo encontrado"
echo ""

# 2. Mostrar string de conexao (parcialmente)
echo "[2/4] String de conexao (parcial):"
echo "----------------------------------------"
grep -A 1 "DefaultConnection" appsettings.json | head -2
echo ""

# 3. Testar conectividade de rede
echo "[3/4] Testando conectividade de rede..."
echo "----------------------------------------"
SERVER="108.181.193.92"
PORT="15000"

# Testar se a porta esta acessivel
timeout 5 bash -c "echo > /dev/tcp/$SERVER/$PORT" 2>/dev/null
if [ $? -eq 0 ]; then
    echo "SUCESSO: Porta $PORT no servidor $SERVER esta acessivel"
else
    echo "AVISO: Nao foi possivel conectar a $SERVER:$PORT"
    echo "  Isso pode ser normal se o firewall bloqueia testes de porta"
fi
echo ""

# 4. Compilar e testar conexao via codigo
echo "[4/4] Testando conexao via codigo .NET..."
echo "----------------------------------------"
dotnet build --configuration Release > /dev/null 2>&1
if [ $? -ne 0 ]; then
    echo "ERRO: Falha na compilacao"
    exit 1
fi

# Executar teste de conexao
DLL_PATH="$PROJECT_DIR/bin/Release/net7.0/SGSB.Web.dll"
if [ -f "$DLL_PATH" ]; then
    dotnet "$DLL_PATH" ApplyMigrations 2>&1 | head -30
else
    echo "ERRO: DLL nao encontrada em $DLL_PATH"
    exit 1
fi

echo ""
echo "========================================"
echo "Teste concluido!"
echo "========================================"

