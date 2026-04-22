using Domain;
using TaskStatus = Domain.TaskStatus;

namespace TaskService.Requests;

public record UpdateTaskStatusRequest(TaskStatus Status);
