#!/bin/bash
# Script para verificar possíveis erros no serviço

echo "========================================"
echo "Verificando Possíveis Erros"
echo "========================================"
echo ""

# 1. Verificar se serviço está rodando
echo "[1/5] Status do serviço:"
systemctl is-active sgsb-api.service && echo "✓ Serviço está ativo" || echo "✗ Serviço não está ativo"

# 2. Verificar logs de erro
echo ""
echo "[2/5] Últimos erros nos logs:"
journalctl -u sgsb-api.service -n 100 --no-pager | grep -i "error\|exception\|fail\|fatal" | tail -20 || echo "Nenhum erro encontrado"

# 3. Verificar se porta está escutando
echo ""
echo "[3/5] Verificando portas:"
echo "Porta 80:"
netstat -tlnp | grep :80 || lsof -i:80 || echo "Nenhum processo na porta 80"
echo ""
echo "Porta 5000:"
netstat -tlnp | grep :5000 || lsof -i:5000 || echo "Nenhum processo na porta 5000"

# 4. Verificar arquivos necessários
echo ""
echo "[4/5] Verificando arquivos:"
[ -f "/var/www/sgsb/publish/WebAPI.dll" ] && echo "✓ WebAPI.dll existe" || echo "✗ WebAPI.dll NÃO existe"
[ -f "/var/www/sgsb/publish/appsettings.json" ] && echo "✓ appsettings.json existe" || echo "✗ appsettings.json NÃO existe"
[ -f "/var/www/sgsb/publish/WebAPI.runtimeconfig.json" ] && echo "✓ runtimeconfig.json existe" || echo "✗ runtimeconfig.json NÃO existe"

# 5. Testar conexão local
echo ""
echo "[5/5] Testando conexão HTTP:"
echo "Testando porta 80:"
curl -s -o /dev/null -w "HTTP Status: %{http_code}\n" --connect-timeout 5 http://localhost:80 || echo "✗ Não conseguiu conectar na porta 80"
echo ""
echo "Testando porta 5000:"
curl -s -o /dev/null -w "HTTP Status: %{http_code}\n" --connect-timeout 5 http://localhost:5000 || echo "✗ Não conseguiu conectar na porta 5000"

echo ""
echo "========================================"
echo "Verificação concluída!"
echo "========================================"
echo ""
echo "Para ver logs completos:"
echo "  journalctl -u sgsb-api.service -f"
echo ""

