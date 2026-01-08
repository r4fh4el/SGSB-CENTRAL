# Guia de Deploy do Sistema SGSB no Servidor

## Informações do Servidor
- **IP:** 72.60.57.220
- **Usuário:** root
- **Senha:** B4b4el123#123#
- **Repositório:** https://github.com/r4fh4el/SGSB-CENTRAL.git

## Passo a Passo

### 1. Conectar ao Servidor via SSH

```bash
ssh root@72.60.57.220
```

**Importante:** Quando conectar, se aparecer um prompt pedindo domínio, pressione **Enter**.

Digite a senha quando solicitado: `B4b4el123#123#`

### 2. Executar o Script de Deploy

Você tem duas opções:

#### Opção A: Executar comandos diretamente

Copie e cole os comandos abaixo no terminal SSH:

```bash
# Atualizar sistema
apt-get update -y

# Instalar dependências
apt-get install -y git curl wget

# Instalar .NET SDK 7.0 (se não estiver instalado)
if ! command -v dotnet &> /dev/null; then
    wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
    chmod +x dotnet-install.sh
    ./dotnet-install.sh --channel 7.0
    export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
    echo 'export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools' >> ~/.bashrc
    source ~/.bashrc
fi

# Criar diretório do projeto
mkdir -p /var/www/sgsb
cd /var/www/sgsb

# Clonar repositório
git clone https://github.com/r4fh4el/SGSB-CENTRAL.git .

# Navegar para o diretório do projeto
cd SIstemaGestaoSegurancaBarragem-master

# Restaurar dependências
dotnet restore

# Compilar e publicar WebAPI
cd WebAPI
dotnet build --configuration Release
dotnet publish --configuration Release --output /var/www/sgsb/publish

# Criar serviço systemd
cat > /etc/systemd/system/sgsb-api.service << 'EOF'
[Unit]
Description=SGSB Web API
After=network.target

[Service]
Type=notify
ExecStart=/usr/bin/dotnet /var/www/sgsb/publish/WebAPI.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=sgsb-api
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://0.0.0.0:5000

[Install]
WantedBy=multi-user.target
EOF

# Configurar e iniciar serviço
systemctl daemon-reload
systemctl enable sgsb-api.service
systemctl start sgsb-api.service

# Verificar status
systemctl status sgsb-api.service
```

#### Opção B: Usar o script deploy_commands.sh

1. Faça upload do arquivo `deploy_commands.sh` para o servidor
2. Execute:
```bash
chmod +x deploy_commands.sh
./deploy_commands.sh
```

### 3. Verificar se está rodando

```bash
# Ver status do serviço
systemctl status sgsb-api.service

# Ver logs em tempo real
journalctl -u sgsb-api.service -f

# Verificar se a porta está aberta
netstat -tlnp | grep 5000
```

### 4. Acessar o Sistema

Após o deploy, o sistema estará disponível em:

- **API:** http://72.60.57.220:5000
- **Swagger:** http://72.60.57.220:5000/swagger

### 5. Comandos Úteis

```bash
# Parar o serviço
systemctl stop sgsb-api.service

# Iniciar o serviço
systemctl start sgsb-api.service

# Reiniciar o serviço
systemctl restart sgsb-api.service

# Ver logs
journalctl -u sgsb-api.service -n 50

# Atualizar código (após fazer push no GitHub)
cd /var/www/sgsb/SIstemaGestaoSegurancaBarragem-master
git pull origin main
cd WebAPI
dotnet publish --configuration Release --output /var/www/sgsb/publish
systemctl restart sgsb-api.service
```

### 6. Configurar Firewall (se necessário)

```bash
# Permitir porta 5000
ufw allow 5000/tcp
# ou
iptables -A INPUT -p tcp --dport 5000 -j ACCEPT
```

## Troubleshooting

### Erro: .NET não encontrado
```bash
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools
source ~/.bashrc
```

### Erro: Porta já em uso
```bash
# Verificar o que está usando a porta
lsof -i :5000
# ou
netstat -tlnp | grep 5000
```

### Verificar erros do serviço
```bash
journalctl -u sgsb-api.service -n 100 --no-pager
```

### Reinstalar serviço
```bash
systemctl stop sgsb-api.service
systemctl disable sgsb-api.service
rm /etc/systemd/system/sgsb-api.service
systemctl daemon-reload
# Depois execute novamente os comandos de criação do serviço
```

