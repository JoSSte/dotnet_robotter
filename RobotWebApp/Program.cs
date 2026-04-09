using Microsoft.Extensions.DependencyInjection;
using RobotWebApp.Robot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddSingleton<IRobotController, RobotControllerImpl>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

// Log all endpoints to the console on startup TODO: not working. shows no endpoints
var endpointDataSource = app.Services.GetRequiredService<EndpointDataSource>();
var endpoints = endpointDataSource.Endpoints.OfType<RouteEndpoint>();

Console.WriteLine("\n=== Available Endpoints ===");
foreach (var endpoint in endpoints.OrderBy(e => e.RoutePattern.RawText ?? ""))
{
    var methods = endpoint.Metadata.OfType<HttpMethodMetadata>().FirstOrDefault();
    var methodsText = methods?.HttpMethods.Any() == true 
        ? string.Join(", ", methods.HttpMethods) 
        : "ANY";
    Console.WriteLine($"{methodsText,-7} {endpoint.RoutePattern.RawText}");
}
Console.WriteLine("===========================\n");

app.Run();