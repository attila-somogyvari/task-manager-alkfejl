using Domain;
using TaskItem = Domain.Task;
using TaskStatus = Domain.TaskStatus;

namespace McpService.Clients;

public interface ITaskServiceClient
{
    Task<IEnumerable<TaskItem>> GetTasksAsync(string? status = null, string? search = null, CancellationToken cancellationToken = default);

    Task<TaskItem> CreateTaskAsync(CreateTaskRequest request, CancellationToken cancellationToken = default);

    Task<TaskItem?> UpdateTaskStatusAsync(Guid id, TaskStatus status, CancellationToken cancellationToken = default);

    Task<bool> DeleteTaskAsync(Guid id, CancellationToken cancellationToken = default);
}
