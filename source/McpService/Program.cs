using McpService.Clients;
using McpService.Configuration;
using ModelContextProtocol.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TaskServiceSettings>(
    builder.Configuration.GetSection(TaskServiceSettings.SectionName));

builder.Services.AddHttpClient<ITaskServiceClient, TaskServiceClient>((serviceProvider, httpClient) =>
{
    var settings = serviceProvider
        .GetRequiredService<Microsoft.Extensions.Options.IOptions<TaskServiceSettings>>()
        .Value;

    httpClient.BaseAddress = new Uri(settings.BaseUrl);
});

builder.Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();

var app = builder.Build();

app.MapGet("/", () => Results.Ok("McpService is running."));
app.MapMcp("/mcp");

app.Run();
