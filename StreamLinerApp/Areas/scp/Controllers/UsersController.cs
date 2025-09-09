using Microsoft.AspNetCore.Mvc;
using StreamLinerLogicLayer.Helper.LicenseServices;
using StreamLinerLogicLayer.Services.Auth;
using StreamLinerViewModelLayer.ModelDTO;
using System.ComponentModel;

namespace StreamLinerApp.Areas.scp.Controllers
{
    [Area("scp")]
    public class UsersController : Controller
    {
        private readonly IAuthService _accountService;
        private readonly IWebHostEnvironment _env;
        private readonly ILicenseService _licenseService;

        public UsersController(IAuthService accountService, IWebHostEnvironment env,
             ILicenseService licenseService)
        {
            _accountService = accountService;
            _env = env;
            _licenseService = licenseService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["application"] = "Admin";
            ViewData["action"] = "Users";
            ViewData["controller"] = "Users";
            var users = await _accountService.GetAllUsersAsync();

            return View(users);
        }


        public async Task<IActionResult> AddUser()
        {
            ViewData["application"] = "Admin";
            ViewData["action"] = "New User";
            ViewData["controller"] = "Users";
            ViewData["pwd"] = await _accountService.GeneratePasswordAsync(12);
             
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(userDto userDto)
        {
            ViewData["application"] = "Admin";
            ViewData["action"] = "New User";
            ViewData["controller"] = "Users";



        
            if (ModelState.IsValid)
            {
                var user = await _accountService.AddUser(userDto);
                if (user.Id != null)
                {
                    return RedirectToAction("Index");
                }
                 
            }
            ViewData["pwd"] = await _accountService.GeneratePasswordAsync(12);
            return View(userDto);
        }


    }
}
