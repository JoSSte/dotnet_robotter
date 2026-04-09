using Microsoft.Extensions.DependencyInjection;
using RobotWebApp.RobotBoard;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson();

//This is singleton to share state and make testing easier.
builder.Services.AddSingleton<IRobotController, RobotControllerImpl>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // something is missing for this to work. shows no endpoints in swagger. TODO: fix
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();