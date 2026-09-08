# EventsHub

[![CI](https://github.com/raulivan1200/Web-Services/actions/workflows/ci.yml/badge.svg)](https://github.com/raulivan1200/Web-Services/actions/workflows/ci.yml)

ICI Web Services 2026 — EventsHub backend, OpenAPI client generation, Bruno
integration requests, and the Vite/React starter app.

## Requirements

- .NET SDK 10.0.400 (or another .NET 10 SDK)
- EF Core CLI 10.0.11 for creating or inspecting migrations:

  ```sh
  dotnet tool install --global dotnet-ef --version 10.0.11
  ```

- Node.js with npm for the optional `web/` Vite application
- Bruno to run the integration collection interactively

## Run the API

```sh
dotnet tool restore
dotnet build EventsHub.slnx
dotnet run --project src/EventsHub.Api/EventsHub.Api.csproj --launch-profile https
```

The API starts at `https://localhost:5001`. On its first run it applies the
SQLite migration and seeds ten events. The database files are intentionally
ignored by Git.

## Integration requests

Open `tests/EventsHub.IntegrationTests/` in Bruno and select the `local`
environment. Run the full collection: the Events list request captures a real
seeded event ID for the following `Events - Get - 200` request.

## OpenAPI client

See [docs/OpenApi.md](docs/OpenApi.md) to regenerate the checked-in OpenAPI
document and C# RPC client.

## Vite app

```sh
cd web
npm ci
npm run dev
```
