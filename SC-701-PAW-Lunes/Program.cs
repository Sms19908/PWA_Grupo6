using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SC_701_PAW_Lunes.Data;
using SC_701_PAW_Lunes.Services;
using SC_701_PAW_Lunes.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Cambio de idioma
builder.Services.AddLocalization();
var idiomasSoportados = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("es-ES"),
};
var localizationOptiones = new RequestLocalizationOptions();
localizationOptiones.SupportedCultures = idiomasSoportados;
localizationOptiones.SupportedUICultures = idiomasSoportados;
localizationOptiones.SetDefaultCulture ("es-ES");
localizationOptiones.ApplyCurrentCultureToResponseHeaders = true;

// Configurar DBContext con SQL Server
builder.Services.AddDbContext<PAWDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Simplified Identity configuration
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<PAWDbContext>()
.AddDefaultTokenProviders();

// Cookie configuration
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.SlidingExpiration = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Events.OnSigningOut = async context =>
    {
        await Task.CompletedTask;
        context.HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");
    };
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseRequestLocalization(localizationOptiones);

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PAWDbContext>();
    db.Database.Migrate();
}

// Combine all initialization into a single scope
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<PAWDbContext>();
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var sessionInvalidator = services.GetRequiredService<SessionInvalidationService>();

        await DbInitializer.Initialize(context, userManager, roleManager);
        await sessionInvalidator.InvalidateAllSessionsAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during initialization");
    }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();