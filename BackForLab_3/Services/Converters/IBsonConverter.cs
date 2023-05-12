using BackForLab_3.Models.Dto;
using BackForLab_3.Models.Dto.Musics;
using BackForLab_3.Models.Entities;
using MongoDB.Bson;

namespace BackForLab_3.Services.Converters
{
    public interface IBsonConverter
    {
        DictionaryDto ConvertToDictionaryDto(BsonDocument document);
        BsonDocument ConvertMusicToBson(Music music);
        MusicWithVideoClipDto ConvertBsonToMusicWithVideoClip(BsonDocument music);
        MusicWithAuthorDto ConvertBsonToMusicWithAuthor(BsonDocument bson);
        MusicWithPlaylistsDto ConvertBsonToMusicWithPlaylists(BsonDocument bson);
        BsonDocument ConvertPostToBson (Post post);
    }
}
