# Guia de Criação do Banco de Dados SGSB

Este guia explica como criar um novo banco de dados e aplicar as migrations do sistema SGSB (Sistema de Gestão de Segurança de Barragem).

## Pré-requisitos

1. **.NET SDK 7.0** ou superior instalado
2. **SQL Server** acessível (servidor: 108.181.193.92,15000)
3. **PowerShell** (Windows) ou PowerShell Core (Linux/Mac)
4. **Entity Framework Core Tools** (já incluído no projeto)

## Configuração Atual

- **Servidor**: 108.181.193.92,15000
- **Banco de Dados**: SGSB_2
- **Usuário**: admin
- **Senha**: Rl031139@

## Método 1: Script Automatizado (Recomendado)

Execute o script PowerShell que automatiza todo o processo:

```powershell
.\create_database.ps1
```

O script irá:
1. ✅ Testar a conexão com o SQL Server
2. ✅ Criar o banco de dados SGSB_2 (se não existir)
3. ✅ Atualizar os arquivos `appsettings.json`
4. ✅ Aplicar todas as migrations do Entity Framework

### Parâmetros Opcionais

Você pode personalizar os parâmetros:

```powershell
.\create_database.ps1 -Server "108.181.193.92,15000" -Database "SGSB_2" -User "admin" -Password "Rl031139@"
```

## Método 2: Manual (Passo a Passo)

### Passo 1: Criar o Banco de Dados

Execute o script de teste de conexão:

```powershell
.\test_connection.ps1
```

Ou crie manualmente usando SQL Server Management Studio ou sqlcmd:

```sql
CREATE DATABASE SGSB_2;
```

### Passo 2: Atualizar Strings de Conexão

Os arquivos `appsettings.json` já foram atualizados com a nova string de conexão:

- `WebAPI/appsettings.json`
- `SGSB.Web/appsettings.json`

### Passo 3: Aplicar Migrations

Execute o comando do Entity Framework:

```powershell
dotnet ef database update --project Infraestrutura --startup-project WebAPI
```

Ou especifique a string de conexão diretamente:

```powershell
dotnet ef database update --project Infraestrutura --startup-project WebAPI --connection "Data Source=108.181.193.92,15000;Initial Catalog=SGSB_2;Persist Security Info=True;User ID=admin;Password=Rl031139@;TrustServerCertificate=Yes;MultipleActiveResultSets=True;Application Name=EntityFramework"
```

## Método 3: Aplicação Programática

Você pode aplicar migrations programaticamente adicionando este código no `Program.cs` da WebAPI:

```csharp
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<Contexto>();
        context.Database.Migrate();
        Console.WriteLine("Migrations aplicadas com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao aplicar migrations: {ex.Message}");
    }
}
```

## Verificação

### 1. Verificar Banco de Dados

Conecte-se ao SQL Server e verifique se o banco foi criado:

```sql
SELECT name FROM sys.databases WHERE name = 'SGSB_2';
```

### 2. Verificar Tabelas

Verifique se as tabelas foram criadas:

```sql
USE SGSB_2;
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES;
```

### 3. Testar API Swagger

Inicie a WebAPI e acesse o Swagger:

```powershell
cd WebAPI
dotnet run
```

Acesse: `https://localhost:5001/swagger` (ou a porta configurada)

## Estrutura de Migrations

As migrations estão localizadas em: `Infraestrutura/Migrations/`

Principais migrations:
- `20230418124017_Criacao_18042023.cs` - Criação inicial
- `20231216033055_ADDZONA.cs` - Última migration conhecida

## Troubleshooting

### Erro: "Cannot open database"

- Verifique se o banco de dados foi criado
- Verifique as credenciais de acesso
- Verifique se o servidor está acessível

### Erro: "Migration already applied"

- O banco já possui as migrations aplicadas
- Para recriar, delete o banco e execute novamente

### Erro: "dotnet ef not found"

Instale as ferramentas do Entity Framework:

```powershell
dotnet tool install --global dotnet-ef
```

### Erro de conexão

Verifique:
1. Firewall do servidor SQL Server
2. Porta 15000 está aberta
3. Credenciais corretas
4. TrustServerCertificate=Yes na string de conexão

## Arquivos Modificados

- ✅ `WebAPI/appsettings.json` - String de conexão atualizada
- ✅ `SGSB.Web/appsettings.json` - String de conexão atualizada
- ✅ `Infraestrutura/Configuracoes/Contexto.cs` - String de conexão padrão atualizada

## Próximos Passos

1. Execute o script `create_database.ps1`
2. Verifique o banco de dados no SQL Server
3. Teste a API através do Swagger
4. Configure os dados iniciais (se necessário)

## Suporte

Para problemas ou dúvidas, verifique:
- Logs da aplicação
- Logs do SQL Server
- Documentação do Entity Framework Core

