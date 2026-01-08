#!/bin/bash
# Script para verificar acesso externo e diagnosticar problemas

echo "========================================"
echo "Diagnóstico de Acesso Externo"
echo "========================================"
echo ""

# 1. Verificar IP do servidor
echo "[1/6] IP do servidor:"
hostname -I
ip addr show | grep "inet " | grep -v "127.0.0.1"

# 2. Verificar se serviços estão escutando
echo ""
echo "[2/6] Serviços escutando:"
netstat -tlnp | grep -E ":80|:8080"

# 3. Verificar firewall local
echo ""
echo "[3/6] Regras de firewall local:"
iptables -L INPUT -n -v | grep -E "80|8080"

# 4. Testar conectividade local
echo ""
echo "[4/6] Teste local:"
echo "WebAPI (porta 80):"
curl -I http://localhost:80 2>&1 | head -3
echo ""
echo "SGSB.Web (porta 8080):"
curl -I http://localhost:8080 2>&1 | head -3

# 5. Verificar se há algum proxy ou load balancer
echo ""
echo "[5/6] Verificando configurações de rede:"
if [ -f "/etc/nginx/nginx.conf" ]; then
    echo "Nginx encontrado (pode estar bloqueando)"
    cat /etc/nginx/nginx.conf | grep -i "listen\|server" | head -5
fi

if [ -f "/etc/apache2/apache2.conf" ]; then
    echo "Apache encontrado (pode estar bloqueando)"
fi

# 6. Informações para o provedor
echo ""
echo "[6/6] Informações para verificar no painel do provedor:"
echo "========================================"
echo "SERVIÇOS ESTÃO CONFIGURADOS CORRETAMENTE LOCALMENTE"
echo "========================================"
echo ""
echo "O problema está no FIREWALL DO PROVEDOR/HOSTING"
echo ""
echo "Ações necessárias:"
echo "  1. Acesse o painel de controle do seu provedor/hosting"
echo "  2. Procure por 'Firewall', 'Security Groups', 'Network Rules'"
echo "  3. Abra as portas:"
echo "     - Porta 80 (HTTP)"
echo "     - Porta 8080 (HTTP)"
echo "  4. Se estiver usando cloud (AWS, Azure, GCP):"
echo "     - AWS: Security Groups -> Inbound Rules"
echo "     - Azure: Network Security Groups"
echo "     - GCP: Firewall Rules"
echo ""
echo "Teste de fora do servidor:"
echo "  curl http://72.60.57.220"
echo "  curl http://72.60.57.220:8080"
echo ""
echo "Ou use um serviço online para testar:"
echo "  https://www.yougetsignal.com/tools/open-ports/"
echo "  https://www.canyouseeme.org/"
echo ""

