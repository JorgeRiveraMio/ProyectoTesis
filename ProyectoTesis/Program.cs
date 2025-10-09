using Microsoft.EntityFrameworkCore;
using ProyectoTesis.Data;
using QuestPDF.Infrastructure;
using ProyectoTesis.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

// ================================================
// 🔧 Forzar entorno Railway para EF Core CLI
// ================================================
Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");

if (builder.Configuration["Environment"] == null)
{
    builder.Configuration["Environment"] = "Railway";
}

// ================================================
// Servicios del contenedor
// ================================================
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PdfService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddHttpClient<PythonApiService>();

// ================================================
// Conexión a base de datos (Railway o local)
// ================================================
string connectionString = string.Empty;
var envCustom = builder.Configuration["Environment"];
var railwayConnection = Environment.GetEnvironmentVariable("DATABASE_URL");

Console.WriteLine("=========== DEBUG CONEXIÓN ===========");
Console.WriteLine($"Environment: {envCustom}");
Console.WriteLine($"DATABASE_URL (env): {railwayConnection ?? "(null)"}");
Console.WriteLine("======================================");

if (!string.IsNullOrEmpty(railwayConnection))
{
    // Limpia protocolo y query
    if (railwayConnection.StartsWith("postgresql://"))
        railwayConnection = railwayConnection.Replace("postgresql://", "postgres://");

    var cleanUrl = railwayConnection.Split('?')[0];

    try
    {
        // ✅ Parseo manual robusto (sin Uri)
        var withoutProtocol = cleanUrl.Replace("postgres://", "");
        var parts = withoutProtocol.Split('@');
        var creds = parts[0].Split(':', 2);
        var hostPortDb = parts[1].Split('/', 2);

        var hostPort = hostPortDb[0].Split(':', 2);
        var host = hostPort[0];
        var port = hostPort.Length > 1 ? hostPort[1] : "5432";
        var database = hostPortDb[1];

        connectionString =
            $"Host={host};Port={port};Database={database};Username={creds[0]};Password={creds[1]};SSL Mode=Require;Trust Server Certificate=true;";

        Console.WriteLine($"[DEBUG] ConnectionString construido: {connectionString}");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
    }
    catch (Exception ex)
    {
        throw new Exception($"❌ Error al parsear DATABASE_URL: {railwayConnection}\nDetalles: {ex.Message}");
    }
}
else if (envCustom == "Railway")
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnectionRailway")
        ?? throw new Exception("No se encontró la cadena de conexión Railway en appsettings.json.");

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else if (envCustom == "J")
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnectionJorge")
        ?? throw new Exception("No se encontró la cadena de conexión de Jorge.");

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
}
else if (envCustom == "M")
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnectionMarco")
        ?? throw new Exception("No se encontró la cadena de conexión de Marco.");

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
}
else
{
    throw new Exception("No se ha definido un Environment válido (usa 'J', 'M' o 'Railway').");
}

// ================================================
// Configuración de sesión
// ================================================
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".VocacionalApp.Session";
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

// ================================================
// Configuración del pipeline HTTP
// ================================================
var app = builder.Build();

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

// ================================================
// Ejecutar migraciones automáticamente al iniciar
// ================================================
Console.WriteLine($"[DEBUG] ConnectionString final antes de migrar: {connectionString}");
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();
