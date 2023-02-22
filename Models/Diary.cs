using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Nyneo_Web.Models;

public class Diary
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }


    [BsonRepresentation(BsonType.ObjectId)]
    public string? userId { get; set; }

    // [BsonRepresentation(BsonType.ObjectId)]
    public IEnumerable<Comment> comments { get; set; } = null!;

    [DisplayName("Title")]
    public string? title { get; set; }
    [DisplayName("Content")]
    public string? content { get; set; }

    public DateTime created_at { get; set; } = DateTime.Now;
}

