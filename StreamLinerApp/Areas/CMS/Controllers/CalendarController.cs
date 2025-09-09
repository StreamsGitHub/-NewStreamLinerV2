using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StreamLinerApp.Areas.CMS.Controllers
{
    [Area("CMS")]
    [Authorize(Roles = "Administrator,Admin,NamedUser")]
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddMeeting()
        {
            return View();
        }
    }
}
