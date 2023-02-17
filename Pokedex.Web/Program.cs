using Application.Contracts;
using Application.Services;
using Database;
using Database.Interfaces;
using Database.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PokeContext>(options =>
                            options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));

builder.Services.AddScoped<ITypeService, TypeService>();
builder.Services.AddScoped<IPokemonService, PokemonService>();
builder.Services.AddScoped<IRegionService, RegionService>();

builder.Services.AddScoped<ITypeRepository, TypeRepository>();
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddScoped<IRegionRepository, RegionRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
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
