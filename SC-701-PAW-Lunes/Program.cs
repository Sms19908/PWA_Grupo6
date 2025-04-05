using Microsoft.EntityFrameworkCore;
using SC_701_PAW_Lunes.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar DBContext con SQL Server
builder.Services.AddDbContext<PAWDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PAWDbContext>();
    db.Database.Migrate(); // Applies any pending migrations
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
