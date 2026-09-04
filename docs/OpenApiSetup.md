# OpenAPI and typed client

`EventsHub.OpenApi` is a small, standalone host that discovers the API
controllers and serves the OpenAPI document and Swagger UI. It is only a
documentation host: it uses an in-memory `AppDbContext` so it never opens or
modifies the API's SQLite database.

## Files

| Path | Purpose |
| --- | --- |
| `src/EventsHub.OpenApi/` | Host for Swagger/OpenAPI generation |
| `src/openapi/EventsHub.v1.json` | Generated OpenAPI document; do not edit manually |
| `src/nswag/EventsHub.nswag` | Checked-in NSwag configuration |
| `src/EventsHub.OpenApi/Generated/EventsHubRpcClient.generated.cs` | Generated C# client; do not edit manually |

## Prerequisites

From the repository root, restore the local NSwag tool:

```sh
dotnet tool restore
```

The project targets .NET 10, so the tool configuration uses the `Net100`
runtime and NSwag 14.7.1.

## Regenerate the document and client

1. Start the documentation host in one terminal:

   ```sh
   dotnet run --project src/EventsHub.OpenApi/EventsHub.OpenApi.csproj --no-launch-profile --urls http://127.0.0.1:5011
   ```

2. Fetch the live document while that host is running:

   ```sh
   curl -fsS http://127.0.0.1:5011/swagger/EventsHub/swagger.json -o src/openapi/EventsHub.v1.json
   ```

   In PowerShell, use:

   ```powershell
   Invoke-WebRequest http://127.0.0.1:5011/swagger/EventsHub/swagger.json -OutFile src/openapi/EventsHub.v1.json
   ```

3. Stop the host with `Ctrl+C`, then generate the typed client:

   ```sh
   dotnet tool run nswag run src/nswag/EventsHub.nswag
   ```

4. Verify the generated code still builds:

   ```sh
   dotnet build EventsHub.slnx
   ```

The JSON document and the `.generated.cs` file are outputs of this pipeline.
Commit their regenerated versions when the API contract changes, but never edit
them by hand.
