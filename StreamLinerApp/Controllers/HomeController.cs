using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StreamLinerApp.Models;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerLogicLayer.Services.Auth;
using StreamLinerViewModelLayer.ModelDTO;
using System.Diagnostics;

namespace StreamLinerApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthService _authService;

        public HomeController(ILogger<HomeController> logger , IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        public IActionResult Index()
        {
            ViewData["application"] = " root";
            ViewData["action"] = "Dashboard";
            ViewData["controller"] = "Dashboard";

            return View();
        }


        public IActionResult Privacy()
        {
            ViewData["application"] = " root";
            ViewData["action"] = "Dashboard";
            ViewData["controller"] = "Dashboard";

            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
