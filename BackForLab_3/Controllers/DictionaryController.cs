using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.Operations;
using BackForLab_3.Services.Dictionaries;

namespace BackForLab_3.Controllers
{
    public class DictionaryController:ControllerBase
    {

        private readonly IDictionaryService _dicionaryService;

        public DictionaryController(IDictionaryService dicionaryService)
        {
            _dicionaryService= dicionaryService;
        }

        [Route("/authors")]
        [HttpGet]
        public IActionResult Authors()
        {
            return Ok(_dicionaryService.GetAuthors());
        }


        [Route("/genres")]
        [HttpGet]
        public IActionResult Genres()
        {
            return Ok(_dicionaryService.GetGenres());
        }
    }
}
