using Domain;
using TaskItem = Domain.Task;
using TaskPriority = Domain.TaskPriority;
using TaskStatus = Domain.TaskStatus;

namespace TaskService.Repositories;

public class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<TaskItem> tasks =
    [
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
    ];

    public System.Threading.Tasks.Task<IEnumerable<TaskItem>> GetAllAsync(string? status, string? search, CancellationToken cancellationToken = default)
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

        return System.Threading.Tasks.Task.FromResult(result);
    }

    public System.Threading.Tasks.Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = tasks.FirstOrDefault(task => task.Id == id);
        return System.Threading.Tasks.Task.FromResult(task);
    }

    public System.Threading.Tasks.Task<TaskItem> CreateAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        task.Id = Guid.NewGuid();
        task.CreatedAt = DateTime.UtcNow;
        task.UpdatedAt = DateTime.UtcNow;

        tasks.Add(task);

        return System.Threading.Tasks.Task.FromResult(task);
    }

    public System.Threading.Tasks.Task<TaskItem?> UpdateAsync(Guid id, TaskItem task, CancellationToken cancellationToken = default)
    {
        var existingTask = tasks.FirstOrDefault(taskItem => taskItem.Id == id);

        if (existingTask is null)
        {
            return System.Threading.Tasks.Task.FromResult<TaskItem?>(null);
        }

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.Status = task.Status;
        existingTask.Priority = task.Priority;
        existingTask.DueDate = task.DueDate;
        existingTask.UpdatedAt = DateTime.UtcNow;

        return System.Threading.Tasks.Task.FromResult<TaskItem?>(existingTask);
    }

    public System.Threading.Tasks.Task<TaskItem?> UpdateStatusAsync(Guid id, TaskStatus status, CancellationToken cancellationToken = default)
    {
        var existingTask = tasks.FirstOrDefault(task => task.Id == id);

        if (existingTask is null)
        {
            return System.Threading.Tasks.Task.FromResult<TaskItem?>(null);
        }

        existingTask.Status = status;
        existingTask.UpdatedAt = DateTime.UtcNow;

        return System.Threading.Tasks.Task.FromResult<TaskItem?>(existingTask);
    }

    public System.Threading.Tasks.Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var existingTask = tasks.FirstOrDefault(task => task.Id == id);

        if (existingTask is null)
        {
            return System.Threading.Tasks.Task.FromResult(false);
        }

        tasks.Remove(existingTask);

        return System.Threading.Tasks.Task.FromResult(true);
    }
}
