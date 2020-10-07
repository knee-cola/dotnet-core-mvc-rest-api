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

# ToDo
* pogledati ideo do kraja (sada sam na 56. minuti)
* proučiti Asp.Net middleware - općenito, ali i konkretno: `UseRouting`, `UseEndpoints`
