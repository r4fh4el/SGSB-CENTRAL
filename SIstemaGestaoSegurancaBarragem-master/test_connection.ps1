# Script para testar conexao com SQL Server
# Sistema de Gestao de Seguranca de Barragem (SGSB)
#
# NOTA DE SEGURANCA: Este script aceita -Password como string para compatibilidade
# com scripts automatizados. Para uso interativo, prefira usar -Credential (Get-Credential).
# Exemplo: .\test_connection.ps1 -Credential (Get-Credential)

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
Write-Host "Teste de Conexao SQL Server" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

Write-Host "Servidor: $Server" -ForegroundColor Yellow
Write-Host "Banco de Dados: $Database" -ForegroundColor Yellow

# Construir string de conexao
if ($UseTrustedConnection -or ([string]::IsNullOrEmpty($User) -and [string]::IsNullOrEmpty($Password))) {
    Write-Host "Autenticacao: Windows (Trusted Connection)" -ForegroundColor Yellow
    $masterConnectionString = "Data Source=$Server;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=Yes;MultipleActiveResultSets=True;Application Name=EntityFramework"
    $dbConnectionString = "Data Source=$Server;Initial Catalog=$Database;Integrated Security=True;TrustServerCertificate=Yes;MultipleActiveResultSets=True;Application Name=EntityFramework"
} else {
    Write-Host "Autenticacao: SQL Server (Usuario: $User)" -ForegroundColor Yellow
    $masterConnectionString = "Data Source=$Server;Initial Catalog=master;Persist Security Info=True;User ID=$User;Password=$Password;TrustServerCertificate=Yes;MultipleActiveResultSets=True;Application Name=EntityFramework"
    $dbConnectionString = "Data Source=$Server;Initial Catalog=$Database;Persist Security Info=True;User ID=$User;Password=$Password;TrustServerCertificate=Yes;MultipleActiveResultSets=True;Application Name=EntityFramework"
}
Write-Host ""

# Testar conexao com master primeiro
Write-Host "[1/3] Testando conexao com SQL Server (master)..." -ForegroundColor Yellow

try {
    $connection = New-Object System.Data.SqlClient.SqlConnection($masterConnectionString)
    $connection.Open()
    
    Write-Host "  Conexao estabelecida com sucesso!" -ForegroundColor Green
    Write-Host "  Versao do SQL Server: $($connection.ServerVersion)" -ForegroundColor Gray
    
    # Verificar se o banco de dados existe
    Write-Host ""
    Write-Host "[2/3] Verificando banco de dados '$Database'..." -ForegroundColor Yellow
    
    $checkDbCommand = $connection.CreateCommand()
    $checkDbCommand.CommandText = "SELECT COUNT(*) FROM sys.databases WHERE name = '$Database'"
    $dbExists = $checkDbCommand.ExecuteScalar()
    
    if ($dbExists -eq 0) {
        Write-Host "  Banco de dados '$Database' nao existe." -ForegroundColor Yellow
        Write-Host "  Criando banco de dados..." -ForegroundColor Yellow
        
        $createDbCommand = $connection.CreateCommand()
        $createDbCommand.CommandText = "CREATE DATABASE [$Database]"
        $createDbCommand.ExecuteNonQuery()
        
        Write-Host "  Banco de dados '$Database' criado com sucesso!" -ForegroundColor Green
    } else {
        Write-Host "  Banco de dados '$Database' ja existe" -ForegroundColor Green
    }
    
    $connection.Close()
    
    # Testar conexao com o banco especifico
    Write-Host ""
    Write-Host "[3/3] Testando conexao com banco '$Database'..." -ForegroundColor Yellow
    
    try {
        $dbConnection = New-Object System.Data.SqlClient.SqlConnection($dbConnectionString)
        $dbConnection.Open()
        
        Write-Host "  Conexao com banco '$Database' estabelecida!" -ForegroundColor Green
        
        # Verificar tabelas (se existirem)
        $tablesCommand = $dbConnection.CreateCommand()
        $tablesCommand.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'"
        $tableCount = $tablesCommand.ExecuteScalar()
        
        Write-Host "  Total de tabelas: $tableCount" -ForegroundColor Gray
        
        $dbConnection.Close()
        
        Write-Host ""
        Write-Host "========================================" -ForegroundColor Cyan
        Write-Host "Teste concluido com sucesso!" -ForegroundColor Green
        Write-Host "========================================" -ForegroundColor Cyan
        Write-Host ""
        Write-Host "Proximos passos:" -ForegroundColor Yellow
        Write-Host "  1. Execute: .\create_database.ps1 (para aplicar migrations)" -ForegroundColor Gray
        Write-Host "  2. Ou execute: .\apply_migrations.ps1 (apenas migrations)" -ForegroundColor Gray
        Write-Host "  3. Verifique com: .\verify_database.ps1" -ForegroundColor Gray
        Write-Host ""
        
        exit 0
    } catch {
        Write-Host "  Erro ao conectar com banco '$Database': $($_.Exception.Message)" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host ""
    Write-Host "Erro ao conectar: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host ""
    Write-Host "Verifique:" -ForegroundColor Yellow
    Write-Host "  1. Servidor SQL Server esta acessivel?" -ForegroundColor Gray
    Write-Host "  2. Porta 15000 esta aberta no firewall?" -ForegroundColor Gray
    Write-Host "  3. Credenciais estao corretas?" -ForegroundColor Gray
    Write-Host "  4. TrustServerCertificate=Yes esta na string de conexao?" -ForegroundColor Gray
    Write-Host ""
    exit 1
}
