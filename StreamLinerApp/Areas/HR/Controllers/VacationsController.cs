using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StreamLinerDataLayer.Data;
using StreamLinerViewModelLayer.HRViewModel;
using System.Security.Claims;

namespace StreamLinerApp.Areas.HR.Controllers;

[Area("HR")]
[Authorize]
public class VacationsController : Controller
{
    private readonly ApplicationDbContext _context;
    string ControllerName = "Leave ";
    string AppName = "Human Resource Apps";
    public VacationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = "All Vacations";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);


        var applicationDbContext = await _context.HRVacations
                 .Where(v => v.Active == false && v.VacationState < 3)
                 .Where(v=>v.CompanyId == user.CompanyId)
                 .Include(v => v.Partner)
                 .Include(v => v.HRVacationType)
                 .OrderByDescending(v => v.VacationId)
                 .ToListAsync();


        return View(applicationDbContext);

    }


    public async Task<IActionResult> AddVacationk()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = "New Leave";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == user.CompanyId), "PartnerId", "FullName");
        ViewData["VacationTypeId"] = new SelectList(_context.HRVacationType.Where(p => p.CompanyId == user.CompanyId), "VacationTypeId", "VacationTypeName");


        return View();
    }

    public async Task<IActionResult> Vacation(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = "New Leave";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        var vacation = await _context.HRVacations
              .Where(v => v.VacationId == id  )
              .Include(v => v.Partner).Include(v => v.HRVacationType)
              .FirstOrDefaultAsync();

        if (vacation == null)
        {
            return RedirectToRoute(new { action = "index" });
        }

        return View(vacation);
    }


    [HttpPost]
    public async Task<IActionResult> ApproveVacation(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);

        var vacation = await _context.HRVacations
            .Where(v => v.VacationId == id)
            .FirstOrDefaultAsync();

        vacation.VacationState = 3;
        vacation.Approved = true;
        vacation.Allowed = true;
        vacation.Active = true;
        _context.Update(vacation);
        await _context.SaveChangesAsync();


        return RedirectToRoute(new { action = "index" });
    }


    [HttpPost]
    public async Task<IActionResult> RejectVacation(RejectReasonViewModel rejectReasonViewModel)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);

        var vacation = await _context.HRVacations
           .Where(v => v.VacationId == rejectReasonViewModel.VacationId)
           .FirstOrDefaultAsync();

        vacation.VacationState = 5;
        vacation.AllowedReason = rejectReasonViewModel.RejectReason;
        vacation.Allowed = false;
        vacation.Active = false;
        _context.Update(vacation);
        await _context.SaveChangesAsync();

        return RedirectToRoute(new { action = "index" });
    }

    // Rejected
    public async Task<IActionResult> Rejected(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = " Rejected"; 
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        var vacation = await _context.HRVacations
              .Where(v => v.VacationState ==  4 || v.VacationState == 5)
                .Where(v => v.CompanyId == user.CompanyId)
              .Include(v => v.Partner).Include(v => v.HRVacationType)
                       .ToListAsync();

        return View(vacation);
    }

    // Approved
    public async Task<IActionResult> Approved(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = "Approved";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        var vacation = await _context.HRVacations
              .Where(v => v.VacationState == 3 && v.Active == true )
                .Where(v => v.CompanyId == user.CompanyId)
              .Include(v => v.Partner).Include(v => v.HRVacationType)
                      .ToListAsync();

        return View(vacation);
    }

}
