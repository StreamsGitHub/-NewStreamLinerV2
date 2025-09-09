using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StreamLinerApp.Areas.CMS.Controllers
{
    [Area("CMS")]
    [Authorize(Roles = "Administrator,Admin,NamedUser")]
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
