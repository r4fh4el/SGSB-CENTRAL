# Script para criar e testar usuario
param(
    [string]$ApiUrl = "http://localhost:5204"
)

$ErrorActionPreference = "Continue"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Teste de Criacao e Autenticacao de Usuario" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Dados do usuario
$userData = @{
    nome = "Maria Santos"
    login = "maria.santos"
    email = "maria.santos@teste.com"
    senha = "Senha@123456"
    celular = "(11) 98888-7777"
    idade = 28
} | ConvertTo-Json

Write-Host "Dados do novo usuario:" -ForegroundColor Yellow
Write-Host "  Nome: Maria Santos" -ForegroundColor Gray
Write-Host "  Login: maria.santos" -ForegroundColor Gray
Write-Host "  Email: maria.santos@teste.com" -ForegroundColor Gray
Write-Host ""

# Passo 1: Verificar se API esta rodando
Write-Host "[1/4] Verificando se a API esta acessivel..." -ForegroundColor Yellow
try {
    $response = Invoke-WebRequest -Uri "$ApiUrl/swagger" -Method Get -TimeoutSec 5 -ErrorAction Stop
    Write-Host "  OK API esta acessivel" -ForegroundColor Green
} catch {
    Write-Host "  ERRO API nao esta acessivel em $ApiUrl" -ForegroundColor Red
    Write-Host "  Inicie a API primeiro: cd WebAPI; dotnet run" -ForegroundColor Yellow
    exit 1
}

# Passo 2: Criar usuario
Write-Host ""
Write-Host "[2/4] Criando usuario via Identity..." -ForegroundColor Yellow
try {
    $createResponse = Invoke-RestMethod -Uri "$ApiUrl/API/AdicionarUsuarioIdentity" -Method Post -Body $userData -ContentType "application/json" -ErrorAction Stop
    Write-Host "  OK Usuario criado: $createResponse" -ForegroundColor Green
} catch {
    Write-Host "  ERRO ao criar usuario: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

Start-Sleep -Seconds 2

# Passo 3: Listar usuarios
Write-Host ""
Write-Host "[3/4] Verificando usuarios cadastrados..." -ForegroundColor Yellow
try {
    $users = Invoke-RestMethod -Uri "$ApiUrl/API/BuscarUsuarios" -Method Get -ErrorAction Stop
    $foundUser = $users | Where-Object { $_.email -eq "maria.santos@teste.com" -or $_.Email -eq "maria.santos@teste.com" }
    if ($foundUser) {
        Write-Host "  OK Usuario encontrado!" -ForegroundColor Green
        Write-Host "    Total de usuarios: $($users.Count)" -ForegroundColor Gray
    } else {
        Write-Host "  AVISO Usuario nao encontrado na listagem" -ForegroundColor Yellow
    }
} catch {
    Write-Host "  AVISO Erro ao listar usuarios: $($_.Exception.Message)" -ForegroundColor Yellow
}

# Passo 4: Testar login
Write-Host ""
Write-Host "[4/4] Testando login e obtencao de token JWT..." -ForegroundColor Yellow
try {
    $loginData = @{
        email = "maria.santos@teste.com"
        senha = "Senha@123456"
    } | ConvertTo-Json
    
    $token = Invoke-RestMethod -Uri "$ApiUrl/API/CriarTokenIdentity" -Method Post -Body $loginData -ContentType "application/json" -ErrorAction Stop
    
    if ($token) {
        Write-Host "  OK Login bem-sucedido!" -ForegroundColor Green
        Write-Host "    Token: $($token.Substring(0, [Math]::Min(50, $token.Length)))..." -ForegroundColor Gray
        $global:JwtToken = $token
    }
} catch {
    Write-Host "  ERRO Login falhou: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "    Verifique se o usuario foi criado corretamente" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Teste concluido!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

if ($global:JwtToken) {
    Write-Host "Resumo:" -ForegroundColor Yellow
    Write-Host "  OK Usuario criado: maria.santos@teste.com" -ForegroundColor Green
    Write-Host "  OK Login testado com sucesso" -ForegroundColor Green
    Write-Host "  OK Token JWT obtido" -ForegroundColor Green
    Write-Host ""
    Write-Host "Acesse o Swagger para mais testes: $ApiUrl/swagger" -ForegroundColor Cyan
} else {
    Write-Host "AVISO: Usuario pode ter sido criado, mas login falhou" -ForegroundColor Yellow
    Write-Host "Verifique manualmente no Swagger: $ApiUrl/swagger" -ForegroundColor Yellow
}

Write-Host ""

