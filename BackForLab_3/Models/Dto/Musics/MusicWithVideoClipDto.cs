using BackForLab_3.Models.Entities;
using MongoDB.Bson;

namespace BackForLab_3.Models.Dto.Musics
{
    public class MusicWithVideoClipDto
    {
        public string _id { get; set; } = null;
        public string Name { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string? Author { get; set; } = null;
        public string? Genre { get; set; } = null;
        public VideoClip? VideoClip { get; set; } = null;
    }
}
