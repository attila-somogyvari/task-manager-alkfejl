using Domain;
using TaskService.Repositories;
using TaskService.Requests;
using TaskItem = Domain.Task;
using TaskStatus = Domain.TaskStatus;

namespace TaskService.Endpoints;

public static class TaskEndpoints
{
    public static IEndpointRouteBuilder MapTaskEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/tasks", async (string? status, string? search, ITaskRepository repository, CancellationToken cancellationToken) =>
        {
            var result = await repository.GetAllAsync(status, search, cancellationToken);

            return Results.Ok(result);
        });

        app.MapGet("/tasks/{id:guid}", async (Guid id, ITaskRepository repository, CancellationToken cancellationToken) =>
        {
            var task = await repository.GetByIdAsync(id, cancellationToken);

            return task is not null
                ? Results.Ok(task)
                : Results.NotFound();
        });

        app.MapPost("/tasks", async (CreateTaskRequest request, ITaskRepository repository, CancellationToken cancellationToken) =>
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

            var createdTask = await repository.CreateAsync(task, cancellationToken);

            return Results.Created($"/tasks/{createdTask.Id}", createdTask);
        });

        app.MapPut("/tasks/{id:guid}", async (Guid id, UpdateTaskRequest request, ITaskRepository repository, CancellationToken cancellationToken) =>
        {
            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                Priority = request.Priority,
                DueDate = request.DueDate
            };

            var updatedTask = await repository.UpdateAsync(id, task, cancellationToken);

            return updatedTask is not null
                ? Results.Ok(updatedTask)
                : Results.NotFound();
        });

        app.MapPatch("/tasks/{id:guid}/status", async (Guid id, UpdateTaskStatusRequest request, ITaskRepository repository, CancellationToken cancellationToken) =>
        {
            var updatedTask = await repository.UpdateStatusAsync(id, request.Status, cancellationToken);

            return updatedTask is not null
                ? Results.Ok(updatedTask)
                : Results.NotFound();
        });

        app.MapDelete("/tasks/{id:guid}", async (Guid id, ITaskRepository repository, CancellationToken cancellationToken) =>
        {
            var deleted = await repository.DeleteAsync(id, cancellationToken);

            return deleted
                ? Results.NoContent()
                : Results.NotFound();
        });

        return app;
    }
}
