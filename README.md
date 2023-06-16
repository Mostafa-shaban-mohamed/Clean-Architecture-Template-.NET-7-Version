# Clean-Architecture-Template-.NET-7-Version
This is clean architecture template in ASP.NET (.NET 7+ / First Version)

## Description
This is Template for Clean Architecture Design for my future projects.
I though of sharing it with other developers whose had my problem of finding a ready-on-developement template for clean architecture
instead of designing new one from scratch. So I told myself let me design one from scratch and then reuse it in future projects.

## Layers
Clean Archiecture in this template consists of 4 layers:

#### Domain Layer
The domain layer in the clean architecture contains the enterprise logic, like the entities and models.
This layer lies in the center of the architecture where we have application entities, which are the application model classes or database model classes,
using the database first approach (as in this template) in the application development using Asp.net core these entities are used to carry the data of models from and to database tables.
This layer consists of Common folder (contains all general dtos for lists and paging & authentication helper for authorization in project),
Enums (all enums used in project), Interfaces (one of the most important in this project it IRepo) and Models (entities of tables in DB). 

#### Application Layer
The application layer contains the business logic. All the business logic will be written in this layer.
It is in this layer that services interfaces are kept, separate from their implementation, for loose coupling and separation of concerns.
This layer consists of Appservices (example in code is USER), MappingProfile.cs (to handle mapping of dtos to model) and RepositoryWrapper.cs 
(using repository design pattern, it contains all repos of models of whole project)

#### Infrastructure Layer
In the infrastructure layer, we have model objects we will maintain all the database migrations (in case of code first approach)
and database context Objects in this layer. In this la yer, we have the  Entity framweork implementation of repositories of models.
This layer consists of Context (contains ApplicationDbContext which we add DBset of models to connect with the tables in DB),
Repositories (EF implementation of Repo Design pattern in project), appsetting.json (defaultconnectiostring) and if you use code first approach,
you will have migration folder contains all migration in database. 
<strong>(in this version, I tested database first approach and It works fine, but I didn't try code first...)</strong>
And Dependency Injection (contains added appservices, dbconext, RepoWrapper, any added technology such as Blobstorage or localization....).

#### WEB API with Controllers
As I didn't desgin a Presentation Layers, This is my Presentation Layer. A Swagger API representation of endpoints APIs in this project (in example UserController).

##### Important Note
Please give this project a star and help it to reach other developers (especially young developers and starter in .NET development).
And If there is any comment on this project or advice or additional feature you would like to add leave a comment on the project or contact me.

#### I hope this will be in help for all the developers out there :)



