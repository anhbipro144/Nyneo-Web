using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Nyneo_Web.Models;

public class CreateDiary
{
    public string? userId { get; set; }


    [DisplayName("Title")]
    public string? title { get; set; }
    [DisplayName("Content")]
    public string? content { get; set; }

}

