using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StreamLinerApp.Areas.CMS.Controllers
{
    public class OutboundController : Controller
    {
        [Area("CMS")]
        [Authorize(Roles = "Administrator,Admin,NamedUser")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult New()
        {
            return View();
        }
        public IActionResult Accepted()
        {
            return View();
        }
        public IActionResult Rejected()
        {
            return View();
        }
        public IActionResult Reassigned()
        {
            return View();
        }
        public IActionResult Compeleted()
        {
            return View();
        }
    }
}
