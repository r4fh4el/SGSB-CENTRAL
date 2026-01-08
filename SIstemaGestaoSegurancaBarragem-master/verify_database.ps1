# Script para verificar o status do banco de dados e migrations aplicadas
# Sistema de Gestão de Segurança de Barragem (SGSB)
#
# NOTA DE SEGURANCA: Este script aceita -Password como string para compatibilidade
# com scripts automatizados. Para uso interativo, prefira usar -Credential (Get-Credential).
# Exemplo: .\verify_database.ps1 -Credential (Get-Credential)

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
Write-Host "Verificacao do Banco de Dados SGSB" -ForegroundColor Cyan
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
Write-Host ""

try {
    # Verificar se o banco existe
    Write-Host "[1/3] Verificando se o banco de dados existe..." -ForegroundColor Yellow
    $connection = New-Object System.Data.SqlClient.SqlConnection($masterConnectionString)
    $connection.Open()
    
    $checkDbCommand = $connection.CreateCommand()
    $checkDbCommand.CommandText = "SELECT COUNT(*) FROM sys.databases WHERE name = '$Database'"
    $dbExists = $checkDbCommand.ExecuteScalar()
    
    if ($dbExists -eq 1) {
        Write-Host "✓ Banco de dados '$Database' existe" -ForegroundColor Green
    } else {
        Write-Host "✗ Banco de dados '$Database' não encontrado" -ForegroundColor Red
        $connection.Close()
        exit 1
    }
    
    $connection.Close()
    
    # Verificar tabelas
    Write-Host ""
    Write-Host "[2/3] Verificando tabelas criadas..." -ForegroundColor Yellow
    $dbConnection = New-Object System.Data.SqlClient.SqlConnection($connectionString)
    $dbConnection.Open()
    
    $tablesCommand = $dbConnection.CreateCommand()
    $tablesCommand.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'"
    $tableCount = $tablesCommand.ExecuteScalar()
    
    Write-Host "✓ Total de tabelas: $tableCount" -ForegroundColor Green
    
    if ($tableCount -gt 0) {
        $listTablesCommand = $dbConnection.CreateCommand()
        $listTablesCommand.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME"
        $reader = $listTablesCommand.ExecuteReader()
        
        Write-Host ""
        Write-Host "Tabelas encontradas:" -ForegroundColor Cyan
        while ($reader.Read()) {
            Write-Host "  - $($reader[0])" -ForegroundColor Gray
        }
        $reader.Close()
    }
    
    # Verificar migrations aplicadas
    Write-Host ""
    Write-Host "[3/3] Verificando migrations aplicadas..." -ForegroundColor Yellow
    
    $migrationsCommand = $dbConnection.CreateCommand()
    $migrationsCommand.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '__EFMigrationsHistory'"
    $hasMigrationsTable = $migrationsCommand.ExecuteScalar()
    
    if ($hasMigrationsTable -eq 1) {
        $migrationsListCommand = $dbConnection.CreateCommand()
        $migrationsListCommand.CommandText = "SELECT MigrationId FROM __EFMigrationsHistory ORDER BY MigrationId"
        $migrationsReader = $migrationsListCommand.ExecuteReader()
        
        $migrationCount = 0
        $migrations = @()
        while ($migrationsReader.Read()) {
            $migrations += $migrationsReader[0]
            $migrationCount++
        }
        $migrationsReader.Close()
        
        Write-Host "✓ Total de migrations aplicadas: $migrationCount" -ForegroundColor Green
        Write-Host ""
        Write-Host "Últimas 5 migrations:" -ForegroundColor Cyan
        $migrations[-5..-1] | ForEach-Object {
            Write-Host "  - $_" -ForegroundColor Gray
        }
    } else {
        Write-Host "⚠ Tabela de migrations não encontrada. Migrations podem não ter sido aplicadas." -ForegroundColor Yellow
    }
    
    $dbConnection.Close()
    
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "Verificação concluída!" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Cyan
    
} catch {
    Write-Host "✗ Erro durante verificação: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

