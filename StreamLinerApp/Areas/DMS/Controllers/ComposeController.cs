using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StreamLinerApp.Areas.DMS.Controllers
{
    [Area("DMS")]
    [Authorize(Roles = "Administrator,Admin,NamedUser")]
    public class ComposeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Upload()
        {
            return View();
        }
        public IActionResult Scan()
        {
            return View();

        }
       
        public IActionResult Record()
        {
            return View();
        }

        public IActionResult Capturing()
        {
            return View();
        }
    }
}
