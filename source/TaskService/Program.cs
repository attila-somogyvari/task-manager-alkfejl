using Domain;
using System.Text.Json.Serialization;
using TaskService.Endpoints;
using TaskItem = Domain.Task;
using TaskPriority = Domain.TaskPriority;
using TaskStatus = Domain.TaskStatus;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

var tasks = new List<TaskItem>
{
    new()
    {
        Id = Guid.NewGuid(),
        Title = "Projektstruktura kialakitasa",
        Description = "Repo szerkezet letrehozasa.",
        Status = TaskStatus.Done,
        Priority = TaskPriority.High,
        DueDate = DateTime.UtcNow.AddDays(1),
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new()
    {
        Id = Guid.NewGuid(),
        Title = "TaskService API vaz",
        Description = "Backend vegpontok kialakitasa.",
        Status = TaskStatus.InProgress,
        Priority = TaskPriority.High,
        DueDate = DateTime.UtcNow.AddDays(2),
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    }
};

app.MapGet("/", () => Results.Ok("TaskService is running."));
app.MapTaskEndpoints(tasks);

app.Run();
