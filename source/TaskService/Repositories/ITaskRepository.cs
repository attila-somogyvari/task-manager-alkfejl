using Domain;
using TaskItem = Domain.Task;
using TaskStatus = Domain.TaskStatus;

namespace TaskService.Repositories;

public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetAllAsync(string? status, string? search, CancellationToken cancellationToken = default);

    Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<TaskItem> CreateAsync(TaskItem task, CancellationToken cancellationToken = default);

    Task<TaskItem?> UpdateAsync(Guid id, TaskItem task, CancellationToken cancellationToken = default);

    Task<TaskItem?> UpdateStatusAsync(Guid id, TaskStatus status, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
