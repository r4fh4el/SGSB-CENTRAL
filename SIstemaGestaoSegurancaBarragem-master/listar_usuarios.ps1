# Script para listar usuários cadastrados no banco de dados
# Autor: Sistema SGSB
# Data: 2026-01-08

param(
    [string]$Server = "108.181.193.92,15000",
    [string]$Database = "SGSB_2",
    [string]$Username = "sa",
    [string]$Password = "SenhaNova@123"
)

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Listando Usuários Cadastrados" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Criar string de conexão
$connectionString = "Server=$Server;Database=$Database;User ID=$Username;Password=$Password;TrustServerCertificate=Yes;MultipleActiveResultSets=True;"

try {
    Write-Host "[1/3] Conectando ao banco de dados..." -ForegroundColor Yellow
    Write-Host "Servidor: $Server" -ForegroundColor Gray
    Write-Host "Database: $Database" -ForegroundColor Gray
    Write-Host ""
    
    # Criar conexão
    $connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)
    $connection.Open()
    
    Write-Host "[2/3] Consultando tabela AspNetUsers..." -ForegroundColor Yellow
    Write-Host ""
    
    # Query para listar usuários
    $query = @"
SELECT 
    Id,
    UserName,
    Email,
    EmailConfirmed,
    PhoneNumber,
    PhoneNumberConfirmed,
    TwoFactorEnabled,
    LockoutEnabled,
    LockoutEnd,
    AccessFailedCount,
    CONVERT(VARCHAR(19), LockoutEnd, 120) AS LockoutEndFormatted
FROM AspNetUsers
ORDER BY UserName
"@
    
    $command = New-Object System.Data.SqlClient.SqlCommand($query, $connection)
    $adapter = New-Object System.Data.SqlClient.SqlDataAdapter($command)
    $dataset = New-Object System.Data.DataSet
    $adapter.Fill($dataset) | Out-Null
    
    $table = $dataset.Tables[0]
    
    if ($table.Rows.Count -eq 0) {
        Write-Host "[3/3] Nenhum usuário encontrado no banco de dados." -ForegroundColor Yellow
        Write-Host ""
        Write-Host "Isso pode indicar que:" -ForegroundColor Yellow
        Write-Host "  - As tabelas do Identity não foram criadas (migrations não aplicadas)" -ForegroundColor Gray
        Write-Host "  - Nenhum usuário foi cadastrado ainda" -ForegroundColor Gray
        Write-Host ""
    } else {
        Write-Host "[3/3] Total de usuários encontrados: $($table.Rows.Count)" -ForegroundColor Green
        Write-Host ""
        Write-Host "========================================" -ForegroundColor Cyan
        Write-Host "Lista de Usuários:" -ForegroundColor Cyan
        Write-Host "========================================" -ForegroundColor Cyan
        Write-Host ""
        
        # Exibir resultados em formato de tabela
        $table | Format-Table -AutoSize -Property @(
            @{Label="UserName"; Expression={$_.UserName}; Width=30},
            @{Label="Email"; Expression={$_.Email}; Width=35},
            @{Label="EmailConfirmed"; Expression={if($_.EmailConfirmed){"Sim"}else{"Não"}}; Width=15},
            @{Label="PhoneNumber"; Expression={if([string]::IsNullOrEmpty($_.PhoneNumber)){"-"}else{$_.PhoneNumber}}; Width=20},
            @{Label="LockoutEnabled"; Expression={if($_.LockoutEnabled){"Sim"}else{"Não"}}; Width=15},
            @{Label="AccessFailedCount"; Expression={$_.AccessFailedCount}; Width=18}
        )
        
        Write-Host ""
        Write-Host "========================================" -ForegroundColor Cyan
        Write-Host "Detalhes por usuário:" -ForegroundColor Cyan
        Write-Host "========================================" -ForegroundColor Cyan
        Write-Host ""
        
        foreach ($row in $table.Rows) {
            Write-Host "ID: $($row.Id)" -ForegroundColor White
            Write-Host "  UserName: $($row.UserName)" -ForegroundColor Cyan
            Write-Host "  Email: $($row.Email)" -ForegroundColor Cyan
            Write-Host "  Email Confirmado: $(if($row.EmailConfirmed){'Sim'}else{'Não'})" -ForegroundColor $(if($row.EmailConfirmed){'Green'}else{'Yellow'})
            Write-Host "  Telefone: $(if([string]::IsNullOrEmpty($row.PhoneNumber)){'-'}else{$row.PhoneNumber})" -ForegroundColor Gray
            Write-Host "  Telefone Confirmado: $(if($row.PhoneNumberConfirmed){'Sim'}else{'Não'})" -ForegroundColor Gray
            Write-Host "  Two-Factor: $(if($row.TwoFactorEnabled){'Habilitado'}else{'Desabilitado'})" -ForegroundColor Gray
            Write-Host "  Lockout Habilitado: $(if($row.LockoutEnabled){'Sim'}else{'Não'})" -ForegroundColor $(if($row.LockoutEnabled){'Yellow'}else{'Green'})
            if ($row.LockoutEnd -and $row.LockoutEnd -ne [DBNull]::Value) {
                Write-Host "  Lockout até: $($row.LockoutEndFormatted)" -ForegroundColor Red
            }
            Write-Host "  Tentativas falhadas: $($row.AccessFailedCount)" -ForegroundColor $(if($row.AccessFailedCount -gt 0){'Red'}else{'Green'})
            Write-Host ""
        }
    }
    
    # Verificar se a tabela existe
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "Verificando estrutura do banco..." -ForegroundColor Cyan
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host ""
    
    $checkTableQuery = @"
SELECT 
    TABLE_NAME,
    (SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers') AS COLUMN_COUNT
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME = 'AspNetUsers'
"@
    
    $checkCommand = New-Object System.Data.SqlClient.SqlCommand($checkTableQuery, $connection)
    $checkAdapter = New-Object System.Data.SqlClient.SqlDataAdapter($checkCommand)
    $checkDataset = New-Object System.Data.DataSet
    $checkAdapter.Fill($checkDataset) | Out-Null
    
    $checkTable = $checkDataset.Tables[0]
    
    if ($checkTable.Rows.Count -gt 0) {
        Write-Host "✓ Tabela AspNetUsers existe" -ForegroundColor Green
        Write-Host "  Colunas: $($checkTable.Rows[0].COLUMN_COUNT)" -ForegroundColor Gray
    } else {
        Write-Host "✗ Tabela AspNetUsers NÃO existe" -ForegroundColor Red
        Write-Host "  É necessário aplicar as migrations do Identity" -ForegroundColor Yellow
    }
    
    Write-Host ""
    
    # Listar outras tabelas do Identity
    $identityTablesQuery = @"
SELECT TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME LIKE 'AspNet%'
ORDER BY TABLE_NAME
"@
    
    $identityCommand = New-Object System.Data.SqlClient.SqlCommand($identityTablesQuery, $connection)
    $identityAdapter = New-Object System.Data.SqlClient.SqlDataAdapter($identityCommand)
    $identityDataset = New-Object System.Data.DataSet
    $identityAdapter.Fill($identityDataset) | Out-Null
    
    $identityTable = $identityDataset.Tables[0]
    
    if ($identityTable.Rows.Count -gt 0) {
        Write-Host "Tabelas do Identity encontradas:" -ForegroundColor Cyan
        foreach ($row in $identityTable.Rows) {
            Write-Host "  - $($row.TABLE_NAME)" -ForegroundColor Gray
        }
    } else {
        Write-Host "Nenhuma tabela do Identity encontrada!" -ForegroundColor Red
        Write-Host "Execute as migrations do Identity para criar as tabelas." -ForegroundColor Yellow
    }
    
    $connection.Close()
    
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "Consulta concluída!" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Cyan
    
} catch {
    Write-Host ""
    Write-Host "ERRO ao conectar ou consultar o banco de dados:" -ForegroundColor Red
    Write-Host $_.Exception.Message -ForegroundColor Red
    Write-Host ""
    
    if ($_.Exception.InnerException) {
        Write-Host "Detalhes:" -ForegroundColor Yellow
        Write-Host $_.Exception.InnerException.Message -ForegroundColor Red
    }
    
    Write-Host ""
    Write-Host "Verifique:" -ForegroundColor Yellow
    Write-Host "  - Se o servidor SQL está acessível" -ForegroundColor Gray
    Write-Host "  - Se as credenciais estão corretas" -ForegroundColor Gray
    Write-Host "  - Se o firewall permite conexões na porta 15000" -ForegroundColor Gray
    Write-Host ""
    
    exit 1
}

