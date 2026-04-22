using Domain;
using TaskService.Requests;
using TaskItem = Domain.Task;
using TaskStatus = Domain.TaskStatus;

namespace TaskService.Endpoints;

public static class TaskEndpoints
{
    public static IEndpointRouteBuilder MapTaskEndpoints(this IEndpointRouteBuilder app, List<TaskItem> tasks)
    {
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

        app.MapPost("/tasks", (CreateTaskRequest request) =>
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                Priority = request.Priority,
                DueDate = request.DueDate,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            tasks.Add(task);

            return Results.Created($"/tasks/{task.Id}", task);
        });

        app.MapPut("/tasks/{id:guid}", (Guid id, UpdateTaskRequest request) =>
        {
            var existingTask = tasks.FirstOrDefault(task => task.Id == id);

            if (existingTask is null)
            {
                return Results.NotFound();
            }

            existingTask.Title = request.Title;
            existingTask.Description = request.Description;
            existingTask.Status = request.Status;
            existingTask.Priority = request.Priority;
            existingTask.DueDate = request.DueDate;
            existingTask.UpdatedAt = DateTime.UtcNow;

            return Results.Ok(existingTask);
        });

        app.MapPatch("/tasks/{id:guid}/status", (Guid id, UpdateTaskStatusRequest request) =>
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

        return app;
    }
}
