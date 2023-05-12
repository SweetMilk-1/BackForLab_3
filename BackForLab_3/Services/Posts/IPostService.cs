using BackForLab_3.Models.Entities;
namespace BackForLab_3.Services.Posts
{
    public interface IPostService
    {
        Task InsertPost(Post post);
        Task<string> GetMaxId();
        Task LikePost(string UserId, string PostId);
    }
}
