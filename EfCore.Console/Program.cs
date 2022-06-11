// See https://aka.ms/new-console-template for more information


using EfCore.Console.Dal;
using Microsoft.EntityFrameworkCore;

using (var context = new AppDbContext())
{
    //AddEntity(context);

    //FindEntitiesWithChangeTracker(context);
    //EagerLoading(context);
    //ExplicitLoading(context);
    //AddProduct(context);
    //KeylessTables(context);



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