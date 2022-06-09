using Microsoft.Extensions.Configuration;

namespace EfCore.Console
{
    public class Initiliazer
    {
        public static IConfigurationRoot Configuration;
        //public static DbContextOptionsBuilder<AppDbContext> DbContextOptionsBuilder;

        static Initiliazer()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
            //DbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //DbContextOptionsBuilder.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
        }
    }
}