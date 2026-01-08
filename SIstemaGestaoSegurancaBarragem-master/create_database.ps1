# Script para criar banco de dados e aplicar migrations
# Sistema de Gestão de Segurança de Barragem (SGSB)
#
# NOTA DE SEGURANCA: Este script aceita -Password como string para compatibilidade
# com scripts automatizados. Para uso interativo, prefira usar -Credential (Get-Credential).
# Exemplo: .\create_database.ps1 -Credential (Get-Credential)

param(
    [string]$Server = "108.181.193.92,15000",
    [string]$Database = "SGSB_2",
    [string]$User = "sa",
    [System.Management.Automation.PSCredential]$Credential,
    [Parameter(Mandatory=$false)]
    [ValidateNotNullOrEmpty()]
    [string]$Password,
    [switch]$UseTrustedConnection = $false,
    [string]$Driver = "ODBC Driver 17 for SQL Server"
)

$ErrorActionPreference = "Stop"

# Processar credenciais: priorizar PSCredential, depois Password, depois padrão
if ($Credential) {
    $User = $Credential.UserName
    $Password = $Credential.GetNetworkCredential().Password
    Write-Host "Usando credenciais de PSCredential" -ForegroundColor Gray
} elseif ([string]::IsNullOrEmpty($Password)) {
    # Senha padrão apenas se não fornecida (aviso de segurança)
    $Password = "SenhaNova@123"
    Write-Host "AVISO: Usando senha padrão. Para produção, use -Credential (Get-Credential) ou -Password" -ForegroundColor Yellow
}

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Criacao do Banco de Dados SGSB" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Construir string de conexao
if ($UseTrustedConnection -or ([string]::IsNullOrEmpty($User) -and [string]::IsNullOrEmpty($Password))) {
    Write-Host "Autenticacao: Windows (Trusted Connection)" -ForegroundColor Yellow
    $connectionString = "Data Source=$Server;Initial Catalog=$Database;Integrated Security=True;TrustServerCertificate=Yes;MultipleActiveResultSets=True;Application Name=EntityFramework"
    $masterConnectionString = "Data Source=$Server;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=Yes;MultipleActiveResultSets=True;Application Name=EntityFramework"
} else {
    Write-Host "Autenticacao: SQL Server (Usuario: $User)" -ForegroundColor Yellow
    $connectionString = "Data Source=$Server;Initial Catalog=$Database;Persist Security Info=True;User ID=$User;Password=$Password;TrustServerCertificate=Yes;MultipleActiveResultSets=True;Application Name=EntityFramework"
    $masterConnectionString = "Data Source=$Server;Initial Catalog=master;Persist Security Info=True;User ID=$User;Password=$Password;TrustServerCertificate=Yes;MultipleActiveResultSets=True;Application Name=EntityFramework"
}

Write-Host "Servidor: $Server" -ForegroundColor Yellow
Write-Host "Banco de Dados: $Database" -ForegroundColor Yellow
Write-Host ""

# Passo 1: Testar conexão e criar banco de dados
Write-Host "[1/4] Testando conexão com SQL Server..." -ForegroundColor Yellow
try {
    $connection = New-Object System.Data.SqlClient.SqlConnection($masterConnectionString)
    $connection.Open()
    Write-Host "Conexao estabelecida com sucesso!" -ForegroundColor Green
    Write-Host "  Versao do SQL Server: $($connection.ServerVersion)" -ForegroundColor Gray
    
    # Criar banco de dados se nao existir
    Write-Host ""
    Write-Host "[2/4] Criando banco de dados '$Database'..." -ForegroundColor Yellow
    $createDbCommand = $connection.CreateCommand()
    $createDbCommand.CommandText = "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '$Database') BEGIN CREATE DATABASE [$Database] END"
    $createDbCommand.ExecuteNonQuery()
    Write-Host "Banco de dados '$Database' criado ou ja existe!" -ForegroundColor Green
    
    $connection.Close()
} catch {
    Write-Host "Erro ao conectar: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Passo 2: Atualizar arquivos de configuracao
Write-Host ""
Write-Host "[3/4] Atualizando arquivos de configuracao..." -ForegroundColor Yellow

$appsettingsFiles = @(
    "WebAPI\appsettings.json",
    "SGSB.Web\appsettings.json"
)

foreach ($file in $appsettingsFiles) {
    if (Test-Path $file) {
        Write-Host "  Atualizando $file..." -ForegroundColor Gray
        $content = Get-Content $file -Raw | ConvertFrom-Json
        $content.ConnectionStrings.DefaultConnection = $connectionString
        $content | ConvertTo-Json -Depth 10 | Set-Content $file
        Write-Host "  $file atualizado" -ForegroundColor Green
    } else {
        Write-Host "  Arquivo $file nao encontrado" -ForegroundColor Yellow
    }
}

# Passo 3: Aplicar migrations usando dotnet ef
Write-Host ""
Write-Host "[4/4] Aplicando migrations do Entity Framework..." -ForegroundColor Yellow

# Verificar se dotnet esta instalado
$dotnetVersion = dotnet --version 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Host ".NET SDK nao encontrado. Instale o .NET SDK 7.0 ou superior." -ForegroundColor Red
    exit 1
}

Write-Host "  .NET SDK versão: $dotnetVersion" -ForegroundColor Gray

# Navegar para o diretorio do projeto Infraestrutura
$infraestruturaPath = "Infraestrutura"
if (-not (Test-Path $infraestruturaPath)) {
    Write-Host "Diretorio Infraestrutura nao encontrado!" -ForegroundColor Red
    exit 1
}

# Definir variaveis de ambiente para a conexao
$env:ConnectionStrings__DefaultConnection = $connectionString

# Aplicar migrations
Write-Host "  Executando: dotnet ef database update --project Infraestrutura --startup-project WebAPI" -ForegroundColor Gray
Write-Host ""

try {
    Push-Location $PSScriptRoot
    dotnet ef database update --project Infraestrutura --startup-project WebAPI --connection $connectionString
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host ""
        Write-Host "Migrations aplicadas com sucesso!" -ForegroundColor Green
    } else {
        Write-Host ""
        Write-Host "Erro ao aplicar migrations. Codigo de saida: $LASTEXITCODE" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host ""
    Write-Host "Erro ao aplicar migrations: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
} finally {
    Pop-Location
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Processo concluído com sucesso!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Banco de dados '$Database' criado e migrations aplicadas." -ForegroundColor Green
Write-Host "String de conexao: $connectionString" -ForegroundColor Gray
Write-Host ""
Write-Host "Proximos passos:" -ForegroundColor Yellow
Write-Host "  1. Verifique a API Swagger em: https://localhost:5001/swagger (ou porta configurada)" -ForegroundColor Gray
Write-Host "  2. Teste a conexao atraves da API" -ForegroundColor Gray
Write-Host ""

