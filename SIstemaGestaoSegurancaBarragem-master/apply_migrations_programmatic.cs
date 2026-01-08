// Script C# para aplicar migrations programaticamente
// Compile e execute: dotnet run --project WebAPI
// Ou adicione este código temporariamente no Program.cs da WebAPI

using Microsoft.EntityFrameworkCore;
using Infraestrutura.Configuracoes;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext
builder.Services.AddDbContext<Contexto>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Aplicar migrations automaticamente ao iniciar
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

// Continue com a configuração normal da aplicação...
// app.MapControllers();
// app.Run();

