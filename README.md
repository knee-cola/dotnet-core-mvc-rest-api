# Što je ovo?
Projekt nastao slijeđenjem tutoriala [.NET Core 3.1 MVC REST API - Full Course](https://www.youtube.com/watch?v=fmvcAzHpsk8)

Boilerplate projekta je kreiran naredbom `dotnet new webapi`

# Bilješke

## Model
Model je klasa (`Command.cs`) koja opisuju strukturu podataka - dakle value object.

## Repository
Repository je middleware koji apstrahira komunikaciju s bazom, odnosno definira API putem kojeg controller komunicira sa bazom podataka.

Repo je smješten u `Data` direktoriju, u kojem se nalazi interface, kao i konkretna implementacija.

Sadrži jedan interface koji definira API koji controller koristi, te sadrži implementaciju tog interface-a.
Controller do repozitorija dolazi putem dependency injection-a.

Korištenje interface-a predstavlja weak coupling koji omogućuje zamjenu implementacije bez potrebe da se mijenja controller.

Ovo može doći do izražaja u slučaju ako želimo podržati spajanje na više različitih baza podataka, pri čemu će svako biti implementirano u drugoj repository klasi.

## Controller
Kontrolere *aktiviramo* tako da u `Startup` pozovemo `MapControllers` - to će na router nakačiti kontrolere koji su dekorirani sa `Route` atributom.

Kontroler će biti automatski instancirani pri čemu će rađen dependency injection.

### Dekoriranje controllera
Controller klasu dekoriramo sa:
* `Route` - omogućuje nam da zadamo URL na kojem će controller *slušati*
* `ApiController` - daje neke out-of-the-box funkcionalnosti

Metode koje želimo pozivati putem REST-a dekoriramo sa:
* `HttpGet` - funkcija će biti pozvana samo na GET request
* `HttpPost` - funkcija će biti pozvana samo na GET request

Dekoratoru možemo proslijediti sub-url na koji će metoda biti pozvana, pri čemu URl može biti parametriziran (npr `HttpGet("{id}")`).

## Dotnet EF
Za ovaj projekt je potrebno instalirati Entety Framework Core CLI Tools putem naredbe: `dotnet tool install --global dotnet-ef`.

## Database
Instaliran je MS SQL Server (Docker Container). Docker datoteke su smještene u direktoriju `ms-sql-server`.

Admin credentials su:
* login = sa
* pass = cveZ8MzjH5AeYPVe

 Kreiran je login putem kojeg će se korisnik spajati:
* login = CommanderAPI
* pass = tM6vPK4dBux5rqgx

## Concepts: Migrations
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

# ToDo
* pogledati ideo do kraja (sada sam na 56. minuti)
* proučiti Asp.Net middleware - općenito, ali i konkretno: `UseRouting`, `UseEndpoints`


