using BackForLab_3.Models.Entities;
using BackForLab_3.Services.Converters;
using BackForLab_3.Services.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BackForLab_3.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly IBsonConverter _bsonConverter;
        private readonly IMongoContext _mongoContext;
        public PostService(IBsonConverter bsonConverter, IMongoContext mongoContext)
        {
            _bsonConverter = bsonConverter;
            _mongoContext = mongoContext;
        }
        public Task InsertPost(Post post)
        {
            var postBson = _bsonConverter.ConvertPostToBson(post);
            return _mongoContext.Database.GetCollection<BsonDocument>("Posts").InsertOneAsync(postBson);
        }

        public Task LikePost(string UserId, string PostId)
        {
            try
            {
                return _mongoContext.Database.GetCollection<BsonDocument>("Posts").
                    UpdateOneAsync(
                        new BsonDocument("_id", new ObjectId(PostId)),
                        new BsonDocument("$push", new BsonDocument("LikedUsers", UserId))
                    );
            }
            catch {
                throw;
            }
        }
    }
}
