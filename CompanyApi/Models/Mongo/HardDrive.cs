using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CompanyApi.Models.Mongo;

public class HardDrive
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("catagory_id")]
    public string CategoryId { get; set; } = null!;

    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("connection_type_id")]
    public string ConnectionIntTypeId { get; set; } = null!;
    
    [BsonElement("name")]
    public string Name { get; set; } = null!;
    
    [BsonElement("price")]
    public int Price { get; set; }
    
    [BsonElement("count")]
    public int Count { get; set; }
    
    [BsonElement("size")]
    public int Size { get; set; }
}