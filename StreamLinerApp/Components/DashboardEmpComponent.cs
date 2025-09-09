using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StreamLinerDataLayer.Data;
using StreamLinerEntitiesLayer.Entities;

namespace StreamLinerApp.Components
{
    [ViewComponent(Name = "DashboardEmp")]
    public class DashboardEmpComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardEmpComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View();
        }
    }
}
