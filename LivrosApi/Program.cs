using webapi_dotnet.data;
using webapi_dotnet.repositories;
using Microsoft.OpenApi.Models;
using System.Reflection; // Para pegar o caminho do arquivo XML

var builder = WebApplication.CreateBuilder(args);

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Livros",
        Version = "v1",
        Description = "Esta API permite gerenciar uma coleção de livros, incluindo operações para obter, criar, atualizar e excluir livros.",
        Contact = new OpenApiContact
        {
            Name = "Bianca Letcia Román Caldeira",
            Email = "biancaroman1425@gmail.com"
        }
    });

    // Incluindo o caminho do arquivo XML de comentários
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Livros v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
