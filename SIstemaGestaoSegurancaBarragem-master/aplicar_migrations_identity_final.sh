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

# Tentar listar migrations primeiro
if [ "$DOTNET_EF_CMD" = "dotnet ef" ]; then
    dotnet ef migrations list --context ApplicationDbContext 2>&1 | head -20
    MIGRATIONS_EXIST=$?
else
    $DOTNET_EF_CMD migrations list --project "$PROJECT_DIR/SGSB.Web.csproj" --startup-project "$PROJECT_DIR/SGSB.Web.csproj" --context ApplicationDbContext 2>&1 | head -20
    MIGRATIONS_EXIST=$?
fi

if [ $MIGRATIONS_EXIST -eq 0 ]; then
    echo "Migrations encontradas, aplicando ao banco..."
    if [ "$DOTNET_EF_CMD" = "dotnet ef" ]; then
        dotnet ef database update --context ApplicationDbContext
    else
        $DOTNET_EF_CMD database update --project "$PROJECT_DIR/SGSB.Web.csproj" --startup-project "$PROJECT_DIR/SGSB.Web.csproj" --context ApplicationDbContext
    fi
else
    echo "Nenhuma migration encontrada, criando migrations iniciais..."
    if [ "$DOTNET_EF_CMD" = "dotnet ef" ]; then
        dotnet ef migrations add CreateIdentitySchema --context ApplicationDbContext
        if [ $? -eq 0 ]; then
            dotnet ef database update --context ApplicationDbContext
        fi
    else
        $DOTNET_EF_CMD migrations add CreateIdentitySchema --project "$PROJECT_DIR/SGSB.Web.csproj" --startup-project "$PROJECT_DIR/SGSB.Web.csproj" --context ApplicationDbContext
        if [ $? -eq 0 ]; then
            $DOTNET_EF_CMD database update --project "$PROJECT_DIR/SGSB.Web.csproj" --startup-project "$PROJECT_DIR/SGSB.Web.csproj" --context ApplicationDbContext
        fi
    fi
fi
echo ""

# 6. Verificar resultado
echo "[6/6] Verificando resultado..."
echo "----------------------------------------"
if [ $? -eq 0 ]; then
    echo "SUCESSO: Migrations aplicadas com sucesso!"
else
    echo "AVISO: Pode ter havido erros. Verifique os logs acima."
fi
echo ""

echo "========================================"
echo "Processo concluido!"
echo "========================================"

