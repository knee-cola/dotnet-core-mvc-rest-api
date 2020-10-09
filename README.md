# What's this?
This is a demo project created while following the [.NET Core 3.1 MVC REST API - Full Course](https://www.youtube.com/watch?v=fmvcAzHpsk8).

Boilerplate was created by running `dotnet new webapi`

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

Model properties can be annotated which will be used by *migrations* when creating database structures. Here are a few examples:
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

## Dotnet EF
In this project we will be using *Entety Framework Core CLI Tools*, which can be installed via `dotnet tool install --global dotnet-ef`.

### Concepts: Migrations
List of instructions which tells our database how to create database schema which mirrors app's internal representation of data.

In this example it will look at CommanderContext and find `DbSet` of type `Command` called `Commands`.
Migration will replicate that design to our SQL server. If the database doesn't exist it will create one.

Migration is started via cli: `dotnet ef migrations add InitialMigration` ("InitialMigration" is label we have given to this run - it can be anything)

The command will only create a new directory `Migrations`, which contains a C# program which can be run to create structures on SQL Server (database, tables, indexes).

If we have a look at the c# files we will see that the structure (tables, columns) it creates match the structure in our `Command.cs` model.

The command did this by looking at `Startup.cs` where it finds the following:
* registration of `DbContext` of type `CommanderContext`
* `CommanderContext` defines `DbSet` of type `Command` -> this will be converted into a DB table
* `Command` has public properties which will be mapped to table columns

The generated migrations can be removed via `dotnet ef migrations remove`

## Database
For this project to run we need an MS SQL Server.

To simplify things a dockerized version of the server has been added. Have a look at the `ms-sql-server` directory.

Admin credentials are:
* login = sa
* pass = cveZ8MzjH5AeYPVe

A SQL login was created with the following credentials:
* login = CommanderAPI
* pass = tM6vPK4dBux5rqgx


# ToDo
* proučiti Asp.Net middleware - općenito, ali i konkretno: `UseRouting`, `UseEndpoints`


