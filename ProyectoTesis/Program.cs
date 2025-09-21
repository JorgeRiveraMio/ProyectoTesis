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

    connectionString = builder.Configuration.GetConnectionString("DefaultConnectionJorge");
}
else
{
    throw new Exception("No se ha definido un Environment válido (usa 'J' o 'M').");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
