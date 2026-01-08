#!/bin/bash
# Script para corrigir problema de cultura e atualizar IPs hardcoded

echo "========================================"
echo "Corrigindo Cultura e IPs Hardcoded"
echo "========================================"
echo ""

SERVER_IP="72.60.57.220"
PROJECT_PATH="/var/www/sgsb/SIstemaGestaoSegurancaBarragem-master"

# 1. Corrigir problema de cultura - atualizar Program.cs ou Startup.cs
echo "[1/3] Corrigindo problema de cultura..."

# Procurar arquivos Program.cs e Startup.cs
find $PROJECT_PATH -name "Program.cs" -o -name "Startup.cs" | while read file; do
    echo "Atualizando $file..."
    
    # Adicionar configuração para evitar erro de cultura
    if grep -q "CultureInfo\|Culture" "$file"; then
        # Se já tem configuração de cultura, comentar ou remover
        sed -i 's/CultureInfo\.GetCultureInfo/CultureInfo\.InvariantCulture/g' "$file" 2>/dev/null || true
        sed -i 's/"en-us"/CultureInfo.InvariantCulture.Name/g' "$file" 2>/dev/null || true
    fi
done

# 2. Procurar e atualizar IPs hardcoded
echo ""
echo "[2/3] Procurando e atualizando IPs hardcoded..."

# Padrões comuns de IP/localhost para substituir
PATTERNS=(
    "localhost"
    "127.0.0.1"
    "http://localhost"
    "https://localhost"
    "http://127.0.0.1"
    "https://127.0.0.1"
)

# Arquivos para verificar
FILES_TO_CHECK=(
    "$PROJECT_PATH/WebAPI/appsettings.json"
    "$PROJECT_PATH/WebAPI/appsettings.Development.json"
    "$PROJECT_PATH/SGSB.Web/appsettings.json"
    "$PROJECT_PATH/SGSB.Web/appsettings.Development.json"
    "$PROJECT_PATH/Infraestrutura/Configuracoes/Constantes.cs"
    "$PROJECT_PATH/SGSB.Web/Infra/Constantes.cs"
)

for file in "${FILES_TO_CHECK[@]}"; do
    if [ -f "$file" ]; then
        echo "Verificando $file..."
        # Substituir localhost por IP do servidor
        sed -i "s/localhost:5000/$SERVER_IP/g" "$file" 2>/dev/null || true
        sed -i "s/localhost:5001/$SERVER_IP/g" "$file" 2>/dev/null || true
        sed -i "s/localhost:8080/$SERVER_IP:8080/g" "$file" 2>/dev/null || true
        sed -i "s|http://localhost|http://$SERVER_IP|g" "$file" 2>/dev/null || true
        sed -i "s|https://localhost|https://$SERVER_IP|g" "$file" 2>/dev/null || true
        sed -i "s/127.0.0.1/$SERVER_IP/g" "$file" 2>/dev/null || true
    fi
done

# 3. Procurar em arquivos .cs por constantes com IP/localhost
echo ""
echo "[3/3] Procurando em arquivos .cs..."

find $PROJECT_PATH -name "*.cs" -type f | while read file; do
    if grep -qE "localhost|127\.0\.0\.1|http://.*:5000|http://.*:5001|http://.*:8080" "$file" 2>/dev/null; then
        echo "Atualizando $file..."
        sed -i "s/localhost:5000/$SERVER_IP/g" "$file" 2>/dev/null || true
        sed -i "s/localhost:5001/$SERVER_IP/g" "$file" 2>/dev/null || true
        sed -i "s/localhost:8080/$SERVER_IP:8080/g" "$file" 2>/dev/null || true
        sed -i "s|http://localhost|http://$SERVER_IP|g" "$file" 2>/dev/null || true
        sed -i "s|https://localhost|https://$SERVER_IP|g" "$file" 2>/dev/null || true
        sed -i "s/127\.0\.0\.1/$SERVER_IP/g" "$file" 2>/dev/null || true
    fi
done

echo ""
echo "========================================"
echo "Correções aplicadas!"
echo "========================================"
echo ""
echo "Agora você precisa:"
echo "  1. Rebuild e republish dos projetos"
echo "  2. Reiniciar os serviços"
echo ""
echo "Execute:"
echo "  cd $PROJECT_PATH && ./deploy_webapi_e_web.sh"
echo ""

