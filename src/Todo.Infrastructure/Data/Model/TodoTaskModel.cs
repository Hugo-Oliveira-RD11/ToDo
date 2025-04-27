using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Todo.Domain.Enums;

namespace Todo.Infrastructure.Data.Model;

public class TodoTaskModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
    
    [BsonElement("goal")]
    public string Goal { get; set; } = string.Empty;

    [BsonElement("notes")]
    public string Notes { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.Int32)]
    [BsonElement("category")]
    public Categories Category { get; set; } = Categories.White;
    
    [BsonElement("done")]
    public bool Done { get; set; } = false;

    [BsonElement("completationDate")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? CompletationDate { get; set; } = null;

    [BsonElement("createdAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}