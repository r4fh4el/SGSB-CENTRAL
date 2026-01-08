using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.Genericos;
using Dominio.Interfaces.InterfaceServicos;
using Dominio.Servicos;
using Entidades.Entidades;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio;
using Infraestrutura.Repositorio.Genericos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Threading;
using WebAPI.Token;

// Forçar cultura invariante para modo invariante do .NET (ANTES de qualquer coisa)
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<Contexto>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSignalR();
builder.Services.AddSignalRCore();
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<Contexto>();
builder.Services.AddControllers();
// Configuração de cultura desabilitada para modo invariante
// builder.Services.Configure<RequestLocalizationOptions>(
//       options =>
//       {
//           var supportedCultures = new List<CultureInfo>
//               {
//                     new CultureInfo("pt-BR")
//               };
//
//           options.DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR");
//           options.SupportedCultures = supportedCultures;
//           options.SupportedUICultures = supportedCultures;
//           options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
//       });

//// INTERFACES e REPOSITORIOS

builder.Services.AddSingleton(typeof(IGenericos<>), typeof(RepositorioGenerico<>));
builder.Services.AddSingleton<IBarragem, RepositorioBarragem>();
builder.Services.AddSingleton<ITipoMaterialBarragem, RepositorioTipoMaterialBarragem>();
builder.Services.AddSingleton<ITipoEstruturaBarragem, RepositorioTipoEstruturaBarragem>();
builder.Services.AddSingleton<IVertedouro, RepositorioVertedouro>();
builder.Services.AddSingleton<IUsuario, RepositorioUsuario>();
builder.Services.AddSingleton<ISistemaDrenagem, RepositorioSistemaDrenagem>();
builder.Services.AddSingleton<IDocumentacaoProjetoConstrucaoOperacao, RepositorioDocumentacaoProjetoConstrucaoOperacao>();
builder.Services.AddSingleton<IInformacoesComplementares, RepositorioInformacoesComplementares>();
builder.Services.AddSingleton<IInstrumentos, RepositorioInstrumentos>();
builder.Services.AddSingleton<ICotaAreaVolume, RepositorioCotaAreaVolume>();
builder.Services.AddSingleton<ITipoEdificacao, RepositorioTipoEdificacao>();
builder.Services.AddSingleton<ICondicaoFundacao, RepositorioCondicaoFundacao>();
builder.Services.AddSingleton<IDescarregadorFundo, RepositorioDescarregadorFundo>();
builder.Services.AddSingleton<IDadosGerais, RepositorioDadosGerais>();
builder.Services.AddSingleton<IUsoBarragem, RepositorioUsoBarragem>();
builder.Services.AddSingleton<ITomadaDagua, RepositorioTomadaDagua>();
builder.Services.AddSingleton<ICaracterizacaoAreaJusanteBarragem, RepositorioCaracterizacaoAreaJusanteBarragem>();
builder.Services.AddSingleton<IInspecoes, RepositorioInspecoes>();
builder.Services.AddSingleton<IReservatorio, RepositorioReservatorio>();
builder.Services.AddSingleton<ITalude, RepositorioTalude>();
builder.Services.AddSingleton<ITipoEmpreendedor, RepositorioTipoEmpreendedor>();
builder.Services.AddSingleton<IDocumentacaoPerguntas, RepositorioDocumentacaoPerguntas>();
builder.Services.AddSingleton<IDanoPotencialAssociado, RepositorioDanoPotencialAssociado>();
builder.Services.AddSingleton<IUsoSoloPredominante, RepositorioUsoSoloPredominante>();
builder.Services.AddSingleton<ICaracteristicaBacia, RepositorioCaracteristicaBacia>();
builder.Services.AddSingleton<ITempoConcentracao, RepositorioTempoConcentracao>();
builder.Services.AddSingleton<IIndiceCaracterizacaoBH, RepositorioIndiceCaracterizacaoBH>();
builder.Services.AddSingleton<IVazaPico, RepositorioVazaPico>();
builder.Services.AddSingleton<ICategoriaRisco, RepositorioCategoriaRisco>();
builder.Services.AddSingleton<ITempoRuptura, RepositorioTempoRuptura>();
builder.Services.AddSingleton<IHidrogramaTriangula, RepositorioHidrogramaTriangula>();
builder.Services.AddSingleton<IHidrogramaParabolico, RepositorioHidrogramaParabolico>();
builder.Services.AddSingleton<IRotaFuga, RepositorioRotaFuga>();
builder.Services.AddSingleton<ITicket, RepositorioTicket>();
builder.Services.AddSingleton<IPontoEncontro, RepositorioPontoEncontro>();
builder.Services.AddSingleton<IInstrumentosBarragem, RepositorioInstrumentosBarragem>();
builder.Services.AddSingleton<ITipoEdificacaoBarragem, RepositorioTipoEdificacaoBarragem>();
builder.Services.AddSingleton<ISistemaDrenagemBarragem, RepositorioSistemaDrenagemBarragem>();
builder.Services.AddSingleton<IDocumentacaoProjetoConstrucaoOperacao, RepositorioDocumentacaoProjetoConstrucaoOperacao>();
builder.Services.AddSingleton<IUsoBarragemBarragem, RepositorioUsoBarragemBarragem>();
builder.Services.AddSingleton<IBarragemKml, RepositorioBarragemKml>();
builder.Services.AddSingleton<IZona, RepositorioZona>();



//// SERVI�OS e DOMINIOS
builder.Services.AddSingleton<IServicoTipoMaterialBarragem, ServicoTipoMaterialBarragem>();
builder.Services.AddSingleton<IServicoBarragem, ServicoBarragem>();
builder.Services.AddSingleton<IServicoTipoEstruturaBarragem, ServicoTipoEstruturaBarragem>();
builder.Services.AddSingleton<IServicoVertedouro, ServicoVertedouro>();
builder.Services.AddSingleton<IServicoInstrumentos, ServicoInstrumentos>();
builder.Services.AddSingleton<IServicoSistemaDrenagem, ServicoSistemaDrenagem>();
builder.Services.AddSingleton<IServicoDocumentacaoProjetoConstrucaoOperacao, ServicoDocumentacaoProjetoConstrucaoOperacao>();
builder.Services.AddSingleton<IServicoInformacoesComplementares, ServicoInformacoesComplementares>();
builder.Services.AddSingleton<IServicoTipoEdificacao, ServicoTipoEdificacao>();
builder.Services.AddSingleton<IServicoCotaAreaVolume, ServicoCotaAreaVolume>();
builder.Services.AddSingleton<IServicoCondicaoFundacao, ServicoCondicaoFundacao>();
builder.Services.AddSingleton<IServicoDescarregadorFundo, ServicoDescarregadorFundo>();
builder.Services.AddSingleton<IServicoDadosGerais, ServicoDadosGerais>();
builder.Services.AddSingleton<IServicoUsoBarragem, ServicoUsoBarragem>();
builder.Services.AddSingleton<IServicoTomadaDagua, ServicoTomadaDagua>();
builder.Services.AddSingleton<IServicoCaracterizacaoAreaJusanteBarragem, ServicoCaracterizacaoAreaJusanteBarragem>();
builder.Services.AddSingleton<IServicoInspecoes, ServicoInspecoes>();
builder.Services.AddSingleton<IServicoReservatorio, ServicoReservatorio>();
builder.Services.AddSingleton<IServicoTalude, ServicoTalude>();
builder.Services.AddSingleton<IServicoTipoEmpreendedor, ServicoTipoEmpreendedor>();
builder.Services.AddSingleton<IServicoDocumentacaoPerguntas, ServicoDocumentacaoPerguntas>();
builder.Services.AddSingleton<IServicoDanoPotencialAssociado, ServicoDanoPotencialAssociado>();
builder.Services.AddSingleton<IServicoUsoSoloPredominante, ServicoUsoSoloPredominante>();
builder.Services.AddSingleton<IServicoCaracteristicaBacia, ServicoCaracteristicaBacia>();
builder.Services.AddSingleton<IServicoTempoConcentracao, ServicoTempoConcentracao>();
builder.Services.AddSingleton<IServicoIndiceCaracterizacaoBH, ServicoIndiceCaracterizacaoBH>();
builder.Services.AddSingleton<IServicoVazaPico, ServicoVazaPico>();
builder.Services.AddSingleton<IServicoCategoriaRisco, ServicoCategoriaRisco>();
builder.Services.AddSingleton<IServicoTempoRuptura, ServicoTempoRuptura>();
builder.Services.AddSingleton<IServicoHidrogramaTriangula, ServicoHidrogramaTriangula>();
builder.Services.AddSingleton<IServicoHidrogramaParabolico, ServicoHidrogramaParabolico>();
builder.Services.AddSingleton<IServicoRotaFuga, ServicoRotaFuga>();
builder.Services.AddSingleton<IServicoTicket, ServicoTicket>();
builder.Services.AddSingleton<IServicoPontoEncontro, ServicoPontoEncontro>();
builder.Services.AddSingleton<IServicoInstrumentosBarragem, ServicoInstrumentosBarragem>();
builder.Services.AddSingleton<IServicoTipoEdificacaoBarragem, ServicoTipoEdificacaoBarragem>();
builder.Services.AddSingleton<IServicoSistemaDrenagemBarragem, ServicoSistemaDrenagemBarragem>();
builder.Services.AddSingleton<IServicoUsoBarragemBarragem, ServicoUsoBarragemBarragem>();
builder.Services.AddSingleton<IServicoBarragemKml, ServicoBarragemKml>();
builder.Services.AddSingleton<IServicoZona, ServicoZona>();

//// INTERFACE APLICACAO
builder.Services.AddSingleton<IAplicacaoBarragem, AplicacaoBarragem>();
builder.Services.AddSingleton<IAplicacaoTipoMaterialBarragem, AplicacaoTipoMaterialBarragem>();
builder.Services.AddSingleton<IAplicacaoTipoEstruturaBarragem, AplicacaoTipoEstruturaBarragem>();
builder.Services.AddSingleton<IAplicacaoVertedouro, AplicacaoVertedouro>();
builder.Services.AddSingleton<IAplicacaoUsuario, AplicacaoUsuario>();
builder.Services.AddSingleton<IAplicacaoInstrumentos, AplicacaoInstrumentos>();
builder.Services.AddSingleton<IAplicacaoSistemaDrenagem, AplicacaoSistemaDrenagem>();
builder.Services.AddSingleton<IAplicacaoDocumentacaoProjetoConstrucaoOperacao, AplicacaoDocumentacaoProjetoConstrucaoOperacao>();
builder.Services.AddSingleton<IAplicacaoTipoEdificacao, AplicacaoTipoEdificacao>();
builder.Services.AddSingleton<IAplicacaoInformacoesComplementares, AplicacaoInformacoesComplementares>();
builder.Services.AddSingleton<IAplicacaoCotaAreaVolume, AplicacaoCotaAreaVolume>();
builder.Services.AddSingleton<IAplicacaoCondicaoFundacao, AplicacaoCondicaoFundacao>();
builder.Services.AddSingleton<IAplicacaoDescarregadorFundo, AplicacaoDescarregadorFundo>();
builder.Services.AddSingleton<IAplicacaoDadosGerais, AplicacaoDadosGerais>();
builder.Services.AddSingleton<IAplicacaoTalude, AplicacaoTalude>();
builder.Services.AddSingleton<IAplicacaoUsoBarragem, AplicacaoUsoBarragem>();
builder.Services.AddSingleton<IAplicacaoTomadaDagua, AplicacaoTomadaDagua>();
builder.Services.AddSingleton<IAplicacaoCaracterizacaoAreaJusanteBarragem, AplicacaoCaracterizacaoAreaJusanteBarragem>();
builder.Services.AddSingleton<IAplicacaoInspecoes, AplicacaoInspecoes>();
builder.Services.AddSingleton<IAplicacaoReservatorio, AplicacaoReservatorio>();
builder.Services.AddSingleton<IAplicacaoTipoEmpreendedor, AplicacaoTipoEmpreendedor>();
builder.Services.AddSingleton<IAplicacaoDocumentacaoPerguntas, AplicacaoDocumentacaoPerguntas>();
builder.Services.AddSingleton<IAplicacaoDanoPotencialAssociado, AplicacaoDanoPotencialAssociado>();
builder.Services.AddSingleton<IAplicacaoUsoSoloPredominante, AplicacaoUsoSoloPredominante>();
builder.Services.AddSingleton<IAplicacaoCaracteristicaBacia, AplicacaoCaracteristicaBacia>();
builder.Services.AddSingleton<IAplicacaoTempoConcentracao, AplicacaoTempoConcentracao>();
builder.Services.AddSingleton<IAplicacaoIndiceCaracterizacaoBH, AplicacaoIndiceCaracterizacaoBH>();
builder.Services.AddSingleton<IAplicacaoVazaPico, AplicacaoVazaPico>();
builder.Services.AddSingleton<IAplicacaoCategoriaRisco, AplicacaoCategoriaRisco>();
builder.Services.AddSingleton<IAplicacaoTempoRuptura, AplicacaoTempoRuptura>();
builder.Services.AddSingleton<IAplicacaoHidrogramaParabolico, AplicacaoHidrogramaParabolico>();
builder.Services.AddSingleton<IAplicacaoHidrogramaTriangula, AplicacaoHidrogramaTriangula>();
builder.Services.AddSingleton<IAplicacaoRotaFuga, AplicacaoRotaFuga>();
builder.Services.AddSingleton<IAplicacaoTicket, AplicacaoTicket>();
builder.Services.AddSingleton<IAplicacaoPontoEncontro, AplicacaoPontoEncontro>();
builder.Services.AddSingleton<IAplicacaoInstrumentosBarragem, AplicacaoInstrumentosBarragem>();
builder.Services.AddSingleton<IAplicacaoTipoEdificacaoBarragem, AplicacaoTipoEdificacaoBarragem>();
builder.Services.AddSingleton<IAplicacaoSistemaDrenagemBarragem, AplicacaoSistemaDrenagemBarragem>();
builder.Services.AddSingleton<IAplicacaoUsoBarragemBarragem, AplicacaoUsoBarragemBarragem>();
builder.Services.AddSingleton<IAplicacaoBarragemKml, AplicacaoBarragemKml>();
builder.Services.AddSingleton<IAplicacaoZona, AplicacaoZona>();





//// SEGURANCA
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(option =>
     {
         option.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = false,
             ValidateAudience = false,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,

             ValidIssuer = "SGSB.Securiry.Bearer",
             ValidAudience = "SGSB.Securiry.Bearer",
             IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-SGSB159357")
         };

         option.Events = new JwtBearerEvents
         {
             OnAuthenticationFailed = context =>
             {
                 Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                 return Task.CompletedTask;
             },
             OnTokenValidated = context =>
             {
                 Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                 return Task.CompletedTask;
             }
         };
     });

//// CORS
builder.Services.AddCors();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
});

var app = builder.Build();
app.UseWebSockets();
//// CORS EXEMPLO
///
var urlCliente1 = Constantes.URI;
var urlCliente2 = "https://www.teste2.com.br";

app.UseCors(x => x.WithOrigins(urlCliente1, urlCliente2));
app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
}

app.UseRouting();


app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
