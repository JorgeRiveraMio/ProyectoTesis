using Microsoft.EntityFrameworkCore;
using ProyectoTesis.Data;
using QuestPDF.Infrastructure; 
using System.Diagnostics.Eventing.Reader;
using ProyectoTesis.Services;


var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ProyectoTesis.Services.PdfService>();
builder.Services.AddScoped<ProyectoTesis.Services.EmailService>();
builder.Services.AddHttpClient<PythonApiService>();


#region conexion base de datos
var envCustom = builder.Configuration["Environment"];

string connectionString;

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
#endregion

#region Sesion
// Agregar servicios de sesión
builder.Services.AddDistributedMemoryCache(); // almacén en memoria
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".VocacionalApp.Session"; // nombre de la cookie
    options.IdleTimeout = TimeSpan.FromHours(1);    // duración de la sesión
    options.Cookie.HttpOnly = true;                 // solo accesible desde servidor
    options.Cookie.IsEssential = true;              // no bloqueado por GDPR
});
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // 🔹 Muestra detalles del error en desarrollo
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
