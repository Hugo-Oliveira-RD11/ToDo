using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models;
public enum Category {
    White = 0,
    Blue = 1,
    Yellow = 2,
    Red = 3
}
public class TasksUsers
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
    
    [BsonElement("objetivo")]
    public string Objetivo { get; set; } = string.Empty;

    [BsonElement("notas")]
    public string Notas { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.Int32)]
    [BsonElement("category")]
    public Category Category { get; set; } = Category.White;
    
    [BsonElement("feito")]
    public bool Feito { get; set; } = false;

    [BsonElement("aDayToComplet")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? ADayToComplet { get; set; } = null;

    [BsonElement("createdAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
