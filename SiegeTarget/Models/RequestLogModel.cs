using MongoDB.Bson.Serialization.Attributes;

namespace SiegeTarget.Models;

public class RequestLogModel
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    public string SessionId { get; set; }

    public string Path { get; set; }

    public string ContentType { get; set; }

    public string RequestMethod { get; set; }

    public string? QueryString { get; set; }

    public string Ip { get; set; }

    public DateTime DateTime { get; set; }
}
