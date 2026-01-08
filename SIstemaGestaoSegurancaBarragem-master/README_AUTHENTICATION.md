# Guia de Teste de Autenticação e Criação de Usuário

Este guia explica como testar o sistema de autenticação e criação de usuários do SGSB.

## Endpoints Disponíveis

### 1. Criar Usuário (Identity - Recomendado)
- **URL**: `POST /API/AdicionarUsuarioIdentity`
- **Body**:
```json
{
  "nome": "João Silva",
  "login": "joao.silva",
  "email": "joao.silva@teste.com",
  "senha": "Senha@123",
  "celular": "(11) 98765-4321",
  "idade": 35
}
```

### 2. Criar Usuário (Método Antigo)
- **URL**: `POST /API/AdicionaUsuario`
- **Body**: Mesmo formato acima

### 3. Login e Obter Token JWT (Identity - Recomendado)
- **URL**: `POST /API/CriarTokenIdentity`
- **Body**:
```json
{
  "email": "joao.silva@teste.com",
  "senha": "Senha@123"
}
```

### 4. Login e Obter Token JWT (Método Antigo)
- **URL**: `POST /API/CriarToken`
- **Body**: Mesmo formato acima

### 5. Listar Usuários
- **URL**: `GET /API/BuscarUsuarios`

## Como Testar

### Método 1: Script PowerShell Automatizado

Execute o script completo de teste:

```powershell
.\test_user_authentication.ps1
```

Ou com parâmetros personalizados:

```powershell
.\test_user_authentication.ps1 -Nome "Maria Santos" -Login "maria.santos" -Email "maria@teste.com" -Senha "MinhaSenha@123"
```

### Método 2: Script Simples

Para um teste rápido:

```powershell
.\test_user_simple.ps1
```

### Método 3: Via Swagger

1. Inicie a API:
```powershell
cd WebAPI
dotnet run
```

2. Acesse o Swagger: `http://localhost:5204/swagger`

3. Teste os endpoints:
   - `/API/AdicionarUsuarioIdentity` - Criar usuário
   - `/API/CriarTokenIdentity` - Fazer login e obter token

### Método 4: Via PowerShell (Manual)

#### Criar Usuário:
```powershell
$userData = @{
    nome = "João Silva"
    login = "joao.silva"
    email = "joao.silva@teste.com"
    senha = "Senha@123"
    celular = "(11) 98765-4321"
    idade = 35
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5204/API/AdicionarUsuarioIdentity" `
    -Method Post `
    -Body $userData `
    -ContentType "application/json"
```

#### Fazer Login:
```powershell
$loginData = @{
    email = "joao.silva@teste.com"
    senha = "Senha@123"
} | ConvertTo-Json

$token = Invoke-RestMethod -Uri "http://localhost:5204/API/CriarTokenIdentity" `
    -Method Post `
    -Body $loginData `
    -ContentType "application/json"

Write-Host "Token: $token"
```

#### Usar Token em Requisições:
```powershell
$headers = @{
    "Authorization" = "Bearer $token"
}

Invoke-RestMethod -Uri "http://localhost:5204/API/BuscarUsuarios" `
    -Method Get `
    -Headers $headers
```

### Método 5: Via cURL

#### Criar Usuário:
```bash
curl -X POST "http://localhost:5204/API/AdicionarUsuarioIdentity" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "João Silva",
    "login": "joao.silva",
    "email": "joao.silva@teste.com",
    "senha": "Senha@123",
    "celular": "(11) 98765-4321",
    "idade": 35
  }'
```

#### Fazer Login:
```bash
curl -X POST "http://localhost:5204/API/CriarTokenIdentity" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "joao.silva@teste.com",
    "senha": "Senha@123"
  }'
```

## Estrutura de Dados

### Modelo Login
```csharp
public class Login
{
    public string nome { get; set; }      // Nome completo
    public string login { get; set; }     // Nome de usuário (UserName)
    public string email { get; set; }     // Email (usado para login)
    public string senha { get; set; }     // Senha
    public string celular { get; set; }   // Telefone celular
    public int idade { get; set; }        // Idade
}
```

### ApplicationUser
- Herda de `IdentityUser`
- Campos adicionais: Nome, Idade, Celular, Tipo
- Tipo padrão: `Comun`

## Requisitos de Senha

O sistema usa ASP.NET Core Identity, que por padrão requer:
- Mínimo de 6 caracteres
- Pelo menos um caractere não alfanumérico (recomendado)
- Pelo menos uma letra maiúscula (recomendado)
- Pelo menos uma letra minúscula (recomendado)
- Pelo menos um número (recomendado)

**Exemplo de senha válida**: `Senha@123`

## Troubleshooting

### Erro: "Falta algumas informações"
- Verifique se todos os campos obrigatórios estão preenchidos
- Email e senha são obrigatórios

### Erro: "Erro ao adicionar o usuário"
- Verifique se o email já não está cadastrado
- Verifique se a senha atende aos requisitos
- Verifique os logs da aplicação

### Erro: 401 Unauthorized no login
- Verifique se o email está correto
- Verifique se a senha está correta
- Verifique se o usuário foi criado com sucesso
- **Nota**: O endpoint `CriarTokenIdentity` usa `email` como UserName para login

### Erro: "Erro ao confirmar o usuário"
- O sistema tenta confirmar o email automaticamente
- Se falhar, o usuário pode não estar totalmente ativado
- Verifique os logs para mais detalhes

### Usuário criado mas não consegue fazer login
- Verifique se está usando o `email` (não o `login`) no endpoint de login
- O Identity pode estar configurado para usar email como UserName

## Verificação no Banco de Dados

Para verificar se o usuário foi criado:

```sql
USE SGSB_2;
SELECT Id, UserName, Email, Nome, Celular, Tipo 
FROM AspNetUsers;
```

## Próximos Passos

1. Execute o script de teste
2. Verifique se o usuário foi criado no banco
3. Teste o login via Swagger
4. Use o token JWT para acessar endpoints protegidos

## Notas Importantes

- O sistema usa **ASP.NET Core Identity** para gerenciamento de usuários
- As senhas são **hasheadas** automaticamente pelo Identity
- O token JWT tem validade de **60 minutos**
- O endpoint `CriarTokenIdentity` usa `email` como identificador para login
- O sistema confirma o email automaticamente após a criação

