namespace backend.Models;

public class TasksUsersDatabaseSettings
{
    public string ConnectionString { get; init ; } = null!;
    
    public string DatabaseName { get; init; } = null!;
    
    public string CollectionName { get; init; } = null!;
}