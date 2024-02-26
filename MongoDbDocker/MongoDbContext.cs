using MongoDB.Driver;

namespace MongoDbDocker;

public class MongoDbContext
{
    public IMongoCollection<Person> _collection { get; init; }
    public MongoDbContext(IConfiguration configuration )
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:Database"));
        _collection = database.GetCollection<Person>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
    }
}
