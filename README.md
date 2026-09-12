# EventsHub

ICI 2026 01 — Web Services API built with ASP.NET Core 10 and Entity Framework Core (SQLite).

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Node.js 22+](https://nodejs.org/) (only for running Bruno integration tests)

## Getting started

```bash
# Clone the repository
git clone https://github.com/raulivan1200/Web-Services.git
cd Web-Services

# Restore dependencies
dotnet restore EventsHub.slnx

# Restore local .NET tools (NSwag)
dotnet tool restore

# Run the API
dotnet run --project src/EventsHub.Api/EventsHub.Api.csproj
```

The API starts at `https://localhost:5001` by default.

## Available endpoints

| Method | Route                      | Description              |
| ------ | -------------------------- | ------------------------ |
| GET    | `/api/v1/events`           | List all events          |
| GET    | `/api/v1/events/{id}`      | Get a single event by ID |
| GET    | `/api/v1/weatherforecast`  | Get weather forecasts    |

## Running integration tests (Bruno)

With the API running on a separate terminal:

```bash
cd tests/EventsHub.IntegrationTests
npx @usebruno/cli run . -r --env local --bail
```

## OpenAPI documentation

```bash
# Start the Swagger documentation host
dotnet run --project src/EventsHub.OpenApi/EventsHub.OpenApi.csproj

# Open http://localhost:5010/swagger in your browser
```

## Project structure

```
EventsHub.slnx
├── src/
│   ├── EventsHub.Api/             # REST API (controllers, Program.cs)
│   ├── EventsHub.Application/     # Application layer
│   ├── EventsHub.Domain/          # Domain entities (Event)
│   ├── EventsHub.OpenApi/         # Swagger/NSwag documentation host
│   └── EventsHub.Persistence/     # EF Core DbContext, migrations, seed data
├── tests/
│   └── EventsHub.IntegrationTests/  # Bruno API tests
└── docs/
    └── OpenApi.md                 # OpenAPI setup guide
```
