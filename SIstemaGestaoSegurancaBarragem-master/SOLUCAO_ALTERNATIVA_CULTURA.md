# Solução Alternativa: Instalar ICU no Servidor

## Problema
O `Microsoft.Data.SqlClient` tenta usar "en-us" mesmo em modo invariante, causando `CultureNotFoundException`.

## Solução Alternativa: Instalar ICU ao invés de usar modo invariante

Se o modo invariante não funcionar, podemos instalar as bibliotecas ICU no servidor Linux:

### 1. Instalar ICU no Debian/Ubuntu
```bash
sudo apt-get update
sudo apt-get install -y libicu-dev libicu72
```

### 2. Remover modo invariante
```bash
# Remover DOTNET_SYSTEM_GLOBALIZATION_INVARIANT do systemd service
# E do runtimeconfig.json
```

### 3. Configurar locale do sistema
```bash
sudo locale-gen en_US.UTF-8
sudo update-locale LANG=en_US.UTF-8
export LC_ALL=en_US.UTF-8
export LANG=en_US.UTF-8
```

## Comparação de Abordagens

| Abordagem | Vantagens | Desvantagens |
|-----------|-----------|--------------|
| **Modo Invariante** | Não precisa de bibliotecas externas | `Microsoft.Data.SqlClient` tem problemas |
| **ICU Instalado** | Funciona com todas as bibliotecas | Requer instalação de pacotes, maior tamanho |

## Recomendação
Tentar primeiro com ICU instalado, pois é mais compatível com bibliotecas .NET existentes.

