using Microsoft.EntityFrameworkCore;
using SC_701_PAW_Lunes.Models;

namespace SC_701_PAW_Lunes.Data
{
    public class PAWDbContext : DbContext
    {
        public PAWDbContext(DbContextOptions options) : base(options){}

        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Category> Category { get; set;}
    }
}
