using System.Reflection;
using EventsHub.Api.Controllers;
using EventsHub.Persistence;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// The documentation host loads the real controllers but should not open the
// API's SQLite database. This satisfies controller activation for the Swagger
// host while keeping it isolated from the running API.
services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("EventsHub.OpenApi"));

services
    .AddOpenApiDocument(document =>
    {
        document.DocumentName = "EventsHub";
        document.Title = "EventsHubV1"; // Official interface name. No spaces. PascalCase.
        document.Version = "1.0.0";
        document.DefaultResponseReferenceTypeNullHandling =
            NJsonSchema.Generation.ReferenceTypeNullHandling.NotNull;
    });

var pluginAssembly = Assembly.GetAssembly(typeof(WeatherForecastController));
services.AddMvc()
    .AddApplicationPart(pluginAssembly!)
    .AddControllersAsServices()
    .AddNewtonsoftJson(options =>
    {
        // Match the API's camelCase JSON output.
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    });

var app = builder.Build();
app.UseOpenApi();
app.UseSwaggerUi();
app.MapControllers();
app.Run();
