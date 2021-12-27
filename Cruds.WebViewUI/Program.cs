using Cruds.Domain;
using Cruds.Infra;
using Cruds.WebViewUI;
using Cruds.WebViewUI.Models;
using FluentMigrator.Runner;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using LinqToDB.Configuration;
using LinqToDB.Data;
using Microsoft.EntityFrameworkCore;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Cruds.WebViewUI.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepositoryCliente, RepositoryClienteLinq>();
builder.Services.AddRazorPages();
builder.Services.AddLinqToDbContext<AppDataConnection>((provider, options) =>
{
    options

     .UseSqlServer(builder.Configuration.GetConnectionString("Default"))

     .UseDefaultLogging(provider);


});


builder.Services.AddLogging(c => c.AddFluentMigratorConsole())
    .AddFluentMigratorCore().ConfigureRunner(c => c.AddSqlServer()
    .WithGlobalConnectionString(@"Server=localhost\SQLEXPRESS;Database=dbClientes;Trusted_Connection=True;")
    .ScanIn(Assembly.GetExecutingAssembly()).For.All());


builder.Services.AddDbContext<DbContextCliente>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddControllers();

DataConnection.DefaultSettings = new MySettings();

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
app.UseDefaultFiles();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseFileServer();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

Database_Migration.EnsureDatabase("Persist Security Info = False; Integrated Security = true; Initial Catalog = master; server = .\\SQLEXPRESS", "dbClientes");
app.Migrate();
app.Run();
