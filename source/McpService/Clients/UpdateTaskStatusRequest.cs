using Domain;
using TaskStatus = Domain.TaskStatus;

namespace McpService.Clients;

public record UpdateTaskStatusRequest(TaskStatus Status);
