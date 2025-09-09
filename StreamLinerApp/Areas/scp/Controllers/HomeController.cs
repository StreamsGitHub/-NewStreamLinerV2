using Microsoft.AspNetCore.Mvc;

namespace StreamLinerApp.Areas.scp.Controllers
{
    [Area("scp")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
