using FoodieApp.DataServices;
using FoodieApp.DBAccess;
using FoodieApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<DbAccess>();
builder.Services.AddTransient<DataAccessServices>();
string sqlbuild = builder.Configuration.GetConnectionString("DBCon");
builder.Services.AddDbContext<FoodieDbContext>(cfg =>
{
    cfg.UseSqlServer(sqlbuild);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Search}/{action=SearchFood}/{id?}")
    .WithStaticAssets();


app.Run();
