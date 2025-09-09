using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StreamLinerApp.Areas.CMS.Controllers
{
    [Area("CMS")]
    [Authorize(Roles = "Administrator,Admin")]
    public class ConfigurationController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Action"] = "CMS Setting";
            return View();
        }
    }
}
