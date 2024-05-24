
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentaCar.DAL;
using RentaCar.Helpers;
using RentaCar.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IFileService, FileService>();


var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));
#region User
builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.Password.RequireNonAlphanumeric = true;
        //options.Password.RequiredUniqueChars = 6;
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;

        options.User.RequireUniqueEmail = true;
        options.Lockout.MaxFailedAccessAttempts = 3;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(3);
    })
.AddEntityFrameworkStores<AppDbContext>();
#endregion
#region app
var app = builder.Build();
app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=account}/{action=login}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}"
    );

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using (var scope = scopeFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    await DbInitializer.SeedAsync(userManager, roleManager);
}

app.Run();
#endregion
