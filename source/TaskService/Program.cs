using Domain;
using System.Text.Json.Serialization;
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

app.MapGet("/tasks", (string? status, string? search) =>
{
    IEnumerable<TaskItem> result = tasks;

    if (!string.IsNullOrWhiteSpace(status) &&
        Enum.TryParse<TaskStatus>(status, true, out var parsedStatus))
    {
        result = result.Where(task => task.Status == parsedStatus);
    }

    if (!string.IsNullOrWhiteSpace(search))
    {
        result = result.Where(task =>
            task.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
            task.Description.Contains(search, StringComparison.OrdinalIgnoreCase));
    }

    return Results.Ok(result);
});

app.MapGet("/tasks/{id:guid}", (Guid id) =>
{
    var task = tasks.FirstOrDefault(task => task.Id == id);

    return task is not null
        ? Results.Ok(task)
        : Results.NotFound();
});

app.MapPost("/tasks", (TaskItem task) =>
{
    task.Id = Guid.NewGuid();
    task.CreatedAt = DateTime.UtcNow;
    task.UpdatedAt = DateTime.UtcNow;

    tasks.Add(task);

    return Results.Created($"/tasks/{task.Id}", task);
});

app.MapPut("/tasks/{id:guid}", (Guid id, TaskItem updatedTask) =>
{
    var existingTask = tasks.FirstOrDefault(task => task.Id == id);

    if (existingTask is null)
    {
        return Results.NotFound();
    }

    existingTask.Title = updatedTask.Title;
    existingTask.Description = updatedTask.Description;
    existingTask.Status = updatedTask.Status;
    existingTask.Priority = updatedTask.Priority;
    existingTask.DueDate = updatedTask.DueDate;
    existingTask.UpdatedAt = DateTime.UtcNow;

    return Results.Ok(existingTask);
});

app.MapPatch("/tasks/{id:guid}/status", (Guid id, TaskStatusUpdateRequest request) =>
{
    var existingTask = tasks.FirstOrDefault(task => task.Id == id);

    if (existingTask is null)
    {
        return Results.NotFound();
    }

    existingTask.Status = request.Status;
    existingTask.UpdatedAt = DateTime.UtcNow;

    return Results.Ok(existingTask);
});

app.MapDelete("/tasks/{id:guid}", (Guid id) =>
{
    var existingTask = tasks.FirstOrDefault(task => task.Id == id);

    if (existingTask is null)
    {
        return Results.NotFound();
    }

    tasks.Remove(existingTask);

    return Results.NoContent();
});

app.Run();

public record TaskStatusUpdateRequest(TaskStatus Status);
