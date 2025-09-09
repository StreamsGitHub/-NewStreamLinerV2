using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StreamLinerApp.Areas.BPM.Controllers
{
    [Area("BPM")]
    [Authorize(Roles = "Administrator,Admin")]
    public class ConfigurationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
