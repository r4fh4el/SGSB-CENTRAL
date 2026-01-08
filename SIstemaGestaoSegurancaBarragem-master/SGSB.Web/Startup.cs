using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SGSB.Web.Areas.Identity;
using SGSB.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorTable;
using Radzen;
using Entidades.Entidades;
using OneSignal.CSharp.SDK;
using OneSignal;
using OneSignal.CSharp;
using Blazorise;
using Humanizer;
using Microsoft.AspNetCore.Http.Features;
using System.Globalization;
using System.Threading;

namespace SGSB.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
            // Garantir cultura invariante no construtor também
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
                options.MemoryBufferThreshold = int.MaxValue;
                options.MultipartBodyLengthLimit = long.MaxValue;
                options.MultipartHeadersCountLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });
            // Inside the ConfigureServices method

            services.AddControllers();
            // Inside the ConfigureServices method
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAny",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddServerSideBlazor().AddHubOptions(config => config.MaximumReceiveMessageSize = 1048576);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                // Adicionar configuracoes para evitar problemas de cultura
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                });
            });
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddBlazorTable();

            services.AddRazorPages();
            services.AddServerSideBlazor();
     
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddSingleton<WeatherForecastService>();
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();

            services.AddHttpClient<CondicaoFundacaoService>();
           services.AddHttpClient<TipoEstruturaBarragemService>();
           services.AddHttpClient<BarragemService>();
           services.AddHttpClient<DocumentacaoProjetoConstrucaoOperacaoService>();
           services.AddHttpClient<InstrumentosService>();
           services.AddHttpClient<TipoEmpreendedorService>();
           services.AddHttpClient<SistemaDrenagemService>();
           services.AddHttpClient<CotaAreaVolumeService>();
           services.AddHttpClient<DocumentacaoPerguntasService>();
           services.AddHttpClient<TipoEdificacaoService>();
           services.AddHttpClient<UsoBarragemService>();
           services.AddHttpClient<DanoPotencialAssociadoService>();
           services.AddHttpClient<CategoriaRiscoService>();
           services.AddHttpClient<UsoSoloPredominanteService>();
           services.AddHttpClient<CaracteristicaBaciaService>();
           services.AddHttpClient<TempoConcentracaoService>();
           services.AddHttpClient<IndiceCaracterizacaoBHService>();
           services.AddHttpClient<TipoMaterialBarragemService>();
           services.AddHttpClient<VazaPicoService>();
           services.AddHttpClient<HidrogramaTriangulaService>();
           services.AddHttpClient<TempoRupturaService>();
           services.AddHttpClient<HidrogramaParabolicoService>();
           services.AddHttpClient<DadosGeraisService>();
           services.AddHttpClient<TaludeService>();
           services.AddHttpClient<VertedouroService>();
           services.AddHttpClient<DescarregadorFundoService>();
           services.AddHttpClient<TomadaDaguaService>();
           services.AddHttpClient<CaracterizacaoAreaJusanteBarragemService>();
           services.AddHttpClient<InspecoesService>();
           services.AddHttpClient<InformacoesComplementaresService>();
           services.AddHttpClient<ReservatorioService>();
           services.AddHttpClient<RotaFugaService>();
           services.AddHttpClient<TicketService>();
           services.AddHttpClient<PontoEncontroService>();
           services.AddHttpClient<NotificacaoService>();
           services.AddHttpClient<OndaCheiaService>();
           services.AddHttpClient<InstrumentosBarragemService>();
           services.AddHttpClient<TipoEdificacaoBarragemService>();
           services.AddHttpClient<UsoBarragemBarragemService>();
           services.AddHttpClient<SistemaDrenagemBarragemService>();
           services.AddHttpClient<DocumentacaoProjetoConstrucaoOperacaoService>();
           services.AddHttpClient<UsoBarragemBarragemService>();
           services.AddHttpClient<UsoBarragemBarragemService>();
           services.AddHttpClient<ConverterKmlparaJsonService>();
           services.AddHttpClient<MapsService>();
         




        }
    
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
     
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("AllowAny");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
        
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MyHub>("/myHub");
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
