namespace BackForLab_3.Models.Entities
{
    public class Post
    {
        public string _id { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public string Text { get; set; } = null!;
        public DateTime DateTimeCreated { get; set; } = DateTime.Now;
        public List<string> LikedUsers = new();
    }
}
