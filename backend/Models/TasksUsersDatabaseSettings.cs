namespace backend.Models;

public class TasksUsersDatabaseSettings
{
    public string ConnectionString { get; init ; } = string.Empty;
    
    public string DatabaseName { get; init; } = string.Empty;
    
    public string CollectionName { get; init; } = string.Empty;
}