using Cruds.Domain;
using Cruds.Infra;
using Cruds.WebViewUI;
using Cruds.WebViewUI.Models;
using FluentMigrator.Runner;
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using LinqToDB.Common;
using LinqToDB.Configuration;
using LinqToDB.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepositoryCliente, RepoLinqTodb>();
builder.Services.AddRazorPages();
builder.Services.AddLinqToDbContext<AppDataConnection>((provider, options) =>
{
    options

     .UseSqlServer(builder.Configuration.GetConnectionString("Default"))

     .UseDefaultLogging(provider);


});

builder.Services.AddFluentMigratorCore().ConfigureRunner(config =>
     config.AddSqlServer()
     .WithGlobalConnectionString(@"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;")
     .ScanIn(Assembly.GetExecutingAssembly()).For.All()
      

) ;


builder.Services.AddDbContext<DbContextCliente>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddControllers();
DataConnection.DefaultSettings = new MySettings();

var app = builder.Build();



//using (var scope = app.ApplicationServices.CreateScope())
//{
//    var dataConnection = scope.ServiceProvider.GetService<AppDataConnection>();
//    dataConnection.CreateTable<Cliente>();
//}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//using var scope = app.ApplicationServices.CreateScope();
//var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
migrator.ListMigrations();
 
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
