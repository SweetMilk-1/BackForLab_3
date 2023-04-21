using Amazon.Runtime.Internal.Transform;
using BackForLab_3.Models.Dto;
using BackForLab_3.Services.Converters;
using BackForLab_3.Services.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Security.AccessControl;

namespace BackForLab_3.Services.Dictionaries
{
    public class DictionaryService : IDictionaryService
    {
        private readonly IMongoContext _mongoContext;
        private readonly IBsonConverter _bsonConverter;
        public DictionaryService(IMongoContext context, IBsonConverter bsonConverter)
        {
            _mongoContext=context;
            _bsonConverter=bsonConverter;
        }

        public List<DictionaryDto> GetAuthors()
        {
            var bsonList = _mongoContext.Database.
                 GetCollection<BsonDocument>("Authors").
                 Find(new BsonDocument()).
                 ToList();
            var res = bsonList.Select(item => _bsonConverter.ConvertToDictionaryDto(item)).
                ToList();
            return res;
        }

        private void SubtreeToList(BsonDocument bson, List<DictionaryDto> acc)
        {
            acc.Add(_bsonConverter.ConvertToDictionaryDto(bson));
            var bsonArray = bson.GetValue("Children").AsBsonArray;
            if (bsonArray == null)
                return;
            foreach (var item in bsonArray) {
                SubtreeToList(item.AsBsonDocument, acc);
            }
        }
        public List<DictionaryDto> GetGenres()
        {
            var bsonList = _mongoContext.Database.
                 GetCollection<BsonDocument>("Genres").
                 Find(new BsonDocument()).
                 ToList();
            var res = bsonList.Select(item => _bsonConverter.ConvertToDictionaryDto(item)).
                ToList();
            return res;
        }
    }
}
