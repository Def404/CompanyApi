using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CompanyApi.Models.Mongo;

public class HardDriveM
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("category_id")]
    public string CategoryId { get; set; } = null!;

    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("connection_type_id")]
    public string ConnectionIntTypeId { get; set; } = null!;
    
    [BsonElement("ps_id")]
    public int ps_id { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; } = null!;
    
    [BsonElement("price")]
    public int Price { get; set; }
    
    [BsonElement("count")]
    public int Count { get; set; }
    
    [BsonElement("size")]
    public int Size { get; set; }
}