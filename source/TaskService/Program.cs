using Domain;
using System.Text.Json.Serialization;
using TaskService.Endpoints;
using TaskService.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();

var app = builder.Build();

app.MapGet("/", () => Results.Ok("TaskService is running."));
app.MapTaskEndpoints();

app.Run();
