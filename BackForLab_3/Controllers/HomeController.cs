using Microsoft.AspNetCore.Mvc;

namespace BackForLab_3.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            HttpContext.Response.ContentType = "text/html; charset=utf-8";
            await HttpContext.Response.SendFileAsync("wwwroot/index.html");
            return Ok();
        }
    }
}