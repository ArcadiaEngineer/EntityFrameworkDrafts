using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using C = System.Console;

namespace EfCore.Console.Dal
{
    public class AppDbContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        public DbSet<FullProduct> FullProducts { get; set; }
        #region Keyless joined object
        /*
         * When we have a inner joined objects which are obtained from database, EfCore map them to the our DbSet
         */
        #endregion

        //public DbSet<Person> People { get; set; }
        #region Inheretince
        /*
         Person class is the super class of Student and Teacher
         If you don't change EfCore Convensions and dont add DbSet<Person> then apply the migration,
         EfCore creates two table for Student and Teacher and they inherents People class
         If you don't change EfCore Convensions and add DbSet<Person> then apply the migration,
         EfCore creates one table and add Deliminater Column to the table to distinguish them
         */
        #endregion

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        #region Many-To-Many Releationship
        /*
         Students and Teacher has Many to Many relationship
         When we add with EfCore Convensions it creates a new table and add two column which hold entities id values
         We can manipulate table and column names
         */
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = Initiliazer.Configuration.GetConnectionString("SqlServer");
            optionsBuilder.UseSqlServer(path);
            //optionsBuilder.LogTo(C.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

            #region LazyLoading
            /*optionsBuilder.UseLazyLoadingProxies(); Microsoft.EntityFramework.Proxies libraries
                We mark navigation properties with virtual keyword, and we can obtain entites' properties with lazy loading
                Each time we use the nav. prop. EfCore fetch from database*/
            #endregion
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Table Per Type Configuration
            //modelBuilder.Entity<Person>().ToTable("People");
            //modelBuilder.Entity<Student>().ToTable("Students");
            //modelBuilder.Entity<Teacher>().ToTable("Teachers");
            #endregion

            #region Owned Type Entity Configuration
            modelBuilder.Entity<Student>().OwnsOne(s => s.PhysicalPersonFeatures, ppf =>
                {
                    ppf.Property(p => p.Weight).HasColumnName("Weight");
                    ppf.Property(p => p.Height).HasColumnName("Height");
                });
            modelBuilder.Entity<Teacher>().OwnsOne(s => s.PhysicalPersonFeatures, ppf =>
            {
                ppf.Property(p => p.Weight).HasColumnName("Weight");
                ppf.Property(p => p.Height).HasColumnName("Height");
            });
            #endregion

            #region Keyless Entity
            modelBuilder.Entity<FullProduct>().HasNoKey();
            #endregion

            #region Unicode/Ignore
            modelBuilder.Entity<Product>().Property(p => p.Barcode).IsUnicode(false);
            modelBuilder.Entity<Product>().Ignore(p => p.DiscountPrice);
            #endregion

            #region Indexing
            modelBuilder.Entity<Product>().HasIndex(p => p.Price);//Only One
            modelBuilder.Entity<Product>().HasIndex(p => new { p.Price , p.Name});//Composite
            modelBuilder.Entity<Product>().HasIndex(p => p.Price).IncludeProperties(p => p.DiscountPrice);//Multiple Column
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
/*
 * EfCore doesnt track entities which has no key
 * EfCore doesnt track entities which has no key but we can read them
 */