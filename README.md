# What's this?
This is a demo project created while following the [.NET Core 3.1 MVC REST API - Full Course](https://www.youtube.com/watch?v=fmvcAzHpsk8).

Boilerplate was created by running `dotnet new webapi`

# Installed Nuget packages
* `Microsoft.EntityFrameworkCore` - Entety Framework
* `Microsoft.EntityFrameworkCore.Design` - design-time components for Entity Framework Core tools
* `Microsoft.EntityFrameworkCore.SqlServer` - Entety Framework provider form MS SQL Server
* `AutoMapper.Extensions.Microsoft.DependencyInjection` - maps commands objects to DTOs and DTOs to commands
* `Microsoft.AspNetCore.JsonPatch` - support for PATCH HTTP request
* `Microsoft.AspNetCore.Mvc.NewtonsoftJson` - output formatter for PATCH request

# Notes

## Controller
Controller handlers client requests, executes business logica and prepares a response for the client.

Controllers are hooked up in the `Startup.cs` - in our example by calling `MapControllers`. Based on controller's `Route` annotation a new route will be created and assigned to that controller.

A controllers is instantiated automatically when the corresponding route gets triggered.

### Controller annotations
Controller can to be decorated with:
* `ApiController` - gives us some out-of-the-box functionallity
* `Route` - binds controller to a certain URL - the controller will be activated when the URL gets triggered

Controller methods which are exposed to the internet are also annotated:
* `HttpGet` - method will be called only for GET request
* `HttpPost` - method will be called only for POST request

Via annotation a method can be bound to a sub-url annotation, which can also contain parameters (npr `HttpGet("{id}")`) which will be passed to method as a parameter

## Model
Model is set of classed (i.e. `Command.cs`) which (a) describes structure of a data element and (b) is used to store data (value object).

### Model annotationsa
Model properties can be annotated which will be used:
* by *migrations* when creating database structures
* by de-serializer when **validating data** received form the client

Here are a few examples:
* `Key` = property is a unique key
* `Required` = value can not be null
* `MaxLength` = max length of a field

## Repository
Repository is a middleware which defines API through which controllers can communicate with the database.
It hides the implementation details of the database access logic.

In this project repository is stored in the `Data` directory. It consists of:
* interface definition - this defines the API
* interface implementation specific to a database

Repository registered as a service in `Startup.cs` and gets injected into controller as a dependency.

By an interface to define API the implementation is de-coupled from the Controller, which then enables the implementation to be easily swapped.

## Entety Framework
Entery Framework uses the following flow of data:
```
    SQL Database -> EF -> DbContext -> Repository -> Controller
```
* `Controller` - initiates data fetch by calling a Repository method
* `Repository` - fetches the data by calling a method of a `DbSet` object in `DbContext`
* `DbContext` - it does nothing - it's just a C# representation of DB data structure
* `EF` - entety framework talks to the database and returns the requested data

Here's what each of the object does:
* `Entety Framework` is basically a bridge which maps database structures to .Net objects
* `DbContext` defines the structure of the data, which matches the structure in DB
* `Repository` - implements the API which Controller can call + gets the data via EF
* `Controller` - presents the data to the public

### EF Tools
EF tools help us create a database structure which matches the one defined by `DbContext`.

This toolcan be installed via `dotnet tool install --global dotnet-ef`.

### Concepts: Migrations
*Migrations* is a set of C# classes generated which create a database matching the structure defined in `DbContext`.

The tool does this by looging first at `Startup.cs` where it finds the following:
* registration of `DbContext` of type `CommanderContext`
* `CommanderContext` defines `DbSet` of type `Command` -> this will be mapped to a DB table
* `Command` has public properties which will be mapped to table columns

The generated migration classes will be placed inside `Migrations` directory.

Tool is started via CLI: `dotnet ef migrations add InitialMigration` (*"InitialMigration"* is label we have given to this run - it can be anything)

If we have a look at the C# files we will see that it creates a DB structure (tables, columns) which matches the structure in our `Command.cs` model.

The generated migrations can be removed via `dotnet ef migrations remove`

#### Applying migrations to the database
To apply previously created migrations to a database we need to run `dotnet ef database update`.

For this project a database will be created with the name *"CommanderDB"*, which corresponds to the *"Initial Catalog"* from the connection string.
Inside it one table will be created names *"Commands"* which corresponds to `DbSet` name from `CommanderContext.cs`.

In addition to tables corresponding to the Model Entety Framework also creates a `[__EFMigrationsHistory]` table which will contain all the previously applied migations.

## Entety Framework Problem & Data Transfer Object
One problem with Entety Framework is that the whole internal database structure gets directly exposed to the public via controllers.

This has multipke downsides:
* not all feelds need or should be sent do clients
* data contract is tightly coupled to our internal implementation - each change in implementation breaks the data contract

These problem is solved by de-coupling implementation from data contract via *Data Transfer Objects* (DTO).

*Data Transfer Object* is a representation of model.

In this project we're using `AutoMapper`, which does the mapping heavy lifting.

### DTO Annotations
Like model the Data Transfer Object properties can also be annotated.

Here the annotations are used only by the de-serializer to validate the data received from the client (i.e. in CREATE operation).

If the validation fails the server will return a *400 Bad Request* HTTP response.


## Database
For this project to run we need an MS SQL Server.

To simplify things a dockerized version of the server has been added. Have a look at the `ms-sql-server` directory.

Admin credentials are:
* login = sa
* pass = cveZ8MzjH5AeYPVe

A SQL login was created with the following credentials:
* login = CommanderAPI
* pass = tM6vPK4dBux5rqgx

# HTTP Request types
`PUT` and `PATCH` both modify a record.
The difference is that `PUT` requires all the properties of an object to be shipped each time, despite the fact that some might have not changes.
Patch on the other hand is more flexible, meaning that only the modified fields need to be changed.

**Note:** the `id` property is not part of the request body - it's shipped as part of URL (i.e. PUT `/api/commands/1`).

## PATCH
Request body of a PATCH request is formated according to a special format / protocol: it contains one or multiple JSON blocks of the following format:
```json
{
    "op": "replace",
    "path": "/howto",
    "value": "new howto value"
}
```
Each block defines a modification one property of an object. Object is identified by ID which is passed in URL querystring.

# ToDo
* continue watching the video from 3:03:47
* read the documentation about Asp.Net middleware, with focus on `UseRouting`, `UseEndpoints`
