using Microsoft.AspNetCore.Mvc;

namespace StreamLinerApp.Controllers
{
    public class DiscussController : Controller
    {
        public IActionResult Index()
        {
            ViewData["application"] = " root";
            ViewData["action"] = "Dashboard";
            ViewData["controller"] = "Dashboard";

            return View();
        }

        public IActionResult Groups()
        {
            ViewData["application"] = " root";
            ViewData["action"] = "Dashboard";
            ViewData["controller"] = "Dashboard";

            return View();
        }
        public IActionResult Compose()
        {
            ViewData["application"] = " root";
            ViewData["action"] = "Dashboard";
            ViewData["controller"] = "Dashboard";

            return View();
        }


        
        public IActionResult Inboxx()
        {
            ViewData["application"] = " root";
            ViewData["action"] = "Dashboard";
            ViewData["controller"] = "Dashboard";

            return View();
        }


        public IActionResult Sent()
        {
            ViewData["application"] = " root";
            ViewData["action"] = "Dashboard";
            ViewData["controller"] = "Dashboard";

            return View();
        }

        public IActionResult Note()
        {
            ViewData["application"] = " root";
            ViewData["action"] = "Dashboard";
            ViewData["controller"] = "Dashboard";

            return View();
        }
    }
}
