// See https://aka.ms/new-console-template for more information


using EfCore.Console.Dal;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using (var context = new AppDbContext())
{
    //AddEntity(context);
    //AddProduct(context);

    //FindEntitiesWithChangeTracker(context);

    //EagerLoading(context);
    //ExplicitLoading(context);

    //KeylessTables(context);

    //InnerJoingWithMethod(context);
    //InnerJoinWithLINQ(context);

    //LeftRightJoins(context);

    //OuterJoin(context);

    Console.WriteLine("Hello World");

}


Console.WriteLine("Hello, World!");

static void AddEntity(AppDbContext context)
{
    context.Add<Teacher>(new Teacher { Name = "Ali", Phone = "123456789", Age = 32 });
    context.Add<Teacher>(new Teacher { Name = "Veli", Phone = "9874456321", Age = 42 });

    context.Add<Student>(new Student { Name = "Veli", StudentNumber = "9874456321", Age = 42 });
    context.Add<Student>(new Student { Name = "Veli", StudentNumber = "9874456321", Age = 42 });

    context.SaveChanges();
}

static void FindEntitiesWithChangeTracker(AppDbContext context)
{
    context.ChangeTracker.Entries().ToList().ForEach(entry =>
    {
        switch (entry.Entity)
        {
            case Teacher teacher:
                Console.WriteLine(teacher.Name + " " + teacher.Phone + " " + teacher.Age);
                break;

            case Student student:
                Console.WriteLine(student.Name + " " + student.StudentNumber + " " + student.Age);
                break;
            default:
                break;
        }
    });
}

static void EagerLoading(AppDbContext context)
{
    var result = context.Products.Include(p => p.ProductFeature).ThenInclude(pf => pf.Product).Include(p => p.Category).FirstOrDefault();
}

static void ExplicitLoading(AppDbContext context)
{
    var result = context.Categories.FirstOrDefault();
    context.Entry(result!).Collection(c => c.Products).Load();
}

static void AddProduct(AppDbContext context)
{
    context.Add<Product>(new Product { Name = "Kalem", Barcode = 122342, CategoryId = 1, DiscountPrice = 14, IsDeleted = false, Price = 234, Stock = 123 });
    context.Add<Product>(new Product { Name = "Kalem2", Barcode = 122124342, CategoryId = 1, DiscountPrice = 42, IsDeleted = false, Price = 2123, Stock = 1523 });
    context.Categories.Add(new Category { Name = "Kalemler" });

    context.SaveChanges();
}

static void KeylessTables(AppDbContext context)
{
    var result = context.FullProducts.FromSqlRaw(@"select p.Name , p.Price, p.Stock, p.DiscountPrice, c.Name as CategoryName from Products p
inner join Categories c
on p.CategoryId = c.Id").ToList();
}

static void InnerJoingWithMethod(AppDbContext context)
{
    var result = context.Categories.Join(context.Products, c => c.Id, p => p.CategoryId, (c, p) => new { c, p });
    var result2 = context.Categories.Join(context.Products, c => c.Id, p => p.CategoryId, (c, p) => new { c, p }).Join(context.ProductFeatures, a => a.p.Id, pf => pf.Id, (a, pf) => new { a, pf });
}

static void InnerJoinWithLINQ(AppDbContext context)
{
    var result = (from c in context.Categories
                  join p in context.Products on
                  c.Id equals p.CategoryId
                  select new { c, p }).ToList();

    var result2 = (from c in context.Categories
                   join p in context.Products on
                   c.Id equals p.CategoryId
                   join pf in context.ProductFeatures on
                   p.Id equals pf.Id
                   select new { c, p, pf }).ToList();
}

static void LeftRightJoins(AppDbContext context)
{
    var result = (from p in context.Products
                  join pf in context.ProductFeatures on
                  p.Id equals pf.Id into pfList
                  from pf in pfList.DefaultIfEmpty()
                  select new
                  {
                      ProductName = p.Name,
                      ProductPrice = p.Price,
                      ProductWidth = pf.Width,
                  }).ToList();
}

static void OuterJoin(AppDbContext context)
{
    var left = from p in context.Products
               join pf in context.ProductFeatures
               on p.Id equals pf.Id into pfList
               from pf in pfList.DefaultIfEmpty()
               select new
               {
                   ProductName = p.Name,
                   ProductPrice = p.Price,
                   ProductWidth = pf.Width,
               };
    var right = from pf in context.ProductFeatures
                join p in context.Products
                on pf.Id equals p.Id into pList
                from p in pList.DefaultIfEmpty()
                select new
                {
                    ProductName = p.Name,
                    ProductPrice = p.Price,
                    ProductWidth = pf.Width,
                };

    var outer = left.Union(right).ToList();
}