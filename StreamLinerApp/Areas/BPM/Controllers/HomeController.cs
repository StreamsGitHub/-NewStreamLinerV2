using Microsoft.AspNetCore.Mvc;

namespace StreamLinerApp.Areas.Forms.Controllers
{
    [Area("BPM")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
