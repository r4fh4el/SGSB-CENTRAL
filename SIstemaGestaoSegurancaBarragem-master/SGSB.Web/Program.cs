using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SGSB.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Verificar se o argumento e para aplicar migrations
            if (args.Length > 0 && args[0] == "ApplyMigrations")
            {
                var exitCode = ApplyMigrations.Run(args);
                Environment.Exit(exitCode);
                return;
            }
            
            // FORCAR CULTURA INVARIANTE ANTES DE QUALQUER COISA
            // Isso DEVE ser a primeira coisa executada, antes de qualquer biblioteca ser carregada
            try
            {
                // Configurar cultura invariante globalmente
                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
                CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                
                // Configurar para todos os threads futuros
                CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
                CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
                
                // Tambem definir no AppDomain
                AppDomain.CurrentDomain.SetData("REGEX_DEFAULT_MATCH_TIMEOUT", TimeSpan.FromSeconds(2.0));
            }
            catch { }
            
            // Inicializar workaround
            GlobalizationWorkaround.Initialize();
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    // Ambiente será definido pela variável de ambiente ASPNETCORE_ENVIRONMENT
                    // Não forçar aqui para permitir configuração via systemd
                });


    }
}