using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Domain;
using McpService.Clients;
using ModelContextProtocol.Server;
using TaskItem = Domain.Task;
using TaskPriority = Domain.TaskPriority;
using TaskStatus = Domain.TaskStatus;

namespace McpService.Tools;

[McpServerToolType]
public static class TaskTools
{
    [McpServerTool(Name = "GetTasks", Title = "Feladatok lekerdezese", Destructive = false, Idempotent = true, ReadOnly = true)]
    [Description("Visszaadja a feladatok listajat, opcionisan statusz vagy kereses alapjan szurve.")]
    public static async Task<IEnumerable<TaskItem>> GetTasks(
        string? status,
        string? search,
        [FromServices] ITaskServiceClient client,
        CancellationToken cancellationToken)
    {
        return await client.GetTasksAsync(status, search, cancellationToken);
    }

    [McpServerTool(Name = "SearchTasks", Title = "Feladatok keresese", Destructive = false, Idempotent = true, ReadOnly = true)]
    [Description("Feladatok keresese szoveg alapjan a cimben vagy leirasban.")]
    public static async Task<IEnumerable<TaskItem>> SearchTasks(
        string search,
        [FromServices] ITaskServiceClient client,
        CancellationToken cancellationToken)
    {
        return await client.GetTasksAsync(search: search, cancellationToken: cancellationToken);
    }

    [McpServerTool(Name = "CreateTask", Title = "Uj feladat letrehozasa", Destructive = false, Idempotent = false, ReadOnly = false)]
    [Description("Letrehoz egy uj feladatot a megadott adatokkal.")]
    public static async Task<TaskItem> CreateTask(
        string title,
        string description,
        TaskStatus status,
        TaskPriority priority,
        DateTime? dueDate,
        [FromServices] ITaskServiceClient client,
        CancellationToken cancellationToken)
    {
        var request = new CreateTaskRequest(title, description, status, priority, dueDate);
        return await client.CreateTaskAsync(request, cancellationToken);
    }

    [McpServerTool(Name = "UpdateTaskStatus", Title = "Feladat statuszanak modositasa", Destructive = false, Idempotent = true, ReadOnly = false)]
    [Description("Modositja egy meglevo feladat statuszat.")]
    public static async Task<TaskItem?> UpdateTaskStatus(
        Guid id,
        TaskStatus status,
        [FromServices] ITaskServiceClient client,
        CancellationToken cancellationToken)
    {
        return await client.UpdateTaskStatusAsync(id, status, cancellationToken);
    }

    [McpServerTool(Name = "DeleteTask", Title = "Feladat torlese", Destructive = true, Idempotent = false, ReadOnly = false)]
    [Description("Torol egy meglevo feladatot az azonositoja alapjan.")]
    public static async Task<bool> DeleteTask(
        Guid id,
        [FromServices] ITaskServiceClient client,
        CancellationToken cancellationToken)
    {
        return await client.DeleteTaskAsync(id, cancellationToken);
    }
}
