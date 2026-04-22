using Domain;
using TaskPriority = Domain.TaskPriority;
using TaskStatus = Domain.TaskStatus;

namespace TaskService.Requests;

public record UpdateTaskRequest(
    string Title,
    string Description,
    TaskStatus Status,
    TaskPriority Priority,
    DateTime? DueDate);
