# Script simples para testar criação de usuário
# Versão simplificada para testes rápidos

param(
    [string]$ApiUrl = "http://localhost:5204"
)

# Dados do usuário de teste
$userData = @{
    nome = "João Silva"
    login = "joao.silva"
    email = "joao.silva@teste.com"
    senha = "Senha@123"
    celular = "(11) 98765-4321"
    idade = 35
} | ConvertTo-Json

Write-Host "Criando usuário de teste..." -ForegroundColor Yellow

try {
    # Criar usuário
    $response = Invoke-RestMethod -Uri "$ApiUrl/API/AdicionarUsuarioIdentity" `
        -Method Post `
        -Body $userData `
        -ContentType "application/json"
    
    Write-Host "Resposta: $response" -ForegroundColor Green
    
    # Testar login
    Write-Host "`nTestando login..." -ForegroundColor Yellow
    $loginData = @{
        email = "joao.silva@teste.com"
        senha = "Senha@123"
    } | ConvertTo-Json
    
    $token = Invoke-RestMethod -Uri "$ApiUrl/API/CriarTokenIdentity" `
        -Method Post `
        -Body $loginData `
        -ContentType "application/json"
    
    Write-Host "Token obtido: $($token.Substring(0, 50))..." -ForegroundColor Green
    Write-Host "`n✓ Teste concluído com sucesso!" -ForegroundColor Green
    
} catch {
    Write-Host "Erro: $($_.Exception.Message)" -ForegroundColor Red
    if ($_.ErrorDetails.Message) {
        Write-Host "Detalhes: $($_.ErrorDetails.Message)" -ForegroundColor Red
    }
}

