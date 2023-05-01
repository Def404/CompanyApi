using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CompanyApi.Models.Mongo;

public class EmployeeM
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("login")]
    public string Login { get; set; } = null!;
    
    [BsonElement("email")]
    public string Email { get; set; } = null!;

    [BsonElement("full_name")]
    public string FullName { get; set; } = null!;
    
    [BsonElement("phone_number")]
    public string PhoneNumber { get; set; } = null!;
    
    [BsonElement("position")]
    public string Position { get; set; } = null!;
}