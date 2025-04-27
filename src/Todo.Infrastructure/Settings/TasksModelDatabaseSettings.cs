namespace Todo.Infrastructure.Settings;

public class TasksModelDatabaseSettings
{
    public string ConnectionString { get; init ; } = string.Empty;
    
    public string DatabaseName { get; init; } = string.Empty;
    
    public string CollectionName { get; init; } = string.Empty;
}