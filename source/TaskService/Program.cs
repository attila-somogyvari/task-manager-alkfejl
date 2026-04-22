using Domain;
using System.Text.Json.Serialization;
using TaskService.Configuration;
using TaskService.Endpoints;
using TaskService.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection(MongoDbSettings.SectionName));
builder.Services.AddSingleton<ITaskRepository, MongoTaskRepository>();

var app = builder.Build();

app.MapGet("/", () => Results.Ok("TaskService is running."));
app.MapTaskEndpoints();

app.Run();
