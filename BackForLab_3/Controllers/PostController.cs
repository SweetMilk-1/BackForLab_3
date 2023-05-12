using Amazon.Runtime.Internal;
using BackForLab_3.Models.Dto.Post;
using BackForLab_3.Models.Entities;
using BackForLab_3.Services.Cache;
using BackForLab_3.Services.Posts;
using Microsoft.AspNetCore.Mvc;

namespace BackForLab_3.Controllers
{
    public class PostController:ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ICacheService _cacheService;
        public PostController(IPostService postService, ICacheService cacheService)
        {
            _postService = postService;
            _cacheService = cacheService;
        }

        [HttpPost("/Post/Insert")]
        public async Task<IActionResult> Insert()
        {
            var request = HttpContext.Request;
            try
            {
                var insertPost = await request.ReadFromJsonAsync<InsertPostDto>();
                var post = new Post
                {
                    Text = insertPost.Text,
                    AuthorId = insertPost.AuthorId,
                };
                _postService.InsertPost(post);

                //оповещаем

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/Post/Like")]
        public async Task<IActionResult> Like(string userId, string postId)
        {
            try
            {
                _postService.LikePost(userId, postId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
