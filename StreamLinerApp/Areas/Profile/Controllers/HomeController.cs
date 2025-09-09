using Microsoft.AspNetCore.Mvc;
using StreamLinerViewModelLayer.ModelDTO;

namespace StreamLinerApp.Areas.Profile.Controllers
{
    [Area("Profile")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["application"] = "Profile";
            ViewData["action"] = "Home";
            ViewData["controller"] = "Home";
            ViewData["Username"] = "Joseph Bishara";

            userDto userDto = new userDto()
            {
                FirstName = "Joseph",
                LastName = "Bishara",
                EmailAddress = "joseph.bishara@streams.com",
                Role = "Admin",
                PhoneNumber = "0123456789",
                Address = "123 Stream Lane",
                DepartmentId = 1,
                JobTitle = "Software Engineer",
                BirthDate = new DateTime(1990, 1, 1),
                HireDate = new DateTime(2020, 1, 1),    


            };

            return View(userDto);
        }
    
    }
}
