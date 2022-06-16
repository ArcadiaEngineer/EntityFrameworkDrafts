# Microsoft Entity Framework Features
## _Features which are mentioned in this project is here_ 

<br/>

## Topics

<br/>


| Section | Name |
| ------ | ------ |
| 1 | Database First |
| 2 | Code First (Migration)|
| 3 | DbContext |
| 4 | RelationShip |
| 5 | Related Data Load |
| 6 | Inheritance |
| 7 | Model |
| 8 | Query |
| 9  | Store Procedure / Functions |
| 10 | Projections |
| 11 | Transaction |
| 12 | Isolation Levels |
| 12 | Concurrency |

<br/>

## General Informations and Terms
- __DbContext__ : It corresponds to the your database
- __DbSet<T>__ :  It corresponds to the tables of your database
- __OnConfiguring__ : By overriding this method you can set your configuration settings int his method 

<br/>

You can give your ___connection string___ both within __OnConfiguring__ method or from __appSettings.json__ file
If you work with a __console project__ to be able to read your connection string from __appSettings.json__ you need to some libraries:
- Microsoft.Extensions.Configuration
- Microsoft.Extensions.Configuration.FileExtensions
- Microsoft.Extensions.Configuration.Json
 

See how can you read your ___connection string___ from  __appSettings.json__ [Initiliazer.cs](https://github.com/ArcadiaEngineer/EntityFrameworkStudies/blob/master/EfCore.Console/Initiliazer.cs)

If you work with a WebAPI or WebMVC project you can reach your ___connection string___ by using
```sh
   builder.Configuration.GetConnectionString("<Your Tag Name>")
```
<br/>

## 1- Database First

- This way is not best practice to design database.
- Most probably you will expend more energy to design the __Database__ and __Code Side__
- You have to fix and edit your entities manually when you __update__ your __Database__
- However, there may be some appropriate time to use __DatabaseFirst__
    - This time you can create your database manually and then by using __Scaffold Command__
you can scaffold your database in  __Code Side__

        ```sh
        Scaffold-DbContext <Your Connection String> <Your Database Provider>(ex: Microsoft.EntityFrameworkCore.SqlServer) -OutputDir <Your Direction>
        ```
    
## 2- Code First  

You can create your database with code first way. By using __attributes__ and __fluent api__ you can configure your database design.

- Create your entity classes
- Configure database with __attributes__ or __fluent api__
- Create the database with the settigs by using ___migrations___

<br/>
Migration commands: 
- __ADD-MIGRATION _<Migration Name>___
    - Creates a migration file which involves Database settings regarding to the your configuration
- __UPDATE-DATABASE__
    - Reflects the migration to the Database
- __REMOVE-MIGRATION__
    - Removes the last migration
- __SCRIPT-MIGRATION__
    - It gives the migrations file as sql script


## 3- DbContext

__DbContext Methods__: When you use __DbContext__ methods which are: 

- Add
- Update
- Remove
- Find

__EntityFramework__ marks the entities with a tag regarding to the method and tracks them in the memory:

- __Added__
    - When an entity will be added, it is marked with _added_ tag
- __Deleted__
    - When an entity will be removed, it is marked with _deleted_ tag
- __Modified__
    - When an entity will be updated, it is marked with _modified_ tag
- __Unchanged__
    - When an entity will be listed without __AsNoTracking__ Method, it is marked with __unchanged__ tag
- __Detached__
    - When there is an entity but it has no relation with EfCore, it is marked with __detached__ tag

Whenever you call __DbContext.SaveChanges()__ or __DbContext.SaveChangesAsync()__ methods then
__EntityFramework__ reflects them to the database.

There is a method which is provided by EfCore to see __Tracked Entities__ and called __ChangeTracker__.. You can reach the entities and their states.

__Ex:__
```sh
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
```

For configuration of tables there are two ways as we mentioned above. (__attributes__ and __fluent api__)

You can choose one of them to use. They are equivalent statemenets with different ways.
__Ex:__

| Data Annotations Attributes | Fluent API |
| ------ | ------ |
| [Table] | ToTable() |
| [Column] | HasColumnName() |
| [Required] | IsRequired() |
| [MaxLength] | HasMaxLength() |
| [ - - - ] | - - - ( ) |
| [StringLength()] | HasMaxLength() |

## 4- Relationships

** If you follow EntityFramework naming convensions you don't need additional confiuration to set relationships but if you dont, you should specify your relationships by using __Data Annotations Attributes__ or __Fluent API__

 __One To Many__ : 
    - Principal entitiy: Parent entity of child entity.
    - Dependent entity: Child entity which has a foreign key of principal entity.

Navigation property ex:

__Principal__
```sh
  public class Category
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public virtual List<Product> Products { get; set; }
    }
```
__Dependent__
```sh
  public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
```

 __One To One__ : 

You can set ProductFeature class's id as a both primariy key and foreign key or create additional property ProductId to keep Id of Product

Navigation property ex:

__Dependent__
```sh
  public class ProductFeature
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Color { get; set; }
        public virtual Product Product { get; set; }
    }
```
__Principal__
```sh
  public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
```
    
__Many To Many__ : 

When you create many to many relationship, there is need for an extra table. Ef core creates this table when you set many to many relationship. Also you can manipulate this table's name or column names.

Navigation property ex:

```sh
  public class Student
    {
        public int Id { get; set; }
        public string StudentNumber { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
```
```sh
  public class Teacher
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public List<Student> Students { get; set; }
    }
```
And Ef Core creates a table like:
|  StudentsId  | TeachersId  |
| ------ | ------ |
| id | id |
| id | id |
| - - | - - |

__Data Add__ :

You can add data to tables by using navigation properties of entities:

__Ex:__
```sh
context.Categories.Add(new Category
    {
        Name = "Books",
        Products = new List<Product>
        {
            new Product
            {
                Name = "Book1",
                Stock = 10,
                Price = 15,
                ProductFeature = new ProductFeature
                {
                    Color = "Red",
                    Height = 5,
                    Width = 5
                }
            },
            new Product
            {
                Name = "Book2",
                Stock = 20,
                Price = 10,
                ProductFeature = new ProductFeature
                {
                    Color = "Blue",
                    Height = 7,
                    Width = 7
                }
            }
        }
    });
```

You can put a constarint to prevent data adding to the parent entities from the child entities.
In order to achive this remove navigation properties from the child one.

__Delete Behaviors__ :

- _Cascade_:
    -When you delete the parent entity, it deletes entities which holds the parent id as a foreign key
- _Restrict_:
    -When you attempt to delete the parent entity, if there is a related entitiy with it, it does not allow to delete the parent
- _Set Null_:
    -When you delete the parent entity, if there is a related entitiy with it and foreign key field of related entitiy is nullable, Ef Core sets its foreign key null
- _No Action_:
    -Ef Core does not interfere and leaves the responsibility to the database


## 5- Related Data Load

- __Eager Loading__: 
    -You can retrieve all data at once by using navigation properties and __Include__ methods
    Ex:
    ```sh
    var result = context.Products.Include(p => p.ProductFeature).ThenInclude(pf => pf.Product).Include(p => p.Category).FirstOrDefault();
    ```
- __Explicit Loading__: 
    -You can retrieve data by using navigation properties and __Collection__ or __Referance__ methods according to the their relationships and __Load__ Method
    Ex:
    ```sh
    var result = context.Categories.FirstOrDefault();
    context.Entry(result!).Collection(c => c.Products).Load();
    ```
- __Lazy Loading__: 
    -You can retrieve data by using navigation properties. There is no method for lazy loading.
     When you use Navigation properties Ef Core loads data at that time by fetching from database each time you use Navigation properties.

    To enable this feature:
    * First you need to install __Microsoft.EntityFrameworkCore.Proxies__ library
    * And you need to mark your navigation properties with __virtual__ keyword
   
    ```sh
        optionsBuilder.UseLazyLoadingProxies();
    ```

## 6- Inheritance

__Table Per Hierarchy__ :

- If you have a base class and its child classes and you dont add base class as a __DbSet__, your tables are created with base class properties. It is default behavior of Entity Framework for TBT.
- If you have a base class and its child classes and you add base class as a __DbSet__, there will be only one table and it holds all childrens properties as a column but they will be nullable. To be able to discriminate them, the table holds a column called deliminator. It is default behavior of Entity Framework for TBT.

__Table Per Type__ :

If you have a base class and its child classes and you add base class and its children as a __DbSet__, there will be tables for each classes and base class contains mutual columns , childrens contains their spesific columns.

![ss](https://user-images.githubusercontent.com/89700270/174060519-30dbe722-f21d-4078-8728-2868093c2dcf.PNG)


## Remained Topics

__//You can reach each of them from :__
<h4> __[Program.cs](https://github.com/ArcadiaEngineer/EntityFrameworkStudies/blob/master/EfCore.Console/Program.cs)__ </h4>
<h4> __[AppDbContext.cs](https://github.com/ArcadiaEngineer/EntityFrameworkStudies/blob/master/EfCore.Console/Dal/AppDbContext.cs)__</h4>

__For methods you can look at _Program.cs_ for their configuration _AppDbContext___

## For Concurrency Handling see
<h4> __[Concurrency Handling](https://github.com/ArcadiaEngineer/EntityFrameworkStudies/tree/master/ConcurrencyHandling)__</h4>





Bu kaynağı https://www.udemy.com/course/entity-framework-core-sifirdan-zirveye/ kursundan yararlanarak hazırlamaya çalıştım. Daha fazlasını Türkçe olarak orada bulabilirsiniz.


