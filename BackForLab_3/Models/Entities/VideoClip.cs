using MongoDB.Bson;

namespace BackForLab_3.Models.Entities
{
    public class VideoClip
    {
        public string FilePath { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string DirectBy { get; set; } = null!;
    }
}
