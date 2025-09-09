using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerLogicLayer.Helper.LicenseServices;
using StreamLinerLogicLayer.Helper.EmailService;
using StreamLinerLogicLayer.Services.Auth;
using System.ComponentModel;

namespace StreamLinerApp.Controllers
{
    public class LicenseController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILicenseService _licenseService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _accountService;
        private readonly IEmailSender _emailSender;
        public LicenseController(ILicenseService licenseService, UserManager<ApplicationUser> userManager, IAuthService accountService, IEmailSender emailSender)
        {
            _httpClient = new HttpClient();
            _licenseService = licenseService;
            _userManager = userManager;
            _accountService = accountService;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> download(string LicenseKey)
        {

            try
            {
                await _licenseService.RequestFileLicense(LicenseKey);
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        //("api/licenseupload")
        //[HttpPost]
        //public async Task<IActionResult> Upload(IFormFile licenseFile, string LicenseKey)
        //{

        //    try
        //    {

        //        // check if Strlcs null 
        //        // => create Master data
        //        // ===> user 
        //        //var Licenses = _licenseService.GetAll().Result;
        //        //if (!Licenses.Any())
        //        //{

        //        //    var adminUser = await _userManager.FindByNameAsync("admin");
        //        //    if (adminUser == null)
        //        //    {                         // Create the admin user if it does not exist
        //        //        var user = new ApplicationUser
        //        //        {
        //        //            UserName = "admin",
        //        //            Email = "joseph.bishara@streams.com.eg",

        //        //        };
        //        //        var password = "P@$$w0rd";
        //        //        var result = await _userManager.CreateAsync(user, password);

        //        //        if (result.Succeeded)
        //        //        {
        //        //            await _userManager.AddToRoleAsync(user, "Admin");
        //        //            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //        //            var link = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
        //        //            await _emailSender.SendEmailAsync(user.Email, "Confirm Email", $"<a href='{link}'>Confirm your account</a>");


        //        //            return RedirectToAction("Login");
        //        //        }
        //        //        else
        //        //        {
        //        //            return BadRequest(result.Errors);
        //        //        }
        //        //    }
        //        //    return RedirectToAction("Login");

        //        //}

        //        // upload license file to server and get license information from api if no



        //        // licences exists
        //        var licenseInfo = await _licenseService.UploadLicenseFile(licenseFile, LicenseKey);
        //        if (licenseInfo == null)
        //        {
        //            return BadRequest(new { message = "Failed to upload license file." });
        //        }
        //        //save license file to database 
        //        try
        //        {
        //            // Map LicenseRequest to Strlcs before calling InsertFileRecord
        //            var strlcsModel = new Strlcs
        //            {
        //                ExpirationDate = DateTime.Parse(licenseInfo.LicenseExpirationDate),
        //                CreationDate = DateTime.Parse(licenseInfo.LicenseCreationDate),
        //                LicenseKey = LicenseKey,
        //                FileName = licenseFile.FileName,
        //                FilePath = licenseInfo.FilePath,

        //                NumberOfAdmins = licenseInfo.NumberOfAdmins,
        //                NumberOfUsers = licenseInfo.NumberOfUsers,
        //                NumberOfParticipants = licenseInfo.NumberOfParticipants,

        //                LicenseStatus = "Active",
        //                MaxQuote = licenseInfo.MaxQuote
        //            };

        //            var isInserted = await _licenseService.InsertFileRecord(strlcsModel);
        //            return RedirectToAction("Login", "Account");
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(new { message = ex.Message });
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}
    }
}
