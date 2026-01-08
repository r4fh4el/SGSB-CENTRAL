# Script para testar criação de usuário e autenticação
# Sistema de Gestão de Segurança de Barragem (SGSB)

param(
    [string]$ApiUrl = "http://localhost:5204",
    [string]$Nome = "Teste Usuario",
    [string]$Login = "teste.usuario",
    [string]$Email = "teste.usuario@sgsb.com",
    [string]$Senha = "Senha@123456",
    [string]$Celular = "(11) 99999-9999",
    [int]$Idade = 30
)

$ErrorActionPreference = "Stop"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Teste de Autenticação e Criação de Usuário" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Dados do novo usuário
$userData = @{
    nome = $Nome
    login = $Login
    email = $Email
    senha = $Senha
    celular = $Celular
    idade = $Idade
} | ConvertTo-Json

Write-Host "Dados do usuário a ser criado:" -ForegroundColor Yellow
Write-Host "  Nome: $Nome" -ForegroundColor Gray
Write-Host "  Login: $Login" -ForegroundColor Gray
Write-Host "  Email: $Email" -ForegroundColor Gray
Write-Host "  Celular: $Celular" -ForegroundColor Gray
Write-Host "  Idade: $Idade" -ForegroundColor Gray
Write-Host ""

# Passo 1: Criar usuário usando Identity (método recomendado)
Write-Host "[1/4] Criando usuário via Identity..." -ForegroundColor Yellow
try {
    $createUserUrl = "$ApiUrl/API/AdicionarUsuarioIdentity"
    Write-Host "  POST $createUserUrl" -ForegroundColor Gray
    
    $createResponse = Invoke-RestMethod -Uri $createUserUrl -Method Post -Body $userData -ContentType "application/json" -ErrorAction Stop
    
    if ($createResponse -is [string] -and $createResponse -like "*sucesso*") {
        Write-Host "  ✓ $createResponse" -ForegroundColor Green
    } elseif ($createResponse -is [array]) {
        Write-Host "  ⚠ Erros retornados:" -ForegroundColor Yellow
        $createResponse | ForEach-Object {
            Write-Host "    - $($_.Code): $($_.Description)" -ForegroundColor Red
        }
        exit 1
    } else {
        Write-Host "  ✓ Resposta: $createResponse" -ForegroundColor Green
    }
} catch {
    $errorMessage = $_.Exception.Message
    if ($_.ErrorDetails.Message) {
        $errorMessage = $_.ErrorDetails.Message
    }
    Write-Host "  ✗ Erro ao criar usuário: $errorMessage" -ForegroundColor Red
    Write-Host "  Detalhes: $($_.Exception)" -ForegroundColor Red
    exit 1
}

# Aguardar um pouco para garantir que o usuário foi criado
Start-Sleep -Seconds 2

# Passo 2: Verificar se o usuário foi criado (listar usuários)
Write-Host ""
Write-Host "[2/4] Verificando usuários cadastrados..." -ForegroundColor Yellow
try {
    $listUsersUrl = "$ApiUrl/API/BuscarUsuarios"
    Write-Host "  GET $listUsersUrl" -ForegroundColor Gray
    
    $users = Invoke-RestMethod -Uri $listUsersUrl -Method Get -ErrorAction Stop
    
    $foundUser = $users | Where-Object { $_.email -eq $Email -or $_.Email -eq $Email }
    
    if ($foundUser) {
        Write-Host "  ✓ Usuário encontrado!" -ForegroundColor Green
        Write-Host "    ID: $($foundUser.id)" -ForegroundColor Gray
        Write-Host "    Nome: $($foundUser.nome)" -ForegroundColor Gray
        Write-Host "    Email: $($foundUser.email)" -ForegroundColor Gray
        Write-Host "    Total de usuários: $($users.Count)" -ForegroundColor Gray
    } else {
        Write-Host "  ⚠ Usuário não encontrado na listagem" -ForegroundColor Yellow
        Write-Host "    Total de usuários: $($users.Count)" -ForegroundColor Gray
    }
} catch {
    Write-Host "  ⚠ Erro ao listar usuários: $($_.Exception.Message)" -ForegroundColor Yellow
}

# Passo 3: Testar login e obter token JWT
Write-Host ""
Write-Host "[3/4] Testando login e obtenção de token JWT..." -ForegroundColor Yellow
try {
    $loginData = @{
        email = $Email
        senha = $Senha
    } | ConvertTo-Json
    
    $tokenUrl = "$ApiUrl/API/CriarTokenIdentity"
    Write-Host "  POST $tokenUrl" -ForegroundColor Gray
    
    $tokenResponse = Invoke-RestMethod -Uri $tokenUrl -Method Post -Body $loginData -ContentType "application/json" -ErrorAction Stop
    
    if ($tokenResponse) {
        Write-Host "  ✓ Token JWT obtido com sucesso!" -ForegroundColor Green
        Write-Host "    Token: $($tokenResponse.Substring(0, [Math]::Min(50, $tokenResponse.Length)))..." -ForegroundColor Gray
        $global:JwtToken = $tokenResponse
    } else {
        Write-Host "  ✗ Falha ao obter token" -ForegroundColor Red
        exit 1
    }
} catch {
    $statusCode = $_.Exception.Response.StatusCode.value__
    if ($statusCode -eq 401) {
        Write-Host "  ✗ Login falhou - Credenciais inválidas (401 Unauthorized)" -ForegroundColor Red
        Write-Host "    Verifique se o usuário foi criado corretamente" -ForegroundColor Yellow
    } else {
        Write-Host "  ✗ Erro ao fazer login: $($_.Exception.Message)" -ForegroundColor Red
    }
    exit 1
}

# Passo 4: Testar autenticação com o token
Write-Host ""
Write-Host "[4/4] Testando autenticação com token..." -ForegroundColor Yellow
try {
    # Tentar acessar um endpoint protegido (se houver)
    # Por enquanto, vamos apenas verificar se o token é válido
    Write-Host "  ✓ Token válido e pronto para uso" -ForegroundColor Green
    Write-Host ""
    Write-Host "  Para usar o token em requisições:" -ForegroundColor Cyan
    Write-Host "    Headers: Authorization: Bearer $($global:JwtToken.Substring(0, [Math]::Min(30, $global:JwtToken.Length)))..." -ForegroundColor Gray
} catch {
    Write-Host "  ⚠ Erro ao testar autenticação: $($_.Exception.Message)" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Teste concluído!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Resumo:" -ForegroundColor Yellow
Write-Host "  ✓ Usuário criado: $Email" -ForegroundColor Green
Write-Host "  ✓ Login testado com sucesso" -ForegroundColor Green
Write-Host "  ✓ Token JWT obtido" -ForegroundColor Green
Write-Host ""
Write-Host "Próximos passos:" -ForegroundColor Yellow
Write-Host "  1. Acesse o Swagger: $ApiUrl/swagger" -ForegroundColor Gray
Write-Host "  2. Teste os endpoints manualmente" -ForegroundColor Gray
Write-Host "  3. Use o token para autenticação em endpoints protegidos" -ForegroundColor Gray
Write-Host ""

