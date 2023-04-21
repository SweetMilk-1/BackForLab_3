using BackForLab_3.Models.Dto;
using MongoDB.Bson;
using BackForLab_3.Models.Entities;
using BackForLab_3.Models.Dto.Musics;

namespace BackForLab_3.Services.Musics
{
    public interface IMusicService
    {        Task Delete(string id);
        Task Update(string id, Music music);
        Task Insert(Music music);
        Task<MusicWithVideoClipDto?> GetWithVideoClip(string id);
        Task<MusicWithAuthorDto?> GetWithAuthor(string id);
        Task<MusicWithPlaylistsDto> GetWithPlaylists(string id);
        Task<IEnumerable<MusicWithVideoClipDto?>> GetMusicsForNotification();
        Task<double> GetGrade(string id);
    }
}
