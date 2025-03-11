using BusinessLogicLayer.Mapping;
using BusinessLogicLayer.Services.Classes;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer;
using BusinessLogicLayer;
using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Initializer;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBLLLayer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<ShopDbContext>();
        Initializer.InitializeDatabase(context);

        var seeder = serviceProvider.GetRequiredService<SeedDatabase>();
        seeder.Seed(context);
    }
    catch (Exception)
    {
        throw;
    }
}


app.UseSwagger();
app.UseSwaggerUI();

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
