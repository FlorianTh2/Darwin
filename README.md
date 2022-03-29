# Darwin

A dotnet Microservice regarding identity management. This service does include functions like register, authentication with custom login and jwt creation, authorization with roles as containers for permissions. Fort that there is no external identity server needed.

## Learned

- entity framework fluent api
- permission management with roles and permission (roles as containers for permissions)
- geolocation postgres

## Prerequisites

- dotnet 6
- running postgres instance under `localhost:5432` with a created database called `hello-user-auth`, so that connection-string equals:
  `"PostgresLocal": "Host=localhost;Port=5432;Username=postgresUN;Password=postgresPW;Database=hello-user-auth;Integrated Security=true;Pooling=true;"`
- custom ./appsettings.json file according to the template ./appsettings.template.json
- vscode

## Getting Started

- `dotnet restore`
- `dotnet run`
- `http://localhost:5000/swagger`

## Important commands

- dotnet hot reaload
  - `dotnet watch`
- testing
  - `dotnet test`
- install ef global
  - `dotnet tool install --global dotnet-ef`
- more see ./commands.txt

## Build with

- dotnet 6
- nuget
- entity framwork
- asp.net identity
- fluent apis
  - fluent validations
  - ef fluent api
  - fluent assertions
- serilog
- open api
- postgres
