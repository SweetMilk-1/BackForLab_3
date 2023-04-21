namespace BackForLab_3.Models.Dto.Musics
{
    public class MusicWithAuthorDto
    {
        public string _id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public AuthorDto Author { get; set; }
    }

public class AuthorDto
    {
        public string _id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
