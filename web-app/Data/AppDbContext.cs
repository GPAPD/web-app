using Microsoft.EntityFrameworkCore;
using web_app.Data.Entity;

namespace web_app.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {

        }
        public DbSet<Product> Products{get; set;}
    }
}
