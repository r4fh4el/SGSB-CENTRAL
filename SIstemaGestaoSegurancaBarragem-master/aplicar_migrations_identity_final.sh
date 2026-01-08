#!/bin/bash

echo "========================================"
echo "Aplicar Migrations do Identity - Versao Final"
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
export LC_PAPER=C
export LC_NAME=C
export LC_ADDRESS=C
export LC_TELEPHONE=C
export LC_MEASUREMENT=C
export LC_IDENTIFICATION=C

# Diretorio do projeto
PROJECT_DIR="/var/www/sgsb/SIstemaGestaoSegurancaBarragem-master/SGSB.Web"
cd "$PROJECT_DIR"

# 1. Verificar dotnet-ef instalado globalmente
echo "[1/6] Verificando dotnet-ef instalado globalmente..."
echo "----------------------------------------"
dotnet tool list --global | grep dotnet-ef
if [ $? -ne 0 ]; then
    echo "ERRO: dotnet-ef nao esta instalado globalmente"
    echo "Execute: cd /var/www/sgsb/SIstemaGestaoSegurancaBarragem-master && ./instalar_dotnet_ef_net7.sh"
    exit 1
fi
echo ""

# 2. Verificar se dotnet ef funciona
echo "[2/6] Testando comando dotnet ef..."
echo "----------------------------------------"
# Tentar executar diretamente
$HOME/.dotnet/tools/dotnet-ef --version 2>&1
if [ $? -eq 0 ]; then
    echo "dotnet-ef encontrado em $HOME/.dotnet/tools/dotnet-ef"
    # Criar alias temporario se necessario
    alias dotnet-ef="$HOME/.dotnet/tools/dotnet-ef"
fi

# Tentar via dotnet ef
dotnet ef --version 2>&1
if [ $? -ne 0 ]; then
    echo "AVISO: dotnet ef nao funciona diretamente, usando caminho completo"
    DOTNET_EF_CMD="$HOME/.dotnet/tools/dotnet-ef"
else
    DOTNET_EF_CMD="dotnet ef"
fi
echo ""

# 3. Criar ou verificar arquivo de ferramentas locais
echo "[3/6] Configurando ferramentas locais..."
echo "----------------------------------------"
if [ ! -d ".config" ]; then
    mkdir -p .config
fi

# Criar dotnet-tools.json se nao existir
if [ ! -f ".config/dotnet-tools.json" ]; then
    cat > .config/dotnet-tools.json << 'EOF'
{
  "version": 1,
  "isRoot": true,
  "tools": {
    "dotnet-ef": {
      "version": "7.0.20",
      "commands": [
        "dotnet-ef"
      ]
    }
  }
}
EOF
    echo "Arquivo .config/dotnet-tools.json criado"
fi

# Restaurar ferramentas locais
dotnet tool restore
echo ""

# 4. Verificar migrations existentes
echo "[4/6] Verificando migrations existentes..."
echo "----------------------------------------"
if [ -d "Data/Migrations" ]; then
    echo "Migrations encontradas em Data/Migrations:"
    ls -la Data/Migrations/ | head -10
else
    echo "Diretorio Data/Migrations nao encontrado"
    echo "Criando diretorio..."
    mkdir -p Data/Migrations
fi
echo ""

# 5. Aplicar ou criar migrations
echo "[5/6] Aplicando migrations do Identity..."
echo "----------------------------------------"

# Usar caminho completo do dotnet-ef com todas as variaveis de ambiente
DOTNET_EF_PATH="$HOME/.dotnet/tools/dotnet-ef"

# Criar funcao wrapper que garante cultura invariante
run_dotnet_ef() {
    local cmd="$1"
    shift
    # Forcar todas as variaveis de cultura antes de executar
    env LC_ALL=C LANG=C LANGUAGE=C LC_CTYPE=C LC_NUMERIC=C LC_TIME=C LC_COLLATE=C LC_MONETARY=C LC_MESSAGES=C DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true "$DOTNET_EF_PATH" "$cmd" "$@"
}

# Tentar listar migrations primeiro
echo "Verificando migrations existentes..."
run_dotnet_ef migrations list --project "$PROJECT_DIR/SGSB.Web.csproj" --startup-project "$PROJECT_DIR/SGSB.Web.csproj" --context ApplicationDbContext 2>&1 | head -20
MIGRATIONS_EXIST=$?

if [ $MIGRATIONS_EXIST -eq 0 ]; then
    echo "Migrations encontradas, aplicando ao banco..."
    run_dotnet_ef database update --project "$PROJECT_DIR/SGSB.Web.csproj" --startup-project "$PROJECT_DIR/SGSB.Web.csproj" --context ApplicationDbContext
    UPDATE_RESULT=$?
else
    echo "Nenhuma migration encontrada, criando migrations iniciais..."
    run_dotnet_ef migrations add CreateIdentitySchema --project "$PROJECT_DIR/SGSB.Web.csproj" --startup-project "$PROJECT_DIR/SGSB.Web.csproj" --context ApplicationDbContext
    if [ $? -eq 0 ]; then
        echo "Migration criada, aplicando ao banco..."
        run_dotnet_ef database update --project "$PROJECT_DIR/SGSB.Web.csproj" --startup-project "$PROJECT_DIR/SGSB.Web.csproj" --context ApplicationDbContext
        UPDATE_RESULT=$?
    else
        UPDATE_RESULT=1
    fi
fi
echo ""

# 6. Verificar resultado
echo "[6/6] Verificando resultado..."
echo "----------------------------------------"
if [ ${UPDATE_RESULT:-1} -eq 0 ]; then
    echo "SUCESSO: Migrations aplicadas com sucesso!"
else
    echo "AVISO: Pode ter havido erros. Verifique os logs acima."
    echo ""
    echo "Se o erro persistir, tente executar manualmente:"
    echo "  cd $PROJECT_DIR"
    echo "  env LC_ALL=C LANG=C DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true $DOTNET_EF_PATH database update --project SGSB.Web.csproj --startup-project SGSB.Web.csproj --context ApplicationDbContext"
fi
echo ""

echo "========================================"
echo "Processo concluido!"
echo "========================================"

