using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StreamLinerApp.Areas.HR.Controllers
{
    [Area("HR")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly string ControllerName = "Human Resource";
        private readonly string AppName = "Human Resource";
        public IActionResult Index()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Dashboard";
            return View();
        }
    }
}
