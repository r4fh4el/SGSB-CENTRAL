#!/bin/bash
# Script para corrigir o arquivo runtimeconfig.json

echo "========================================"
echo "Corrigindo WebAPI.runtimeconfig.json"
echo "========================================"
echo ""

# Verificar se o arquivo existe
if [ ! -f "/var/www/sgsb/publish/WebAPI.runtimeconfig.json" ]; then
    echo "Arquivo não existe. Criando..."
else
    echo "Arquivo existe. Fazendo backup..."
    cp /var/www/sgsb/publish/WebAPI.runtimeconfig.json /var/www/sgsb/publish/WebAPI.runtimeconfig.json.bak
fi

# Criar arquivo correto com framework especificado
cat > /var/www/sgsb/publish/WebAPI.runtimeconfig.json << 'EOF'
{
  "runtimeOptions": {
    "tfm": "net7.0",
    "framework": {
      "name": "Microsoft.AspNetCore.App",
      "version": "7.0.0"
    },
    "configProperties": {
      "System.Globalization.Invariant": true
    }
  }
}
EOF

echo "✓ Arquivo atualizado!"
echo ""
echo "Conteúdo do arquivo:"
cat /var/www/sgsb/publish/WebAPI.runtimeconfig.json

echo ""
echo "Reiniciando serviço..."
systemctl restart sgsb-api.service

sleep 5
echo ""
echo "Status do serviço:"
systemctl status sgsb-api.service --no-pager -l

