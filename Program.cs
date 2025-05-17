using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjetoTarefas.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Força leitura do appsettings.json (evita o erro da string de conexão)
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Lê e imprime a string de conexão para debug
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine("🔗 String de conexão: " + connectionString);

// Configura o DbContext com Postgre
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Adiciona serviços básicos
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aplica migrations automaticamente (opcional)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); // Aplica as migrations
}

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
