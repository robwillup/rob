using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Rob.Api.Models;

public class Article
{
    public ObjectId Id { get; set; }

    [BsonElement("title")]
    public string Title { get; set; }

    [BsonElement("subTitle")]
    public string SubTitle { get; set; }

    [BsonElement("content")]
    public string Content { get; set; }

    [BsonElement("tags")]
    public string Tags { get; set; }

    public Article()
    {
        Id = ObjectId.GenerateNewId();
    }
}
