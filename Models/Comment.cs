using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Nyneo_Web.Models;

public class Comment
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public int userId { get; set; }

    public string? content { get; set; }

    public DateTime created_at { get; set; } = DateTime.Now;
}

