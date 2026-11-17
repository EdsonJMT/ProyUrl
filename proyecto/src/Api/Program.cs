using proyecto.src.Application.Interfaces;
using proyecto.src.Application.UseCases;
using proyecto.src.Domain.Interfaces;
using proyecto.src.Infrastrucutre.Persistence.Repository;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;



var builder = WebApplication.CreateBuilder(args);

// Forzar puerto si lo deseas
builder.WebHost.UseUrls("http://localhost:7077", "https://localhost:5075");

builder.Services.AddOpenApi();

// Obtener cadena de conexión y registrar dependencias
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IUrlRepository>(provider => new UrlRepository(connectionString!));
builder.Services.AddScoped<InsertarUrlUseCase>();
builder.Services.AddScoped<ClickUrlUseCase>();
builder.Services.AddScoped<IListaUrlUseCase, ListaUrlUseCase>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapOpenApi();

app.MapScalarApiReference();

app.UseAuthorization();
app.MapControllers();
app.Run();