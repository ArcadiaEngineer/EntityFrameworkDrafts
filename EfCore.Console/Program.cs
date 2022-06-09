// See https://aka.ms/new-console-template for more information


using Microsoft.Extensions.Configuration;
using EfCore.Console.Dal;
using EfCore.Console;

var path = Initiliazer.Configuration.GetConnectionString("SqlServer");

using (var context = new AppDbContext())
{
    context.Add<Teacher>(new Teacher { Name = "Ali", Phone = "123456789", Age = 32 });
    context.Add<Teacher>(new Teacher { Name = "Veli", Phone = "9874456321", Age = 42 });

    context.Add<Student>(new Student { Name = "Veli", StudentNumber = "9874456321", Age = 42 });
    context.Add<Student>(new Student { Name = "Veli", StudentNumber = "9874456321", Age = 42 });

    //context.People.ToList();

    context.SaveChanges();

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


Console.WriteLine("Hello, World!");
