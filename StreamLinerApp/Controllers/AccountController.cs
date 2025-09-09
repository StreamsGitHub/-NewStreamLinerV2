using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerLogicLayer.Helper.EmailService;
using StreamLinerLogicLayer.Services.Auth;
using StreamLinerViewModelLayer.ModelDTO;
using StreamLinerLogicLayer.Helper.LicenseServices;
using System.ComponentModel;

namespace StreamLinerApp.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IAuthService _accountService;
        private readonly IWebHostEnvironment _env;
        private readonly ILicenseService _licenseService;



        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager ,
            IEmailSender emailSender,
            IAuthService accountService,
            IWebHostEnvironment env,
            ILicenseService licenseService

            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _accountService = accountService;
            _env = env;
            _licenseService = licenseService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            //var Licenses = _licenseService.GetAll().Result;
            //if (!Licenses.Any())
            //{
            //    return RedirectToAction("Index", "License");
            //}
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            //var Licenses = _licenseService.GetAll().Result;
            //if (!Licenses.Any())
            //{
            //    return RedirectToAction("Index", "License");
            //}
            //if (!ModelState.IsValid) return View(model);

            //// get last Lic Key
            //var LicenseKey = _licenseService.GetLastLicenseKeyAsync().Result;
            ////Update License in DB from api license
            //var updatelicense = await _licenseService.UpdateFileRecord(LicenseKey);
            //if (!updatelicense)
            //{
            //    ModelState.AddModelError("", "Not Updated Licenses .");
            //    return View(model);
            //}
            ////Check Product Information License 
            //var validInfo = await _licenseService.CheckLicenseInformation(LicenseKey);
            //if (!validInfo)
            //{
            //    ModelState.AddModelError("", "Invalid License Key.");
            //    return View(model);
            //}
            ////check expiration date
            //var validExpiration = await _licenseService.CheckLicenseExpirationDate(LicenseKey);
            //if (!validExpiration)
            //{
            //    return RedirectToAction("Index", "License");
            //}
            ////Check License Status
            //var validStatus = await _licenseService.CheckLicenseStatus(LicenseKey);
            //if (!validExpiration)
            //{
            //    return RedirectToAction("Index", "License");
            //}
            ////check quota
            //var validQuota = await _licenseService.CheckLicenseQuota(new AdminViewModel { LicenseKey = LicenseKey });
            //if (!validQuota)
            //{
            //    return RedirectToAction("Index", "License");
            //}

            var result = await _accountService.LoginAsync(model);
            if (result)
            {


                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }


        //[AllowAnonymous]
        //public async Task<IActionResult> Register()
        //{
        //    RegisterViewModel registerViewModel = new RegisterViewModel()
        //    {
        //        FullName = "Mariam",
        //        Email = "Mariam.safwat@streams.com.eg",
        //        Password = "P@$$w0rd2o2o",
        //        ConfirmPassword = "P@$$w0rd2o2o"
        //    };

        //    var result = await _accountService.RegisterAsync(registerViewModel);
        //    return RedirectToAction("Login");
        //}

        [HttpGet]

        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

    }
}
