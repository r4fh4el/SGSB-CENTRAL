# Script para fazer deploy do sistema SGSB no servidor
# Sistema de Gestao de Seguranca de Barragem (SGSB)

param(
    [string]$ServerIP = "72.60.57.220",
    [string]$User = "root",
    [string]$Password = "B4b4el123#123#",
    [string]$GitHubRepo = "https://github.com/r4fh4el/SGSB-CENTRAL.git",
    [string]$ProjectPath = "/var/www/sgsb",
    [string]$Port = "5000"
)

$ErrorActionPreference = "Stop"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Deploy do Sistema SGSB" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

Write-Host "Servidor: $ServerIP" -ForegroundColor Yellow
Write-Host "Usuario: $User" -ForegroundColor Yellow
Write-Host "Repositorio: $GitHubRepo" -ForegroundColor Yellow
Write-Host ""

# Comandos para executar no servidor
$commands = @"
# Atualizar sistema
apt-get update -y

# Instalar dependencias necessarias
apt-get install -y git curl wget

# Instalar .NET SDK 7.0 ou superior
if ! command -v dotnet &> /dev/null; then
    echo "Instalando .NET SDK..."
    wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
    chmod +x dotnet-install.sh
    ./dotnet-install.sh --channel 7.0
    export PATH=`$PATH:`$HOME/.dotnet:`$HOME/.dotnet/tools
    echo 'export PATH=`$PATH:`$HOME/.dotnet:`$HOME/.dotnet/tools' >> ~/.bashrc
fi

# Criar diretorio do projeto
mkdir -p $ProjectPath
cd $ProjectPath

# Clonar ou atualizar repositorio
if [ -d ".git" ]; then
    echo "Atualizando repositorio existente..."
    git pull origin main
else
    echo "Clonando repositorio..."
    git clone $GitHubRepo .
fi

# Navegar para o diretorio do projeto
cd SIstemaGestaoSegurancaBarragem-master

# Restaurar dependencias
echo "Restaurando dependencias do .NET..."
dotnet restore

# Compilar o projeto WebAPI
echo "Compilando projeto WebAPI..."
cd WebAPI
dotnet build --configuration Release

# Publicar o projeto
echo "Publicando projeto..."
dotnet publish --configuration Release --output /var/www/sgsb/publish

# Criar arquivo de servico systemd
echo "Criando servico systemd..."
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
Environment=ASPNETCORE_URLS=http://0.0.0.0:$Port

[Install]
WantedBy=multi-user.target
EOF

# Recarregar systemd
systemctl daemon-reload

# Habilitar servico
systemctl enable sgsb-api.service

# Iniciar servico
echo "Iniciando servico..."
systemctl start sgsb-api.service

# Verificar status
echo "Verificando status do servico..."
systemctl status sgsb-api.service --no-pager

echo ""
echo "========================================"
echo "Deploy concluido!"
echo "========================================"
echo "API rodando em: http://$ServerIP`:$Port"
echo "Swagger disponivel em: http://$ServerIP`:$Port/swagger"
echo ""
"@

Write-Host "[1/3] Preparando comandos para execucao no servidor..." -ForegroundColor Yellow
Write-Host ""

Write-Host "[2/3] Comandos que serao executados:" -ForegroundColor Yellow
Write-Host "----------------------------------------" -ForegroundColor Gray
Write-Host $commands -ForegroundColor Gray
Write-Host "----------------------------------------" -ForegroundColor Gray
Write-Host ""

Write-Host "[3/3] Para executar no servidor, use um dos metodos abaixo:" -ForegroundColor Yellow
Write-Host ""
Write-Host "OPCAO 1 - Conectar manualmente via SSH:" -ForegroundColor Cyan
Write-Host "  ssh root@$ServerIP" -ForegroundColor White
Write-Host "  (Digite a senha quando solicitado: $Password)" -ForegroundColor Gray
Write-Host "  (Pressione Enter se aparecer prompt de dominio)" -ForegroundColor Gray
Write-Host ""
Write-Host "OPCAO 2 - Executar comandos via SSH (copie e cole no terminal SSH):" -ForegroundColor Cyan
Write-Host $commands -ForegroundColor White
Write-Host ""
Write-Host "OPCAO 3 - Salvar comandos em arquivo e executar:" -ForegroundColor Cyan
Write-Host "  Os comandos foram salvos acima. Copie e cole no servidor SSH." -ForegroundColor Gray
Write-Host ""

# Salvar comandos em arquivo
$commands | Out-File -FilePath "deploy_commands.sh" -Encoding UTF8
Write-Host "Comandos salvos em: deploy_commands.sh" -ForegroundColor Green
Write-Host "  Voce pode fazer upload deste arquivo para o servidor e executar:" -ForegroundColor Gray
Write-Host "  chmod +x deploy_commands.sh && ./deploy_commands.sh" -ForegroundColor White
Write-Host ""

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Apos o deploy, acesse:" -ForegroundColor Yellow
Write-Host "  http://$ServerIP`:$Port/swagger" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

