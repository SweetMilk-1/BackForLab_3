using Amazon.Runtime.SharedInterfaces;
using BackForLab_3.Models.Dto;
using BackForLab_3.Models.Dto.Musics;
using BackForLab_3.Models.Entities;
using BackForLab_3.Services.Converters;
using BackForLab_3.Services.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BackForLab_3.Services.Musics
{
    public class MusicService : IMusicService
    {
        private readonly IBsonConverter _bsonConverter;
        private readonly IMongoContext _mongoContext;
        public MusicService(IBsonConverter bsonConverter, IMongoContext mongoContext)
        {
            _bsonConverter = bsonConverter;
            _mongoContext = mongoContext;
        }
        public async Task Insert(Music music)
        {
            var bsonDocument = _bsonConverter.ConvertMusicToBson(music);
            var collection = _mongoContext.Database.GetCollection<BsonDocument>("Musics");
            await collection.InsertOneAsync(bsonDocument);
        }
        public async Task Delete(string id)
        {
            await _mongoContext.Database.
                GetCollection<BsonDocument>("Musics").
                DeleteOneAsync(new BsonDocument ("_id", new ObjectId(id)));
        }
        public async Task Update(string id, Music music)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(music));
            await _mongoContext.Database.
                GetCollection<BsonDocument>("Musics").
                ReplaceOneAsync($"{{_id:ObjectId('{id}')}}", _bsonConverter.ConvertMusicToBson(music));
        }
        public async Task<MusicWithVideoClipDto?> GetWithVideoClip(string id)
        {
            var musics = await _mongoContext.Database.
               GetCollection<BsonDocument>("Musics").
               Find($"{{_id : ObjectId('{id}')}}").
               ToListAsync();
            
            if (musics.Count == 0)
                throw new Exception("NotFound");

            return _bsonConverter.ConvertBsonToMusicWithVideoClip(musics[0]);
        }
        public async Task<MusicWithAuthorDto?> GetWithAuthor(string id)
        {
            var musics = await _mongoContext.Database.
              GetCollection<BsonDocument>("Musics").
              Aggregate().
              Match($"{{_id:ObjectId('{id}')}}").
              Lookup("Authors", "Author", "_id", "Author").
              Unwind("Author").
              Project("{VideoClip: 0}").
              ToListAsync();

            if (musics.Count == 0)
                throw new Exception("NotFound");

            return _bsonConverter.ConvertBsonToMusicWithAuthor(musics[0]);
        }
        public async Task<MusicWithPlaylistsDto?> GetWithPlaylists(string id)
        {
            var playlistAggregate = new EmptyPipelineDefinition<BsonDocument>().
                Unwind("Musics").
                Match($"{{Musics:ObjectId('{id}')}}").
                Project("{Musics:0, User:0}");

            return _mongoContext.Database.GetCollection<BsonDocument>("Musics").
                Aggregate().
                Match($"{{_id:ObjectId('{id}')}}").
                Lookup<BsonDocument>(
                    _mongoContext.Database.GetCollection<BsonDocument>("Playlists"),
                    new BsonDocument(),
                    playlistAggregate,
                    "Playlists").
                Project("{Name:1,_id:1, Playlists: 1}").
                ToList().
                Select(item => _bsonConverter.ConvertBsonToMusicWithPlaylists(item)).
                ToArray()[0];

            /*
db.Musics.aggregate(
  {
    $match: {
      _id:ObjectId("643ed64300000c000c000036")
    }
  },
  {
    $lookup: {
      from:"Playlists",
      pipeline: [
        {
          $unwind: "$Musics"
        },
        {
          $match: {
            Musics: ObjectId("643ed64300000c000c000036")
          },
        }, 
        {
          $project: {
            "Musics":0, 
            "User":0
          }
        }
      ],
      as: "Playlists"
    }
  },
  {
    $project: {
      "Name":1,
      "_id":1,
      "Playlists": 1
    }
  }
)
             */
        }
        public async Task<IEnumerable<MusicWithVideoClipDto?>> GetMusicsForNotification()
        {  
            return _mongoContext.Database.
                GetCollection<BsonDocument>("Musics").
                Aggregate().
                Lookup("Authors", "Author", "_id", "AuthorObject").
                Unwind("AuthorObject").
                Match("{ $or: [{VideoClip: null},{AutthorObject: {$elemMatch: {Description: ''}}}]}").
                Project("{AuthorObject:0}").
                ToList().
                Select(item=>_bsonConverter.ConvertBsonToMusicWithVideoClip(item));
            /*
 db.Musics.aggregate(
  {
    $lookup: {
      from:"Authors",
      localField:"Author",
      foreignField:"_id",
      as: "AuthorObject"
    }
  },
  {
    $unwind: "$AuthorObject"
  },
  {
    $match: {
      $or: [
        {
          VideoClip: null,
        },{
          AutthorObject: {
            $elemMatch: {
              Description: ""
            }
          }
        }
      ]
    }
  },
  {
    $project: {
      "AuthorObject":0
    }
  }
)
             */
        }
        public async Task<double> GetGrade(string id)
        {
            return _mongoContext.Database.
                GetCollection<BsonDocument>("Musics").
                Aggregate().
                Match($"{{_id:ObjectId('{id}')}}").
                Lookup("Grades", "_id", "Music", "Grades").
                Project(@"
{
    Grade: {
        $avg: ""$Grades.Grade""
    }
}
                ").FirstOrDefault().GetValue("Grade").AsDouble;
            /*
db.Musics.aggregate(
  {
    $match: {
      _id:ObjectId("643ed64300000c000c000036")
    }
  },
  {
    $lookup: {
      from: "Grades",
      localField:"_id",
      foreignField:"Music",
      as:"Grades"
    }
  },
  {
    $project: {
      "Grade": {
        $avg: "$Grades.Grade"
      }
    }
  }
);

             */
        }
    }
}
