using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using MinimalAPIPeliculas;
using MinimalAPIPeliculas.EndPoints;
using MinimalAPIPeliculas.Entidades;
using MinimalAPIPeliculas.Migrations;
using MinimalAPIPeliculas.Repositorios;

var builder = WebApplication.CreateBuilder(args);
var origenesPermitidos = builder.Configuration.GetValue<string>("OriginesPermitidos")!;

#region Inicio de área de los servicios

builder.Services.AddDbContext<ApplicationDbContext>(opciones => 
    opciones.UseSqlServer("name=DefaultConnection"));

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(configuracion =>
    {
        configuracion.WithOrigins(origenesPermitidos).AllowAnyHeader().AllowAnyMethod();
    });

    opciones.AddPolicy("libre", configuracion =>
    {
        configuracion.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddOutputCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepositorioGeneros, RepositorioGeneros>();
builder.Services.AddScoped<IRepositorioActores, RepositorioActores>();

builder.Services.AddAutoMapper(typeof(Program));

#endregion

var app = builder.Build();

#region Inicio de área de los middleware

//if (builder.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseOutputCache();

#region EndPoints
app.MapGet("/", [EnableCors(policyName: "libre")] () => "Hola mundo!");

app.MapGroup("/generos").MapGeneros();
app.MapGroup("/actores").MapActores();
#endregion
#endregion

app.Run();