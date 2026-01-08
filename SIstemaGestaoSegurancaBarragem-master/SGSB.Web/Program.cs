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
            // Inicializar workaround ANTES de qualquer coisa
            GlobalizationWorkaround.Initialize();
            
            // Forçar cultura invariante ANTES de qualquer inicialização
            // Isso deve ser feito antes de qualquer código do .NET ser executado
            AppDomain.CurrentDomain.SetData("REGEX_DEFAULT_MATCH_TIMEOUT", TimeSpan.FromSeconds(2.0));
            
            // Configurar cultura invariante globalmente
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            
            // Configurar para todos os threads futuros
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
            
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