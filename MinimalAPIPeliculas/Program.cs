using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using MinimalAPIPeliculas;
using MinimalAPIPeliculas.Entidades;
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

app.MapGet("/generos", async (IRepositorioGeneros repositorio) =>
{
    return await repositorio.ObtenerTodos();
}).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(15)));

app.MapGet("/generos/{id:int}", async(IRepositorioGeneros repositorio, int id) =>
{
    var genero = await repositorio.ObtenerPorId(id);

    if (genero is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(genero);
});

app.MapPost("/generos", async(Genero genero, IRepositorioGeneros repositorio) =>
{
    var id = await repositorio.Crear(genero);
    return Results.Created($"/generos/{id}", genero);
});

#endregion

#endregion

app.Run();
