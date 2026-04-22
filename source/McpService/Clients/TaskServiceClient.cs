using System.Net.Http.Json;
using Domain;
using TaskItem = Domain.Task;
using TaskStatus = Domain.TaskStatus;

namespace McpService.Clients;

public class TaskServiceClient(HttpClient httpClient) : ITaskServiceClient
{
    public async Task<IEnumerable<TaskItem>> GetTasksAsync(string? status = null, string? search = null, CancellationToken cancellationToken = default)
    {
        var queryParameters = new List<string>();

        if (!string.IsNullOrWhiteSpace(status))
        {
            queryParameters.Add($"status={Uri.EscapeDataString(status)}");
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            queryParameters.Add($"search={Uri.EscapeDataString(search)}");
        }

        var path = "/tasks";

        if (queryParameters.Count > 0)
        {
            path = $"{path}?{string.Join("&", queryParameters)}";
        }

        var tasks = await httpClient.GetFromJsonAsync<IEnumerable<TaskItem>>(path, cancellationToken);
        return tasks ?? [];
    }

    public async Task<TaskItem> CreateTaskAsync(CreateTaskRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/tasks", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var task = await response.Content.ReadFromJsonAsync<TaskItem>(cancellationToken);
        return task ?? throw new InvalidOperationException("A TaskService nem adott vissza letrehozott feladatot.");
    }

    public async Task<TaskItem?> UpdateTaskStatusAsync(Guid id, TaskStatus status, CancellationToken cancellationToken = default)
    {
        var request = new UpdateTaskStatusRequest(status);

        var response = await httpClient.PatchAsJsonAsync($"/tasks/{id}/status", request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TaskItem>(cancellationToken);
    }

    public async Task<bool> DeleteTaskAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"/tasks/{id}", cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }

        response.EnsureSuccessStatusCode();
        return true;
    }
}
