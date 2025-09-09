using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StreamLinerApp.Areas.CRM.Controllers
{
    [Area("CRM")]
    [Authorize(Roles = "Administrator,Admin")]
    public class ConfigurationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
