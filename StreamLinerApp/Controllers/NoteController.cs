using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreamLinerLogicLayer.Services.Auth;

namespace StreamLinerApp.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public NoteController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
