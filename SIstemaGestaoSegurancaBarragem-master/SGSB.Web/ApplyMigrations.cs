using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using SGSB.Web.Data;

namespace SGSB.Web
{
    public static class ApplyMigrations
    {
        public static int Run(string[] args)
        {
            // FORCAR CULTURA INVARIANTE ANTES DE QUALQUER COISA
            // Isso deve ser a primeira coisa executada
            try
            {
                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
                CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                
                // Tambem definir no sistema
                System.Globalization.CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
                System.Globalization.CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
            }
            catch { }

            try
            {
                Console.WriteLine("========================================");
                Console.WriteLine("Aplicando migrations do Identity...");
                Console.WriteLine("========================================");
                Console.WriteLine("");
                
                // Obter diretorio base
                var basePath = Directory.GetCurrentDirectory();
                if (!File.Exists(Path.Combine(basePath, "appsettings.json")))
                {
                    // Tentar subir um nivel se estiver em bin/Release
                    if (basePath.Contains("bin"))
                    {
                        basePath = Directory.GetParent(basePath).Parent.Parent.FullName;
                    }
                }
                
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    Console.WriteLine("ERRO: ConnectionString 'DefaultConnection' nao encontrada no appsettings.json");
                    return 1;
                }
                
                Console.WriteLine($"String de conexao encontrada: {connectionString.Substring(0, Math.Min(50, connectionString.Length))}...");
                Console.WriteLine("");
                
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(connectionString);

                using (var context = new ApplicationDbContext(optionsBuilder.Options))
                {
                    Console.WriteLine("Verificando conexao com banco de dados...");
                    try
                    {
                        // Tentar conectar
                        var canConnect = context.Database.CanConnect();
                        if (!canConnect)
                        {
                            Console.WriteLine("ERRO: Nao foi possivel conectar ao banco de dados");
                            Console.WriteLine("Verifique:");
                            Console.WriteLine("  - Se o banco de dados SGSB_2 existe");
                            Console.WriteLine("  - Se as credenciais estao corretas");
                            Console.WriteLine("  - Se o usuario 'sa' tem permissao para acessar o banco");
                            return 1;
                        }
                        Console.WriteLine("Conexao estabelecida com sucesso!");
                        Console.WriteLine("");
                    }
                    catch (Microsoft.Data.SqlClient.SqlException sqlEx)
                    {
                        Console.WriteLine("ERRO SQL ao verificar conexao:");
                        Console.WriteLine($"  Numero do erro: {sqlEx.Number}");
                        Console.WriteLine($"  Mensagem: {sqlEx.Message}");
                        Console.WriteLine($"  Servidor: {sqlEx.Server}");
                        if (sqlEx.InnerException != null)
                        {
                            Console.WriteLine($"  Erro interno: {sqlEx.InnerException.Message}");
                        }
                        Console.WriteLine("");
                        
                        // Mensagens especificas por codigo de erro
                        switch (sqlEx.Number)
                        {
                            case 2:
                            case 53:
                                Console.WriteLine("PROBLEMA: Nao foi possivel conectar ao servidor SQL");
                                Console.WriteLine("  Verifique se o servidor esta rodando e acessivel");
                                break;
                            case 18456:
                                Console.WriteLine("PROBLEMA: Falha na autenticacao");
                                Console.WriteLine("  Verifique se o usuario 'sa' e a senha estao corretos");
                                break;
                            case 4060:
                                Console.WriteLine("PROBLEMA: Nao foi possivel abrir o banco de dados 'SGSB_2'");
                                Console.WriteLine("  O banco de dados pode nao existir");
                                Console.WriteLine("  Execute o script create_database.ps1 para criar o banco");
                                break;
                            default:
                                Console.WriteLine($"PROBLEMA: Erro SQL {sqlEx.Number}");
                                break;
                        }
                        return 1;
                    }
                    catch (Exception connEx)
                    {
                        Console.WriteLine("ERRO ao verificar conexao:");
                        Console.WriteLine($"  Tipo: {connEx.GetType().Name}");
                        Console.WriteLine($"  Mensagem: {connEx.Message}");
                        if (connEx.InnerException != null)
                        {
                            Console.WriteLine($"  Erro interno: {connEx.InnerException.Message}");
                        }
                        Console.WriteLine("");
                        Console.WriteLine("Verifique:");
                        Console.WriteLine("  - Se o servidor SQL esta acessivel (108.181.193.92:15000)");
                        Console.WriteLine("  - Se a porta 15000 esta aberta no firewall");
                        Console.WriteLine("  - Se as credenciais estao corretas");
                        Console.WriteLine("  - Se o banco de dados SGSB_2 existe");
                        return 1;
                    }
                    
                    Console.WriteLine("Aplicando migrations...");
                    try
                    {
                        context.Database.Migrate();
                        Console.WriteLine("");
                        Console.WriteLine("========================================");
                        Console.WriteLine("SUCESSO: Migrations aplicadas!");
                        Console.WriteLine("========================================");
                        return 0;
                    }
                    catch (Exception migrateEx)
                    {
                        Console.WriteLine("ERRO ao aplicar migrations:");
                        Console.WriteLine($"  Mensagem: {migrateEx.Message}");
                        if (migrateEx.InnerException != null)
                        {
                            Console.WriteLine($"  Erro interno: {migrateEx.InnerException.Message}");
                        }
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine("========================================");
                Console.WriteLine("ERRO ao aplicar migrations:");
                Console.WriteLine("========================================");
                Console.WriteLine($"Mensagem: {ex.Message}");
                Console.WriteLine("");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Erro interno: {ex.InnerException.Message}");
                    Console.WriteLine("");
                }
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return 1;
            }
        }
    }
}

