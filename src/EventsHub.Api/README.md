# EventsHub.Api

The ASP.NET Core Web API for EventsHub — the only backend piece that's
actually part of the runtime request path (see
[`docs/Architecture.md`](../../docs/Architecture.md) for how it fits with
the rest of the system).

## What it does

- Exposes CRUD endpoints for `Event` over HTTP, backed by SQLite.
- Controllers are thin: each action builds a MediatR command/query from
  `EventsHub.Application` and sends it with `Mediator.Send(...)`. The
  handlers do the actual `AppDbContext` work.
- On every startup, applies pending EF Core migrations and seeds the
  database if it's empty (`Program.cs`, via `EventsHub.Persistence`) —
  failures here are logged, not fatal, so the app still starts even if
  migration/seeding failed.
- Allows cross-origin requests only from the Vite dev server
  (`localhost:3000`, http and https).

## Wiring (`Program.cs`)

- `AddDbContext<AppDbContext>` with `UseSqlite(ConnectionStrings:SqliteConnection)`
- `AddMediatR` scanning the `EventsHub.Application` assembly
- `AddMapper` scanning the `EventsHub.Application` assembly for mapping
  profiles (`MappingProfiles`), registered as a singleton `IMapper`
- `AddCors` + `UseCors` (any header/method, origins `http(s)://localhost:3000`)
- `MapControllers()`

This project references only `EventsHub.Application`; it reaches
`Persistence` and `Domain` transitively through it.

## Endpoints

All routes are versioned under `api/v1/` via the shared
`EventsHubBaseController` base class (`[Route("api/v1/[controller]")]`,
`[ApiController]`, and a protected `Mediator` property) — new controllers
should derive from it rather than declaring their own route.

| Method | Route | Description |
|---|---|---|
| GET | `/api/v1/Events` | List all events |
| GET | `/api/v1/Events/{id}` | Get one event by id |
| POST | `/api/v1/Events` | Create an event (body: `Event`); returns the new id |
| PUT | `/api/v1/Events` | Edit an event (body: `Event`, matched by its `Id`); `204` on success |
| DELETE | `/api/v1/Events/{id}` | Delete an event |
| GET | `/api/v1/WeatherForecast` | Unmodified ASP.NET template sample endpoint, re-parented onto the same base controller |

For the detail, edit, and delete endpoints, a missing event returns `404` with
the message `The event was not found`. The application handlers throw
`KeyNotFoundException`, which the corresponding controller actions translate
to `NotFound` responses.

## Running it

From the repo root:

```powershell
dotnet run --project src/EventsHub.Api
```

Serves on `https://localhost:5001` (see `Properties/launchSettings.json`).
The connection string (`ConnectionStrings:SqliteConnection`) points at a
local `eventshub.db` file, created/migrated automatically on startup.

## Testing

The solution has three test suites:

- **`tests/EventsHub.Application.UnitTests`** (NUnit) — verifies the custom
  mapper and its mapping profiles. Run with:

  ```powershell
  dotnet test tests/EventsHub.Application.UnitTests
  ```

- **`tests/EventsHub.UnitTests`** (NUnit) — calls the controllers in-process
  through MediatR, using a SQLite in-memory `AppDbContext` created and seeded
  once per run (`[SetUpFixture] GlobalTestSetup`). The current tests cover
  listing events, retrieving an existing event, and returning `404` for a
  missing event. Run with:

  ```powershell
  dotnet test tests/EventsHub.UnitTests
  ```

- **`tests/EventsHub.IntegrationTests`** — a [Bruno](https://www.usebruno.com/)
  collection (`.yml` requests under `Events/` and `WeatherForecast/`, plus
  a `local.yml` environment) that exercises the API over real HTTP. Run it
  with the Bruno CLI or app against a running `dotnet run` instance; it is
  separate from `dotnet test`.

Run both NUnit projects together with `dotnet test EventsHub.slnx`.

## Regenerating the OpenAPI client

If you change a controller's routes or an entity's shape, the typed RPC
client used elsewhere is now stale. Regeneration is driven from
`EventsHub.OpenApi`, not from this project — see
[`src/EventsHub.OpenApi/README.md`](../EventsHub.OpenApi/README.md).
