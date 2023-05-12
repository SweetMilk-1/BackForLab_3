using BackForLab_3.Models.Dto.Musics;
using BackForLab_3.Models.Entities;
using BackForLab_3.Services.Cache;
using BackForLab_3.Services.Musics;
using Microsoft.AspNetCore.Mvc;

namespace BackForLab_3.Controllers
{
    public class MusicController:ControllerBase
    {
        private readonly IMusicService _musicService;
        private readonly ICacheService _cacheService;
        public MusicController(IMusicService musicService, ICacheService cacheService)
        {
            _musicService = musicService;
            _cacheService = cacheService;
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
        public async Task<IActionResult> GetMusic(string id, string? type)
        {
            var request = HttpContext.Request;
            var response = HttpContext.Response;
            response.ContentType = "application/json";
            object result = null;
            string key = String.Empty;
            try
            {
                switch (type)
                {
                    case "with-videoclip":
                        #region
                        key = "MusicWithVideoclip_" + id;
                        result = await _cacheService.Get<MusicWithVideoClipDto?>(key);
                        if (result == null)
                        {
                            Console.WriteLine($"Кэширую {key}");
                            return Ok(await _cacheService.Set(
                                key,
                                await _musicService.GetWithVideoClip(id)));
                        }
                        else
                        {
                            Console.WriteLine($"Взял из кэша {key}");
                            return Ok(result);
                        }
                    #endregion
                    case "with-author":
                        #region
                        key = "MusicWithAuthor_" + id;
                        result = await _cacheService.Get<MusicWithAuthorDto?>(key);
                        if (result == null)
                        {
                            Console.WriteLine($"Кэширую {key}");
                            return Ok(await _cacheService.Set(
                                key,
                                await _musicService.GetWithAuthor(id)));
                        }
                        else
                        {
                            Console.WriteLine($"Взял из кэша {key}");
                            return Ok(result);
                        }
                    #endregion
                    case "with-playlists":
                        #region
                        key = "MusicWithPlaylists_" + id;
                        result = await _cacheService.Get<MusicWithPlaylistsDto?>(key);
                        if (result == null)
                        {
                            Console.WriteLine($"Кэширую {key}");
                            return Ok(await _cacheService.Set(
                                key,
                                await _musicService.GetWithPlaylists(id)));
                        }
                        else
                        {
                            Console.WriteLine($"Взял из кэша {key}");
                            return Ok(result);
                        }
                        #endregion
                    case "grade":
                        #region
                        key = "MusicGrade_" + id;
                        result = await _cacheService.Get<int?>(key);
                        if (result == null)
                        {
                            Console.WriteLine($"Кэширую {key}");
                            return Ok(await _cacheService.Set(
                                key,
                                await _musicService.GetGrade(id)));
                        }
                        else
                        {
                            Console.WriteLine($"Взял из кэша {key}");
                            return Ok(result);
                        }
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
