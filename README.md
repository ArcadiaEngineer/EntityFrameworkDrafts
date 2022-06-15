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


<h5> Will be added Soon </h5>


    
    
