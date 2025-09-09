using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StreamLinerApp.Areas.Workflow.Controllers
{
    [Area("CRM")]
    [Authorize(Roles = "Administrator,Admin,NamedUser")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
