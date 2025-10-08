using Microsoft.EntityFrameworkCore;
using ProyectoTesis.Data;
using QuestPDF.Infrastructure;
using ProyectoTesis.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

// Servicios del contenedor
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PdfService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddHttpClient<PythonApiService>();

#region Conexión a base de datos (Railway o local)
var envCustom = builder.Configuration["Environment"];
string connectionString;

// Verifica si existe la variable de entorno DATABASE_URL (modo producción en Render)
var railwayConnection = Environment.GetEnvironmentVariable("DATABASE_URL");

if (!string.IsNullOrEmpty(railwayConnection))
{
    // Modo producción (Render + Railway)
    connectionString = railwayConnection;

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    // Modo desarrollo local (SQL Server)
    if (envCustom == "J")
    {
        connectionString = builder.Configuration.GetConnectionString("DefaultConnectionJorge");
    }
    else if (envCustom == "M")
    {
        connectionString = builder.Configuration.GetConnectionString("DefaultConnectionMarco");
    }
    else
    {
        throw new Exception("No se ha definido un Environment válido (usa 'J' o 'M').");
    }

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
}
#endregion

#region Configuración de sesión
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".VocacionalApp.Session";
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
#endregion

var app = builder.Build();

#region Configuración del pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
#endregion
