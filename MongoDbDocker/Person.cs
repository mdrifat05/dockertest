using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbDocker;

public class Person
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]  
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}
