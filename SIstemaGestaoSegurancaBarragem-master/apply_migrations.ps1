# Script para aplicar migrations usando Entity Framework
# Sistema de Gestão de Segurança de Barragem (SGSB)
#
# NOTA DE SEGURANCA: Este script aceita -Password como string para compatibilidade
# com scripts automatizados. Para uso interativo, prefira usar -Credential (Get-Credential).
# Exemplo: .\apply_migrations.ps1 -Credential (Get-Credential)

param(
    [string]$Server = "108.181.193.92,15000",
    [string]$Database = "SGSB_2",
    [string]$User = "sa",
    [System.Management.Automation.PSCredential]$Credential,
    [Parameter(Mandatory=$false)]
    [ValidateNotNullOrEmpty()]
    [string]$Password,
    [switch]$UseTrustedConnection = $false
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
Write-Host "Aplicacao de Migrations SGSB" -ForegroundColor Cyan
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

# Verificar se dotnet esta instalado
Write-Host "[1/3] Verificando .NET SDK..." -ForegroundColor Yellow
$dotnetVersion = dotnet --version 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Host ".NET SDK nao encontrado. Instale o .NET SDK 7.0 ou superior." -ForegroundColor Red
    Write-Host "  Execute: dotnet tool install --global dotnet-ef" -ForegroundColor Yellow
    exit 1
}
Write-Host "  .NET SDK versao: $dotnetVersion" -ForegroundColor Green

# Verificar se dotnet-ef esta instalado
Write-Host ""
Write-Host "[2/3] Verificando Entity Framework Tools..." -ForegroundColor Yellow
$efVersion = dotnet ef --version 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Host "  dotnet-ef nao encontrado. Instalando..." -ForegroundColor Yellow
    dotnet tool install --global dotnet-ef --version 7.0.0
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Erro ao instalar dotnet-ef" -ForegroundColor Red
        exit 1
    }
    Write-Host "  dotnet-ef instalado com sucesso" -ForegroundColor Green
} else {
    Write-Host "  Entity Framework Tools: $efVersion" -ForegroundColor Green
}

# Verificar se o banco existe
Write-Host ""
Write-Host "[3/3] Verificando banco de dados..." -ForegroundColor Yellow

try {
    $connection = New-Object System.Data.SqlClient.SqlConnection($masterConnectionString)
    $connection.Open()
    
    $checkDbCommand = $connection.CreateCommand()
    $checkDbCommand.CommandText = "SELECT COUNT(*) FROM sys.databases WHERE name = '$Database'"
    $dbExists = $checkDbCommand.ExecuteScalar()
    
    if ($dbExists -eq 0) {
        Write-Host "  Banco de dados '$Database' nao existe. Criando..." -ForegroundColor Yellow
        $createDbCommand = $connection.CreateCommand()
        $createDbCommand.CommandText = "CREATE DATABASE [$Database]"
        $createDbCommand.ExecuteNonQuery()
        Write-Host "  Banco de dados '$Database' criado!" -ForegroundColor Green
    } else {
        Write-Host "  Banco de dados '$Database' existe" -ForegroundColor Green
    }
    
    $connection.Close()
} catch {
    Write-Host "Erro ao verificar/criar banco de dados: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Aplicar migrations
Write-Host ""
Write-Host "Aplicando migrations do Entity Framework..." -ForegroundColor Yellow
Write-Host "  Comando: dotnet ef database update --project Infraestrutura --startup-project WebAPI" -ForegroundColor Gray
Write-Host ""

try {
    Push-Location $PSScriptRoot
    
    # Definir variavel de ambiente para a conexao
    $env:ConnectionStrings__DefaultConnection = $connectionString
    
    # Aplicar migrations
    dotnet ef database update --project Infraestrutura --startup-project WebAPI --connection $connectionString
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host ""
        Write-Host "Migrations aplicadas com sucesso!" -ForegroundColor Green
    } else {
        Write-Host ""
        Write-Host "Erro ao aplicar migrations. Codigo de saida: $LASTEXITCODE" -ForegroundColor Red
        Write-Host "  Verifique os logs acima para mais detalhes" -ForegroundColor Yellow
        Pop-Location
        exit 1
    }
} catch {
    Write-Host ""
    Write-Host "Erro ao aplicar migrations: $($_.Exception.Message)" -ForegroundColor Red
    Pop-Location
    exit 1
} finally {
    Pop-Location
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Processo concluído com sucesso!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Proximos passos:" -ForegroundColor Yellow
Write-Host "  1. Verifique o banco de dados usando: .\verify_database.ps1" -ForegroundColor Gray
Write-Host "  2. Teste a API atraves do Swagger" -ForegroundColor Gray
Write-Host ""

