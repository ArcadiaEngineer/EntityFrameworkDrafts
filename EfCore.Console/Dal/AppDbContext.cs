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
            optionsBuilder.LogTo(C.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Table Per Type Configuration
            //modelBuilder.Entity<Person>().ToTable("People");
            //modelBuilder.Entity<Student>().ToTable("Students");
            //modelBuilder.Entity<Teacher>().ToTable("Teachers");
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
