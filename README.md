# Što je ovo?
Projekt nastao slijeđenjem tutoriala [.NET Core 3.1 MVC REST API - Full Course](https://www.youtube.com/watch?v=fmvcAzHpsk8)

Boilerplate projekta je kreiran naredbom `dotnet new webapi`

# Bilješke

## Model
Model je klasa (`Command.cs`) koja opisuju strukturu podataka - dakle value object.

## Repository
Reppository je spremljen u direktoriju `Data` i definira samo API putem kojeg je moguće komunicirati sa aplikacijom.

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
