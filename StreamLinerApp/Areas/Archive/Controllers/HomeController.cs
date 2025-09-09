using Microsoft.AspNetCore.Mvc;

namespace StreamLinerApp.Areas.Archive.Controllers
{
    [Area("Archive")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
