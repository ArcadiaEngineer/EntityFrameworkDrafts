using EfCore.Console.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EfCore.Console.Dal
{
    public class AppDbContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        public DbSet<ProductEssentials> ProductEssentials { get; set; }

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

        public DbSet<ProductCount> ProductCount { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = Initiliazer.Configuration.GetConnectionString("SqlServer");
            optionsBuilder.UseSqlServer(path);
            //optionsBuilder.LogTo(C.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            //optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); Global No Tracking

            #region LazyLoading
            /*optionsBuilder.UseLazyLoadingProxies(); Microsoft.EntityFramework.Proxies libraries
                We mark navigation properties with virtual keyword, and we can obtain entites' properties with lazy loading
                Each time we use the nav. prop. EfCore fetch from database*/
            #endregion
        }
        public IQueryable<FullProduct> GetFullProducts(int categoryId) => FromExpression(() => GetFullProducts(categoryId));
        public int GetProductCount(int categoryID)
        {
            throw new NotSupportedException();//this method runs only when Ef core call it, we can not use it explicitly
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
            modelBuilder.Entity<Product>().HasIndex(p => new { p.Price, p.Name });//Composite
            modelBuilder.Entity<Product>().HasIndex(p => p.Price).IncludeProperties(p => p.DiscountPrice);//Multiple Column
            #endregion

            #region Constraint
            modelBuilder.Entity<Product>().HasCheckConstraint("PriceCheckDefault", "[Price] > [Price] - [Price] * [DiscountPrice] / 100");
            #endregion

            #region CustomSqlQueries
            modelBuilder.Entity<ProductEssentials>().HasNoKey();
            #endregion

            #region ToSqlQuery
            modelBuilder.Entity<ProductEssentials>().ToSqlQuery("Select Id, Name, Price, DiscountPrice from Products");
            #endregion

            #region ToView
            modelBuilder.Entity<ProductEssentials>().ToView("ViewName");
            #endregion

            #region GlobalFilters
            modelBuilder.Entity<Product>().HasQueryFilter(p => p.IsDeleted == false);
            //it is added all product queries
            #endregion

            #region ToFunction
            modelBuilder.Entity<FullProduct>().ToFunction("getFullProducts");//For function without parameters and returning Table
            #endregion

            #region FunctionWithMethod
            modelBuilder.HasDbFunction(typeof(AppDbContext).GetMethod(nameof(GetFullProducts), new[] { typeof(int) })!).HasName("getFullProductsWithParameter");
            #endregion

            #region FunctionReturnsScalerValueWithMethod
            modelBuilder.HasDbFunction(typeof(AppContext).GetMethod(nameof(GetProductCount), new[] { typeof(int) })!).HasName("getProductCount");
            #endregion

            #region ExplicitlyCallFunctionsReturnsScalarValue
            modelBuilder.Entity<ProductCount>().HasNoKey();
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
/*
 * EfCore doesnt track entities which has no key
 * EfCore doesnt track entities which has no key but we can read them
 * When we create db set but we use it for join results or custom queries, we need to delete their tables in migration
 * We can use LINQ with functions, while we dont be able to use LINQ with store procedures
 */