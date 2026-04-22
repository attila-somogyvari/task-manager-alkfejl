using Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TaskService.Configuration;
using TaskItem = Domain.Task;
using TaskStatus = Domain.TaskStatus;

namespace TaskService.Repositories;

public class MongoTaskRepository : ITaskRepository
{
    private readonly IMongoCollection<TaskItem> collection;

    public MongoTaskRepository(IOptions<MongoDbSettings> settings)
    {
        var mongoSettings = settings.Value;
        var client = new MongoClient(mongoSettings.ConnectionString);
        var database = client.GetDatabase(mongoSettings.DatabaseName);
        collection = database.GetCollection<TaskItem>(mongoSettings.TasksCollectionName);
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync(string? status, string? search, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TaskItem>.Filter.Empty;

        if (!string.IsNullOrWhiteSpace(status) &&
            Enum.TryParse<TaskStatus>(status, true, out var parsedStatus))
        {
            filter &= Builders<TaskItem>.Filter.Eq(task => task.Status, parsedStatus);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchFilter = Builders<TaskItem>.Filter.Or(
                Builders<TaskItem>.Filter.Regex(task => task.Title, new MongoDB.Bson.BsonRegularExpression(search, "i")),
                Builders<TaskItem>.Filter.Regex(task => task.Description, new MongoDB.Bson.BsonRegularExpression(search, "i")));

            filter &= searchFilter;
        }

        return await collection.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await collection.Find(task => task.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TaskItem> CreateAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        task.Id = Guid.NewGuid();
        task.CreatedAt = DateTime.UtcNow;
        task.UpdatedAt = DateTime.UtcNow;

        await collection.InsertOneAsync(task, cancellationToken: cancellationToken);

        return task;
    }

    public async Task<TaskItem?> UpdateAsync(Guid id, TaskItem task, CancellationToken cancellationToken = default)
    {
        var existingTask = await GetByIdAsync(id, cancellationToken);

        if (existingTask is null)
        {
            return null;
        }

        task.Id = existingTask.Id;
        task.CreatedAt = existingTask.CreatedAt;
        task.UpdatedAt = DateTime.UtcNow;

        await collection.ReplaceOneAsync(taskItem => taskItem.Id == id, task, cancellationToken: cancellationToken);

        return task;
    }

    public async Task<TaskItem?> UpdateStatusAsync(Guid id, TaskStatus status, CancellationToken cancellationToken = default)
    {
        var update = Builders<TaskItem>.Update
            .Set(task => task.Status, status)
            .Set(task => task.UpdatedAt, DateTime.UtcNow);

        var options = new FindOneAndUpdateOptions<TaskItem>
        {
            ReturnDocument = ReturnDocument.After
        };

        return await collection.FindOneAndUpdateAsync(task => task.Id == id, update, options, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await collection.DeleteOneAsync(task => task.Id == id, cancellationToken);
        return result.DeletedCount > 0;
    }
}
