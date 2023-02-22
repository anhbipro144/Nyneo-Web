using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace Nyneo_Web.Models;

[CollectionName("ApplicationRoles")]
public class Role : MongoIdentityRole<ObjectId>
{
    // public string? Name { get; set; }
    // [BsonId]
    // [BsonRepresentation(BsonType.ObjectId)]
    //
    // public new string? Id { get; set; }
}