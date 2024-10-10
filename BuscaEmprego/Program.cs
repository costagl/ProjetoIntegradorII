using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Model.Models;
using Model.Services;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("Data Source=COSTA-GL\\SQLEXPRESS;Initial Catalog=db_BuscaEmprego;Integrated Security=True;TrustServerCertificate=True");

builder.Services.AddDbContext<db_BuscaEmpregoContext>(opt => opt.UseSqlServer(connectionString));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=Index}/{id?}");

app.Run();
