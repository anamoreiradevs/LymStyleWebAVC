using LymStyleWebAPPAPI.Application.Service.SQLServerServices;
using LymStyleWebAPPAPI.Domain.Contracts.Repositories;
using LymStyleWebAPPAPI.Domain.Contracts.Services;
using LymStyleWebAPPAPI.Domain.Entities;
using LymStyleWebAPPAPI.Infra.Data.Repository.Contracts;
using LymStyleWebAPPAPI.Infra.Data.Repository.Repositories;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Context SQL Server
builder.Services.AddDbContext<SQLServerContext>
    (options => options.UseSqlServer("Server = ANALYDIAMOREIRA; Database = LymStyle; User Id = sa; Password = adm; TrustServerCertificate = True; Encrypt = False;"));

// ### Dependency Injection
// # Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

// # Services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAccountService, AccountService>();


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
