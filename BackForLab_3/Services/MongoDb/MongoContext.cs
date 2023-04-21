using MongoDB.Driver;

namespace BackForLab_3.Services.MongoDb
{
    public class MongoContext : IMongoContext
    {
        private const string _connectionString = "mongodb://localhost:27017";
        public MongoClient MongoClient { get; private set; }
        public IMongoDatabase Database { get; private set; }
        public MongoContext() {
            MongoClient = new MongoClient(_connectionString);
            Database = MongoClient.GetDatabase("MusicsDB");
        }
    }
}
