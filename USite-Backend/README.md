# USite - BACKEND

## Migration

### Create a new migration

```
dotnet ef migrations add InitialCreate --project USite.Infrastructure --startup-project USite.Presentation --output-dir Persistence/Migrations --context CompleteDbContext
```

### Update the database

```
dotnet ef database update --project USite.Infrastructure --startup-project USite.Presentation --context CompleteDbContext
```

### Remove the last migration

```
dotnet ef migrations remove --project USite.Infrastructure --startup-project USite.Presentation
```

## Run the application

```
dotnet run --project USite.Presentation
```

## Run the tests

```
dotnet test
```

## Run the tests with coverage

```
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

## Run the tests with coverage and generate a report

```
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
reportgenerator "-reports:USite.Tests/coverage.opencover.xml" "-targetdir:USite.Tests/coverage" -reporttypes:Html
```