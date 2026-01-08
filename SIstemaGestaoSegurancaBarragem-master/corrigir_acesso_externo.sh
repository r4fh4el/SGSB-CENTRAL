#!/bin/bash
# Script para corrigir acesso externo aos serviços

echo "========================================"
echo "Corrigindo Acesso Externo"
echo "========================================"
echo ""

# 1. Verificar se serviços estão escutando em 0.0.0.0 (todas as interfaces)
echo "[1/5] Verificando interfaces de rede..."
echo "Porta 80 (WebAPI):"
netstat -tlnp | grep :80
echo ""
echo "Porta 8080 (SGSB.Web):"
netstat -tlnp | grep :8080

# 2. Verificar firewall
echo ""
echo "[2/5] Verificando firewall..."
if command -v ufw &> /dev/null; then
    echo "UFW Status:"
    ufw status
    echo ""
    echo "Abrindo portas no UFW..."
    ufw allow 80/tcp
    ufw allow 8080/tcp
    ufw reload
elif command -v iptables &> /dev/null; then
    echo "Configurando iptables..."
    iptables -A INPUT -p tcp --dport 80 -j ACCEPT
    iptables -A INPUT -p tcp --dport 8080 -j ACCEPT
    # Salvar regras (depende da distribuição)
    iptables-save > /etc/iptables/rules.v4 2>/dev/null || \
    service iptables save 2>/dev/null || \
    echo "Regras aplicadas (salve manualmente se necessário)"
fi

# 3. Verificar seiptables não está bloqueando
echo ""
echo "[3/5] Verificando regras iptables..."
iptables -L -n | grep -E "80|8080|ACCEPT" | head -10

# 4. Testar conectividade local
echo ""
echo "[4/5] Testando conectividade local..."
echo "Testando WebAPI (porta 80):"
curl -I http://localhost:80 2>&1 | head -3
echo ""
echo "Testando SGSB.Web (porta 8080):"
curl -I http://localhost:8080 2>&1 | head -3

# 5. Verificar logs dos serviços
echo ""
echo "[5/5] Verificando logs dos serviços..."
echo "Últimos logs WebAPI:"
journalctl -u sgsb-api.service -n 10 --no-pager | grep -i "listening\|error" || journalctl -u sgsb-api.service -n 5 --no-pager
echo ""
echo "Últimos logs SGSB.Web:"
journalctl -u sgsb-web.service -n 10 --no-pager | grep -i "listening\|error" || journalctl -u sgsb-web.service -n 5 --no-pager

echo ""
echo "========================================"
echo "Verificações concluídas!"
echo "========================================"
echo ""
echo "Se ainda não funcionar, verifique:"
echo "  1. Firewall do provedor/hosting (painel de controle)"
echo "  2. Se o servidor está atrás de um proxy/load balancer"
echo "  3. Configurações de segurança do grupo no cloud (AWS Security Groups, etc)"
echo ""
echo "Teste de fora do servidor:"
echo "  curl http://72.60.57.220"
echo "  curl http://72.60.57.220:8080"
echo ""

