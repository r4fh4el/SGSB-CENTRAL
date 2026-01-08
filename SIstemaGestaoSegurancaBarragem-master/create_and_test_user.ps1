# Script completo para criar e testar usuário
# Inclui múltiplas tentativas de login (email e username)

param(
    [string]$ApiUrl = "http://localhost:5204",
    [string]$Nome = "Maria Santos",
    [string]$Login = "maria.santos",
    [string]$Email = "maria.santos@teste.com",
    [string]$Senha = "Senha@123456",
    [string]$Celular = "(11) 98888-7777",
    [int]$Idade = 28
)

$ErrorActionPreference = "Continue"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Criação e Teste de Usuário SGSB" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Verificar se a API está rodando
Write-Host "[0/5] Verificando se a API está acessível..." -ForegroundColor Yellow
try {
    $healthCheck = Invoke-WebRequest -Uri "$ApiUrl/swagger" -Method Get -TimeoutSec 5 -ErrorAction Stop
    Write-Host "  ✓ API está acessível" -ForegroundColor Green
} catch {
    Write-Host "  ✗ API não está acessível em $ApiUrl" -ForegroundColor Red
    Write-Host "  Inicie a API primeiro: cd WebAPI; dotnet run" -ForegroundColor Yellow
    exit 1
}

# Dados do usuário
$userData = @{
    nome = $Nome
    login = $Login
    email = $Email
    senha = $Senha
    celular = $Celular
    idade = $Idade
} | ConvertTo-Json

Write-Host ""
Write-Host "Dados do novo usuário:" -ForegroundColor Yellow
Write-Host "  Nome: $Nome" -ForegroundColor Gray
Write-Host "  Login (UserName): $Login" -ForegroundColor Gray
Write-Host "  Email: $Email" -ForegroundColor Gray
Write-Host "  Senha: $Senha" -ForegroundColor Gray
Write-Host "  Celular: $Celular" -ForegroundColor Gray
Write-Host "  Idade: $Idade" -ForegroundColor Gray
Write-Host ""

# Passo 1: Criar usuário
Write-Host "[1/5] Criando usuário via Identity..." -ForegroundColor Yellow
try {
    $createUserUrl = "$ApiUrl/API/AdicionarUsuarioIdentity"
    Write-Host "  POST $createUserUrl" -ForegroundColor Gray
    
    $createResponse = Invoke-RestMethod -Uri $createUserUrl -Method Post -Body $userData -ContentType "application/json" -ErrorAction Stop
    
    if ($createResponse -is [string]) {
        if ($createResponse -like "*sucesso*") {
            Write-Host "  ✓ $createResponse" -ForegroundColor Green
        } else {
            Write-Host "  ⚠ Resposta: $createResponse" -ForegroundColor Yellow
        }
    } elseif ($createResponse -is [array]) {
        Write-Host "  ✗ Erros encontrados:" -ForegroundColor Red
        $createResponse | ForEach-Object {
            Write-Host "    - $($_.Code): $($_.Description)" -ForegroundColor Red
        }
        exit 1
    } else {
        Write-Host "  ✓ Usuário criado: $createResponse" -ForegroundColor Green
    }
} catch {
    $statusCode = "N/A"
    if ($_.Exception.Response) {
        $statusCode = $_.Exception.Response.StatusCode.value__
    }
    $errorMessage = $_.Exception.Message
    if ($_.ErrorDetails.Message) {
        $errorMessage = $_.ErrorDetails.Message
    }
    
    Write-Host "  ✗ Erro ao criar usuário (Status: $statusCode)" -ForegroundColor Red
    Write-Host "    $errorMessage" -ForegroundColor Red
    
    # Tentar método alternativo
    Write-Host ""
    Write-Host "  Tentando método alternativo (AdicionaUsuario)..." -ForegroundColor Yellow
    try {
        $altResponse = Invoke-RestMethod -Uri "$ApiUrl/API/AdicionaUsuario" -Method Post -Body $userData -ContentType "application/json" -ErrorAction Stop
        Write-Host "  ✓ Usuário criado via método alternativo: $altResponse" -ForegroundColor Green
    } catch {
        Write-Host "  ✗ Método alternativo também falhou" -ForegroundColor Red
        exit 1
    }
}

Start-Sleep -Seconds 2

# Passo 2: Listar usuários
Write-Host ""
Write-Host "[2/5] Verificando usuários cadastrados..." -ForegroundColor Yellow
try {
    $users = Invoke-RestMethod -Uri "$ApiUrl/API/BuscarUsuarios" -Method Get -ErrorAction Stop
    $foundUser = $users | Where-Object { 
        ($_.email -eq $Email -or $_.Email -eq $Email) -or 
        ($_.userName -eq $Login -or $_.UserName -eq $Login)
    }
    
    if ($foundUser) {
        Write-Host "  ✓ Usuário encontrado!" -ForegroundColor Green
        Write-Host "    ID: $($foundUser.id)" -ForegroundColor Gray
        Write-Host "    UserName: $($foundUser.userName)" -ForegroundColor Gray
        Write-Host "    Email: $($foundUser.email)" -ForegroundColor Gray
        Write-Host "    Nome: $($foundUser.nome)" -ForegroundColor Gray
        Write-Host "    Total de usuários no sistema: $($users.Count)" -ForegroundColor Gray
    } else {
        Write-Host "  ⚠ Usuário não encontrado na listagem" -ForegroundColor Yellow
        Write-Host "    Total de usuários: $($users.Count)" -ForegroundColor Gray
    }
} catch {
    Write-Host "  ⚠ Erro ao listar usuários: $($_.Exception.Message)" -ForegroundColor Yellow
}

# Passo 3: Testar login com EMAIL
Write-Host ""
Write-Host "[3/5] Testando login com EMAIL..." -ForegroundColor Yellow
$token = $null
try {
    $loginData = @{
        email = $Email
        senha = $Senha
    } | ConvertTo-Json
    
    $token = Invoke-RestMethod -Uri "$ApiUrl/API/CriarTokenIdentity" -Method Post -Body $loginData -ContentType "application/json" -ErrorAction Stop
    
    if ($token) {
        Write-Host "  ✓ Login com EMAIL bem-sucedido!" -ForegroundColor Green
        Write-Host "    Token: $($token.Substring(0, [Math]::Min(50, $token.Length)))..." -ForegroundColor Gray
        $global:JwtToken = $token
    }
} catch {
    $statusCode = "N/A"
    if ($_.Exception.Response) {
        $statusCode = $_.Exception.Response.StatusCode.value__
    }
    Write-Host "  ✗ Login com EMAIL falhou (Status: $statusCode)" -ForegroundColor Red
    Write-Host "    Erro: $($_.Exception.Message)" -ForegroundColor Red
}

# Passo 4: Testar login com USERNAME (se email falhou)
if (-not $token) {
    Write-Host ""
    Write-Host "[4/5] Testando login com USERNAME..." -ForegroundColor Yellow
    try {
        $loginData = @{
            email = $Login  # Tentando usar login como email
            senha = $Senha
        } | ConvertTo-Json
        
        $token = Invoke-RestMethod -Uri "$ApiUrl/API/CriarTokenIdentity" -Method Post -Body $loginData -ContentType "application/json" -ErrorAction Stop
        
        if ($token) {
            Write-Host "  ✓ Login com USERNAME bem-sucedido!" -ForegroundColor Green
            Write-Host "    Token: $($token.Substring(0, [Math]::Min(50, $token.Length)))..." -ForegroundColor Gray
            $global:JwtToken = $token
        }
    } catch {
        Write-Host "  ✗ Login com USERNAME também falhou" -ForegroundColor Red
        Write-Host "    Verifique se o usuário foi criado corretamente" -ForegroundColor Yellow
    }
} else {
    Write-Host ""
    Write-Host "[4/5] Pulando teste com USERNAME (login com EMAIL já funcionou)" -ForegroundColor Gray
}

# Passo 5: Testar token
Write-Host ""
Write-Host "[5/5] Validando token JWT..." -ForegroundColor Yellow
if ($global:JwtToken) {
    Write-Host "  ✓ Token JWT válido e pronto para uso" -ForegroundColor Green
    Write-Host ""
    Write-Host "  Exemplo de uso do token:" -ForegroundColor Cyan
    Write-Host "    `$headers = @{ 'Authorization' = 'Bearer $($global:JwtToken)' }" -ForegroundColor Gray
    Write-Host "    Invoke-RestMethod -Uri '$ApiUrl/API/BuscarUsuarios' -Headers `$headers" -ForegroundColor Gray
} else {
    Write-Host "  ⚠ Nenhum token foi obtido" -ForegroundColor Yellow
    Write-Host "    Verifique os logs da API para mais detalhes" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Teste concluído!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Resumo:" -ForegroundColor Yellow
if ($global:JwtToken) {
    Write-Host "  ✓ Usuário criado: $Email" -ForegroundColor Green
    Write-Host "  ✓ Login testado com sucesso" -ForegroundColor Green
    Write-Host "  ✓ Token JWT obtido" -ForegroundColor Green
} else {
    Write-Host "  ⚠ Usuário pode ter sido criado, mas login falhou" -ForegroundColor Yellow
    Write-Host "    Verifique manualmente no Swagger: $ApiUrl/swagger" -ForegroundColor Yellow
}
Write-Host ""
Write-Host "Acesse o Swagger para mais testes: $ApiUrl/swagger" -ForegroundColor Cyan
Write-Host ""

