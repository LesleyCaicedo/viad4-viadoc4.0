using BusinessLayer.Services;
using DataLayer;
using DataLayer.repositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFacturaRepositorio, FacturaRepositorio>();
builder.Services.AddScoped<IFacturaInsertar, FacturaInsertar>();

builder.Services.AddScoped<INotaCreditoRepositorio, NotaCreditoRepositorio>();
builder.Services.AddScoped<INotaDebitoRepositorio, NotaDebitoRepositorio>();

builder.Services.AddScoped<IPruebasRepositorio, PruebasRepositorio>();
builder.Services.AddScoped<IPruebaServicio, PruebasServicio>();

builder.Services.AddDbContext<FacturacionElectronicaQaContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
