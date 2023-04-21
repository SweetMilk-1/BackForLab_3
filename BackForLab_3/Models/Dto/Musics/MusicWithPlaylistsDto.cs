namespace BackForLab_3.Models.Dto.Musics
{
    public class MusicWithPlaylistsDto
    {
        public string _id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PlaylistDto> Playlists{ get; set; }
    }

    public class PlaylistDto
    {
        public string _id { get; set; }
        public string Name { get; set; }
    }
}