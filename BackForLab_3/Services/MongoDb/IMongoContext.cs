using MongoDB.Driver;

namespace BackForLab_3.Services.MongoDb
{
    public interface IMongoContext
    {
        MongoClient MongoClient { get; }
        IMongoDatabase Database { get; }
    }
}
