using Biblioteca.Domain.Ports.In;
using Biblioteca.Domain.Ports.Out;
using Biblioteca.Domain.Services;
using Biblioteca.Infrastructure.Adapters.Out.Repositories;
using Biblioteca.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ITipoMaterialRepository, TipoMaterialRepository>();
builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IPrestamoRepository, PrestamoRepository>();
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();


builder.Services.AddScoped<TipoMaterialService>();
builder.Services.AddScoped<MaterialService>();
builder.Services.AddScoped<PrestamoService>();
builder.Services.AddScoped<PersonaService>();
builder.Services.AddScoped<RolService>();


builder.Services.AddScoped<TipoMaterialUseCases>();
builder.Services.AddScoped<MaterialUseCases>();
builder.Services.AddScoped<PrestamoUseCases>();
builder.Services.AddScoped<PersonaUseCases>();
builder.Services.AddScoped<RolUseCases>();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(); // <-- Necesario para registrar tus controladores

var app = builder.Build();

app.MapControllers(); // <-- Esto activa los endpoints tipo api/[controller]

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
