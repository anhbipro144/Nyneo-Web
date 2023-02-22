using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace Nyneo_Web.Models;

[CollectionName("ApplicationUsers")]
public class User : MongoIdentityUser<ObjectId>
{



}