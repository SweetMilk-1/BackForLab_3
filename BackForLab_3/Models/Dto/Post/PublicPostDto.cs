namespace BackForLab_3.Models.Dto.Post
{
    public class PostDto
    {
        public string _id { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public string Text { get; set; } = null!;
        public DateTime DateTimeCreated { get; set; }
    }
}
