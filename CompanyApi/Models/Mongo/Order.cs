using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CompanyApi.Models.Mongo;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("client_id")]
    public string ClientId { get; set; } = null!;
    
    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement("date")]
    public DateTime Date { get; set; }

    [BsonElement("status")]
    public string Status { get; set; } = null!;
    
    [BsonElement("cart")]
    public List<Cart> Carts { get; set; } 
}