using Microsoft.EntityFrameworkCore;
using RazorPageControl_MVC.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("DefaultConnect");

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(connection);
});

builder.Services.AddRazorPages();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
