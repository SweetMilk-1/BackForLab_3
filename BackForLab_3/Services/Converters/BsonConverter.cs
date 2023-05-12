using Amazon.Runtime.Internal.Transform;
using BackForLab_3.Models.Dto;
using BackForLab_3.Models.Dto.Musics;
using BackForLab_3.Models.Entities;
using MongoDB.Bson;

namespace BackForLab_3.Services.Converters
{
    public class BsonConverter : IBsonConverter
    {
        public MusicWithAuthorDto ConvertBsonToMusicWithAuthor(BsonDocument bson)
        {
            var authorBson = bson.GetValue("Author").AsBsonDocument;
            AuthorDto authorDto = new AuthorDto { 
                _id=authorBson.GetValue("_id").AsObjectId.ToString(),
                Name=authorBson.GetValue("Name").AsString,
                Description=authorBson.GetValue("Description").AsString,
            };
            return new MusicWithAuthorDto
            {
                Author = authorDto,
                _id = bson.GetValue("_id").AsObjectId.ToString(),
                Name = bson.GetValue("Name").AsString,
                ReleaseDate = bson.GetValue("ReleaseDate").AsDateTime,
                FilePath = bson.GetValue("FilePath").AsString,
                Genre = bson.GetValue("Genre").AsObjectId.ToString()
            };
        }

        public MusicWithVideoClipDto ConvertBsonToMusicWithVideoClip(BsonDocument bson)
        {
            
            VideoClip? videoClip = null;
            try
            {
                var videoClipBsonUnk = bson.GetValue("VideoClip");
                var videoClipBson = videoClipBsonUnk.AsBsonDocument;
                videoClip = new VideoClip
                {
                    FilePath = videoClipBson.GetValue("FilePath").AsString,
                    ReleaseDate = videoClipBson.GetValue("ReleaseDate").AsDateTime,
                    DirectBy = videoClipBson.GetValue("DirectBy").AsString
                };
            }
            catch
            {
                videoClip = null;
            }

            return new MusicWithVideoClipDto
            {
                _id = bson.GetValue("_id").AsObjectId.ToString(),
                Name = bson.GetValue("Name").AsString,
                FilePath = bson.GetValue("FilePath").AsString,
                ReleaseDate=bson.GetValue("ReleaseDate").AsDateTime,
                Author = bson.GetValue("Author").AsObjectId.ToString(),
                Genre = bson.GetValue("Genre").AsObjectId.ToString(),
                VideoClip = videoClip
            };
        }

        public BsonDocument ConvertMusicToBson(Music music)
        {
            BsonValue videoClipBson = music.VideoClip is null ? BsonNull.Value : new BsonDocument
            {
                { "FilePath",music.VideoClip.FilePath },
                { "ReleaseDate", music.VideoClip.ReleaseDate},
                { "DirectBy", music.VideoClip.DirectBy},
            };
            return new BsonDocument 
            {
                { "Name", music.Name},
                { "FilePath", music.FilePath},
                { "ReleaseDate", music.ReleaseDate},
                { "Author", music.Author},
                { "Genre", music.Genre},
                { "VideClip", videoClipBson}
            };
        }

        public DictionaryDto ConvertToDictionaryDto(BsonDocument document)
        {
            return new DictionaryDto
            {
                Value = document.GetValue("_id").AsObjectId.ToString(),
                Label=document.GetValue("Name").AsString
            };
        }
        public MusicWithPlaylistsDto ConvertBsonToMusicWithPlaylists(BsonDocument bson)
        {
            return new MusicWithPlaylistsDto
            {
                _id = bson.GetValue("_id").AsObjectId.ToString(),
                Name = bson.GetValue("Name").AsString,
                Playlists = bson.GetValue("Playlists").AsBsonArray.Select(item =>
                {
                    return new PlaylistDto
                    {
                        _id = item.AsBsonDocument.GetValue("_id").AsObjectId.ToString(),
                        Name = item.AsBsonDocument.GetValue("Name").AsString
                    };
                }).ToList()
            };
        }

        public BsonDocument ConvertPostToBson(Post post)
        {
            return new BsonDocument
            {
                { "_id", new ObjectId()},
                { "AuthorId", post.AuthorId },
                { "Text", post.Text},
                { "DateTimeCreated", new BsonDateTime(post.DateTimeCreated) },
                { "LikedUsers", new BsonArray(post.LikedUsers)}
            };
        }
    }
}
