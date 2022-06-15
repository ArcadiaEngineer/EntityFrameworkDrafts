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

You can create your database with code first way. By using __attributes__ and __fluent api__ you can configure your database design

- Create your entity classes
- Configure database with __attributes__ or __fluent api__
- Create the database with the settigs by using ___migrations___

<br/>
  
Migration commands: 
- ADD-MIGRATION <Migration Name>
    - Creates a migration file which involves Database settings regarding to the your configuration
- UPDATE-DATABASE
    - Reflects the migration to the Database
- REMOVE-MIGRATION
    - Removes the last migration
- SCRIPT-MIGRATION
    - It give the migrations file as sql script


## 3- DbContext

















    
    
