# Comandos Completos de Deploy - WebAPI e SGSB.Web

## PASSO 1: Atualizar Repositório Local (Windows)

```powershell
# Navegar para o diretório do projeto
cd E:\SGSB\SIstemaGestaoSegurancaBarragem-master

# Puxar últimas alterações do GitHub
git pull origin main

# Verificar se está atualizado
git status
```

## PASSO 2: Fazer Push (se houver alterações locais)

```powershell
# Se você fez alterações locais, fazer commit e push
git add .
git commit -m "Suas alterações"
git push origin main
```

## PASSO 3: Deploy no Servidor SSH

Conecte ao servidor SSH e execute:

```bash
# ========================================
# CONFIGURAR VARIÁVEIS
# ========================================
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true

# ========================================
# ATUALIZAR CÓDIGO NO SERVIDOR
# ========================================
cd /var/www/sgsb
git pull origin main

# ========================================
# 1. WEBAPI (Porta 80)
# ========================================
cd SIstemaGestaoSegurancaBarragem-master/WebAPI

# Build e Publish
dotnet restore
dotnet build --configuration Release
dotnet publish --configuration Release --output /var/www/sgsb/publish

# Configurar runtimeconfig.json
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

# Configurar serviço WebAPI
cat > /etc/systemd/system/sgsb-api.service << 'EOF'
[Unit]
Description=SGSB Web API
After=network.target
[Service]
Type=simple
ExecStart=/root/.dotnet/dotnet /var/www/sgsb/publish/WebAPI.dll
Restart=always
RestartSec=10
User=root
WorkingDirectory=/var/www/sgsb/publish
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://0.0.0.0:80
Environment="PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin:/root/.dotnet:/root/.dotnet/tools"
Environment="DOTNET_ROOT=/root/.dotnet"
Environment="DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true"
[Install]
WantedBy=multi-user.target
EOF

systemctl daemon-reload
systemctl enable sgsb-api.service
systemctl restart sgsb-api.service

# ========================================
# 2. SGSB.WEB (Porta 8080)
# ========================================
cd /var/www/sgsb/SIstemaGestaoSegurancaBarragem-master/SGSB.Web

# Build e Publish
dotnet restore
dotnet build --configuration Release
dotnet publish --configuration Release --output /var/www/sgsb/publish-web

# Configurar runtimeconfig.json
cat > /var/www/sgsb/publish-web/SGSB.Web.runtimeconfig.json << 'EOF'
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

# Liberar porta 8080
netstat -tlnp | grep :8080 | awk '{print $7}' | cut -d'/' -f1 | xargs kill -9 2>/dev/null || true

# Configurar serviço SGSB.Web
cat > /etc/systemd/system/sgsb-web.service << 'EOF'
[Unit]
Description=SGSB Web Application
After=network.target
[Service]
Type=simple
ExecStart=/root/.dotnet/dotnet /var/www/sgsb/publish-web/SGSB.Web.dll
Restart=always
RestartSec=10
User=root
WorkingDirectory=/var/www/sgsb/publish-web
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://0.0.0.0:8080
Environment="PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin:/root/.dotnet:/root/.dotnet/tools"
Environment="DOTNET_ROOT=/root/.dotnet"
Environment="DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true"
[Install]
WantedBy=multi-user.target
EOF

# Abrir firewall
ufw allow 8080/tcp 2>/dev/null || iptables -A INPUT -p tcp --dport 8080 -j ACCEPT 2>/dev/null || true

# Iniciar SGSB.Web
systemctl daemon-reload
systemctl enable sgsb-web.service
systemctl restart sgsb-web.service

# ========================================
# VERIFICAR STATUS
# ========================================
echo "Status WebAPI:"
systemctl status sgsb-api.service --no-pager -l | head -10

echo ""
echo "Status SGSB.Web:"
systemctl status sgsb-web.service --no-pager -l | head -10

echo ""
echo "Portas escutando:"
netstat -tlnp | grep -E ":80|:8080"
```

## URLs de Acesso

- **WebAPI Swagger:** http://72.60.57.220/swagger
- **SGSB.Web:** http://72.60.57.220:8080

## Comandos Úteis

```bash
# Ver logs WebAPI
journalctl -u sgsb-api.service -f

# Ver logs SGSB.Web
journalctl -u sgsb-web.service -f

# Reiniciar serviços
systemctl restart sgsb-api.service
systemctl restart sgsb-web.service

# Parar serviços
systemctl stop sgsb-api.service
systemctl stop sgsb-web.service
```

