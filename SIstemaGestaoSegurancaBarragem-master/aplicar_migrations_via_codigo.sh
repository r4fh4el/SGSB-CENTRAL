#!/bin/bash

echo "========================================"
echo "Aplicar Migrations via Codigo C#"
echo "========================================"

# Configurar variaveis de ambiente
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
export LC_ALL=C
export LANG=C
export LANGUAGE=C
export LC_CTYPE=C
export LC_NUMERIC=C
export LC_TIME=C
export LC_COLLATE=C
export LC_MONETARY=C
export LC_MESSAGES=C

# Diretorio do projeto
PROJECT_DIR="/var/www/sgsb/SIstemaGestaoSegurancaBarragem-master/SGSB.Web"
cd "$PROJECT_DIR"

# 1. Verificar se o arquivo ApplyMigrations.cs existe
echo "[1/4] Verificando arquivo ApplyMigrations.cs..."
echo "----------------------------------------"
if [ ! -f "ApplyMigrations.cs" ]; then
    echo "ERRO: Arquivo ApplyMigrations.cs nao encontrado"
    echo "Execute git pull para atualizar o repositorio"
    exit 1
fi
echo "Arquivo encontrado"
echo ""

# 2. Compilar o projeto
echo "[2/4] Compilando projeto..."
echo "----------------------------------------"
dotnet build --configuration Release
if [ $? -ne 0 ]; then
    echo "ERRO: Falha na compilacao"
    exit 1
fi
echo ""

# 3. Executar o programa de migrations
echo "[3/4] Executando aplicacao de migrations..."
echo "----------------------------------------"
# Usar o DLL compilado diretamente
DLL_PATH="$PROJECT_DIR/bin/Release/net7.0/SGSB.Web.dll"
if [ -f "$DLL_PATH" ]; then
    dotnet "$DLL_PATH" ApplyMigrations
else
    dotnet run --configuration Release --project SGSB.Web.csproj -- ApplyMigrations
fi

if [ $? -ne 0 ]; then
    echo "ERRO: Falha ao executar migrations"
    exit 1
fi
echo ""

# 4. Verificar resultado
echo "[4/4] Verificando resultado..."
echo "----------------------------------------"
echo "SUCESSO: Processo concluido!"
echo ""

echo "========================================"
echo "Migrations aplicadas com sucesso!"
echo "========================================"

