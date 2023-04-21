using BackForLab_3.Models.Entities;
using BackForLab_3.Services.Musics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;

namespace BackForLab_3.Controllers
{
    public class MusicController:ControllerBase
    {
        private readonly IMusicService _musicService;
        public MusicController(IMusicService musicService) { 
            _musicService= musicService;
        }

        [Route("/music")]
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var request = HttpContext.Request;
            try { 
                var music = await request.ReadFromJsonAsync<Music>();
                await _musicService.Insert(music);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("/music/{id}")]
        [HttpPut]
        public async Task<IActionResult> Update(string id)
        {
            var request = HttpContext.Request;
            try
            {
                var music = await request.ReadFromJsonAsync<Music>();
                await _musicService.Update(id, music);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("/music/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                _musicService.Delete(id);
                return Ok();
            }
            catch
            { 
                return BadRequest(); 
            }
        }


        [Route("/music/get-for-notification")]
        [HttpGet]
        public async Task<IActionResult> GetForNotification()
        {
            try
            {
                HttpContext.Response.ContentType = "application/json";
                var result = await _musicService.GetMusicsForNotification();
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("/music/{id}/{type}")]
        [HttpGet]
        public async Task<IActionResult> WithVideoClip(string id, string? type)
        {
            var request = HttpContext.Request;
            var response = HttpContext.Response;
            object result = null;
            try
            {
                switch (type)
                {
                    case "with-videoclip":
                        #region
                        response.ContentType = "application/json";
                        result = await _musicService.GetWithVideoClip(id);
                        if (result == null)
                            return NotFound();
                        return Ok(result);
                    #endregion
                    case "with-author":
                        #region
                        response.ContentType = "application/json";
                        result = await _musicService.GetWithAuthor(id);
                        if (result == null)
                            return NotFound();
                        return Ok(result);
                    #endregion
                    case "with-playlists":
                        #region
                        response.ContentType = "application/json";
                        result = await _musicService.GetWithPlaylists(id);
                        if (result == null)
                            return NotFound();
                        return Ok(result);  
                    #endregion
                    case "grade":
                        #region
                        response.ContentType = "application/json";
                        result = await _musicService.GetGrade(id);
                        if (result == null)
                            return NotFound();
                        return Ok(result);
                    #endregion
                    default:
                        return NotFound();
                }
            }
            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "NotFound":
                        return NotFound(ex.Message);
                    default:
                        return BadRequest(ex.Message);
                }
               
            }
        }
    }
}
