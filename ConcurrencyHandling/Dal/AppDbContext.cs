using ConcurrencyHandling.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcurrencyHandling.Dal
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.RowVersion).IsRowVersion();
            base.OnModelCreating(modelBuilder);
        }

    }
}
