using BackForLab_3.Models.Dto.Post;
using BackForLab_3.Models.Entities;
using BackForLab_3.Services;
using BackForLab_3.Services.Cache;
using BackForLab_3.Services.Posts;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Text.Json;

namespace BackForLab_3.Controllers
{
    public class PostController:ControllerBase
    {
        private readonly IPostService _postService;
        private readonly INotificationService _notificationService;

        public PostController(IPostService postService, INotificationService notificationService)
        {
            _postService = postService;
            _notificationService = notificationService;
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
                await _postService.InsertPost(post);


                var publicPostDto = new PublicPostDto
                {
                    _id = await _postService.GetMaxId(),
                    DateTimeCreated= post.DateTimeCreated,
                    Text = insertPost.Text,
                };

                _notificationService.Notify("123", "123");

                //_notificationService.Notify(
                //    "channel_" + insertPost.AuthorId,
                //    JsonSerializer.Serialize(publicPostDto)
                //    );

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
