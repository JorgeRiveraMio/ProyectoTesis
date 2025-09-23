using Microsoft.EntityFrameworkCore;
using ProyectoTesis.Data;
using System.Diagnostics.Eventing.Reader;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region conexion base de datos
var envCustom = builder.Configuration["Environment"];

string connectionString;

if( envCustom=="J")
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnectionJorge");
}else if (envCustom == "M")
{ 

    connectionString = builder.Configuration.GetConnectionString("DefaultConnectionMarco");
}
else
{
    throw new Exception("No se ha definido un Environment v�lido (usa 'J' o 'M').");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
#endregion


#region Sesion
// Agregar servicios de sesi�n
builder.Services.AddDistributedMemoryCache(); // almac�n en memoria
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".VocacionalApp.Session"; // nombre de la cookie
    options.IdleTimeout = TimeSpan.FromHours(1);    // cu�nto dura la sesi�n
    options.Cookie.HttpOnly = true;                 // solo accesible desde servidor
    options.Cookie.IsEssential = true;              // no bloqueado por GDPR
});


#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // 🔹 Esto muestra los detalles completos del error
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
