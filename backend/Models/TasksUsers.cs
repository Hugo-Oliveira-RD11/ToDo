using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models;

public class TasksUsers
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }

    [BsonElement("tasks")]
    public List<TaskModel> Tasks { get; set; } = new List<TaskModel>();
}

public class TaskModel
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("objetivo")]
    public string Objetivo { get; set; } = string.Empty;

    [BsonElement("notas")]
    public string Notas { get; set; } = string.Empty;

    [BsonElement("feito")]
    public bool Feito { get; set; } = false;

    [BsonElement("createdAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}