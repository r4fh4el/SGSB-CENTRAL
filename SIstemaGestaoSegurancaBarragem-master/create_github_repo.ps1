# Script para criar repositorio no GitHub e fazer push
# Sistema de Gestao de Seguranca de Barragem (SGSB)

param(
    [string]$RepoName = "SGSB-CENTRAL",
    [string]$GitHubUsername = "r4fh4el",
    [string]$GitHubToken = "",
    [string]$Description = "Sistema de Gestao de Seguranca de Barragem - Repositorio Central"
)

$ErrorActionPreference = "Stop"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Criacao de Repositorio GitHub" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Tentar criar via API se tiver token
if (-not [string]::IsNullOrEmpty($GitHubToken)) {
    Write-Host "[1/5] Criando repositorio no GitHub via API..." -ForegroundColor Yellow
    
    $repoData = @{
        name = $RepoName
        description = $Description
        private = $false
        auto_init = $false
    } | ConvertTo-Json
    
    $headers = @{
        "Authorization" = "token $GitHubToken"
        "Accept" = "application/vnd.github.v3+json"
    }
    
    try {
        $response = Invoke-RestMethod -Uri "https://api.github.com/user/repos" -Method Post -Headers $headers -Body $repoData -ContentType "application/json"
        Write-Host "  OK Repositorio criado: $($response.html_url)" -ForegroundColor Green
    } catch {
        if ($_.Exception.Response.StatusCode.value__ -eq 422) {
            Write-Host "  AVISO: Repositorio ja existe ou erro de validacao" -ForegroundColor Yellow
            Write-Host "    Continuando com configuracao..." -ForegroundColor Gray
        } else {
            Write-Host "  ERRO ao criar repositorio: $($_.Exception.Message)" -ForegroundColor Red
            Write-Host "    Tente criar manualmente em: https://github.com/new" -ForegroundColor Yellow
        }
    }
} else {
    Write-Host "[1/5] Pulando criacao via API" -ForegroundColor Gray
    Write-Host "  Se precisar criar via API, forneca: -GitHubToken 'seu_token'" -ForegroundColor Gray
    Write-Host "  Ou crie manualmente em: https://github.com/new" -ForegroundColor Gray
    Write-Host "  Nome: $RepoName" -ForegroundColor Cyan
}

# Configurar remote
Write-Host ""
Write-Host "[2/5] Configurando remote do Git..." -ForegroundColor Yellow

# Remover remote antigo se existir
$oldRemote = git remote get-url origin 2>&1
if ($LASTEXITCODE -eq 0) {
    Write-Host "  Removendo remote 'origin' antigo..." -ForegroundColor Gray
    git remote remove origin 2>&1 | Out-Null
}

# Adicionar novo remote
$repoUrl = "https://github.com/$GitHubUsername/$RepoName.git"
Write-Host "  Adicionando remote: $repoUrl" -ForegroundColor Gray
git remote add origin $repoUrl 2>&1 | Out-Null

if ($LASTEXITCODE -eq 0) {
    Write-Host "  OK Remote configurado" -ForegroundColor Green
} else {
    Write-Host "  AVISO: Erro ao configurar remote (pode ja existir)" -ForegroundColor Yellow
    git remote set-url origin $repoUrl 2>&1 | Out-Null
}

# Adicionar arquivos
Write-Host ""
Write-Host "[3/5] Adicionando arquivos ao Git..." -ForegroundColor Yellow

# Limpar arquivos deletados do cache se existirem
git rm --cached -r -f ../patches/* ../prisma/* ../server/* ../shared/* ../sqlserver/* ../types/* 2>&1 | Out-Null
git rm --cached -f ../*.patch ../*.prisma 2>&1 | Out-Null

# Adicionar todos os arquivos do diretorio atual
git add . 2>&1 | Out-Null
git add ../*.md ../*.toml ../*.json ../*.yaml 2>&1 | Out-Null

Write-Host "  OK Arquivos adicionados" -ForegroundColor Green

# Verificar se ha mudancas para commit
$status = git status --porcelain
if ([string]::IsNullOrWhiteSpace($status)) {
    Write-Host ""
    Write-Host "  AVISO: Nenhuma mudanca para commitar" -ForegroundColor Yellow
} else {
    # Fazer commit
    Write-Host ""
    Write-Host "[4/5] Fazendo commit..." -ForegroundColor Yellow
    
    git commit -m "Initial commit: Sistema de Gestao de Seguranca de Barragem" 2>&1 | Out-Null
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "  OK Commit realizado" -ForegroundColor Green
    } else {
        Write-Host "  AVISO: Erro ao fazer commit ou ja esta tudo commitado" -ForegroundColor Yellow
    }
}

# Verificar branch atual
$currentBranch = git branch --show-current 2>&1
if ([string]::IsNullOrEmpty($currentBranch)) {
    Write-Host ""
    Write-Host "  Criando branch 'main'..." -ForegroundColor Yellow
    git checkout -b main 2>&1 | Out-Null
}

# Fazer push
Write-Host ""
Write-Host "[5/5] Fazendo push para GitHub..." -ForegroundColor Yellow

git push -u origin main --force 2>&1

if ($LASTEXITCODE -eq 0) {
    Write-Host "  OK Push realizado com sucesso!" -ForegroundColor Green
} else {
    Write-Host "  ERRO ao fazer push" -ForegroundColor Red
    Write-Host "    Execute manualmente: git push -u origin main --force" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Repositorio configurado!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "URL do repositorio: https://github.com/$GitHubUsername/$RepoName" -ForegroundColor Cyan
Write-Host ""
Write-Host "Proximos passos:" -ForegroundColor Yellow
Write-Host "  1. Verifique o repositorio no GitHub" -ForegroundColor Gray
Write-Host "  2. Execute no servidor SSH:" -ForegroundColor Gray
Write-Host "     git clone https://github.com/$GitHubUsername/$RepoName.git" -ForegroundColor Cyan
Write-Host ""
