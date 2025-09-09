using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StreamLinerDataLayer.Data;
using StreamLinerEntitiesLayer.HREntities;
using StreamLinerViewModelLayer.HRViewModel;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
//using ShareKiteWeb.Migrations;

namespace StreamLinerApp.Areas.HR.Controllers;
[Area("HR")]
[Authorize]
public class PenaltiesController : Controller
{
    private readonly ApplicationDbContext _context;
    string ControllerName = "Penalties";
    string AppName = "HR Apps";
    public PenaltiesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Penalties
    public async Task<IActionResult> Index()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        var applicationDbContext = await  _context.HRPenalty
            .Where(b => b.Active == true)
            .Where(b => b.CompanyId== user.CompanyId)
            .Include(h => h.HRPenaltyTypes)
            .Include(h => h.Partner)
            .ToListAsync();
        return View(applicationDbContext);
    }



    // GET: Penalties/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);

        var hRPenalty = await _context.HRPenalty
            .Include(h => h.HRPenaltyTypes)
            .Include(h => h.Partner)
            .FirstOrDefaultAsync(m => m.HRPenaltyId == id);
        
        return View(hRPenalty);
    }

    // GET: Penalties/Create
    public async Task<IActionResult> Create()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        ViewData["HRPenaltyTypesId"] = new SelectList(_context.HRPenaltyTypes.Where(p=>p.CompanyId == user.CompanyId), "HRPenaltyTypesId", "HRPenaltyTypeName");
        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p=>p.CompanyId == user.CompanyId), "PartnerId", "FullName");
       
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PartnerId,PenaltyDate,ViewValue,PenaltyHour,Description,PenaltyType,HRPenaltyTypesId")] PenaltiesViewModel hRPenaltyview)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);

        string mnthcode = Convert.ToDateTime(hRPenaltyview.PenaltyDate).ToString("yy")
                   + Convert.ToDateTime(hRPenaltyview.PenaltyDate).ToString("MM");


        var emp = await _context.Partner.FindAsync(hRPenaltyview.PartnerId);
        var Manager = await _context.Partner.FindAsync(emp.ManagerId);



        HRPenalty Penalty = new HRPenalty
        {
            PartnerId = hRPenaltyview.PartnerId,
            ManagerId = Manager.PartnerId,
            ManagerName = Manager.FullName,
            PenaltyDate = hRPenaltyview.PenaltyDate,
            PenaltyValue = hRPenaltyview.ViewValue,
            PenaltyHour = hRPenaltyview.PenaltyHour,
            Description = hRPenaltyview.Description,
            MonthCode = mnthcode,
            HRPenaltyTypesId = hRPenaltyview.HRPenaltyTypesId,
            Allowed = true,
            Approved = true,
            Active = true,
            CompanyId = user.CompanyId,
        };

        // ViewValue = القيمة اللي جايلك من الفيو
        // PenaltyValue = القيمة اللي في الموديل

        var employee = await _context.Partner.FindAsync(hRPenaltyview.PartnerId);
        decimal salary = employee.Salary;
        int HRshiftId = employee.HRShiftAttendId;

        var Shift = await _context.HRShiftAttend.FindAsync(HRshiftId);
        //decimal countOfHourShift = Shift.ShiftCount;

        if (hRPenaltyview.PenaltyType == 1)
        {
            //  ViewValue = فلوس
            Penalty.PenaltyValue = hRPenaltyview.ViewValue;

        }
        else if (hRPenaltyview.PenaltyType == 2)
        {
            // ViewValue  =     ايام
            // PenaltyValue  =  salary / 30 * ViewValue

            Penalty.PenaltyDays = hRPenaltyview.ViewValue;
            Penalty.PenaltyValue = salary / 30 * hRPenaltyview.ViewValue;
        }
        else if (hRPenaltyview.PenaltyType == 3)
        {
            // ViewValue=   ساعات 
            //   PenaltyValue = Salary / 30 / countOfHourShift * ViewValue

            hRPenaltyview.ViewValue = Penalty.PenaltyHour;
            //Penalty.PenaltyValue = salary * 30 / countOfHourShift / Penalty.PenaltyDays;
            //Penalty.PenaltyValue = salary / 30 / countOfHourShift * hRPenaltyview.ViewValue;

        }
        if (ModelState.IsValid)
        {
          
            Penalty.CreateId = uid;
           
            Penalty.CreatedDate = DateTime.Now;
           
            Penalty.Active = true;
            _context.Add(Penalty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == user.CompanyId), "PartnerId", "FullName", hRPenaltyview.PartnerId);
        //ViewData["ManagerId"] = new SelectList(_context.Partner, "PartnerId", "FullName", hRPenaltyview.ManagerId);
        ViewData["HRPenaltyTypesId"] = new SelectList(_context.HRPenaltyTypes.Where(p => p.CompanyId == user.CompanyId), "HRPenaltyTypesId", "HRPenaltyTypeName", hRPenaltyview.HRPenaltyTypesId);
        return View(hRPenaltyview);
    }




    // GET: Penalties/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);

        if (id == null || _context.HRPenalty == null)
        {
            return NotFound();
        }

        var hRPenalty = await _context.HRPenalty.FindAsync(id);
        if (hRPenalty == null)
        {
            return NotFound();
        }
        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == user.CompanyId), "PartnerId", "FullName", hRPenalty.Partner);
        //ViewData["ManagerId"] = new SelectList(_context.Partner, "PartnerId", "FullName", hRPenalty.ManagerId);
        ViewData["HRPenaltyTypesId"] = new SelectList(_context.HRPenaltyTypes.Where(p => p.CompanyId == user.CompanyId), "HRPenaltyTypesId", "HRPenaltyTypeName", hRPenalty.HRPenaltyTypesId);

        return View(hRPenalty);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Edit(int id, [Bind("HRPenaltyId,PartnerId,ManagerId,PenaltyDays,PenaltyDate,PenaltyValue,PenaltyHour,Description,MonthCode,Approved,HRPenaltyTypesId")] HRPenalty hRPenalty)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        if (id != hRPenalty.HRPenaltyId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
  
            hRPenalty.UpdateId = uid;
            hRPenalty.UpdatedDate = DateTime.Now;
            hRPenalty.Active = true;
            try
            {
                _context.Update(hRPenalty);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HRPenaltyExists(hRPenalty.HRPenaltyId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == user.CompanyId), "PartnerId", "FullName", hRPenalty.PartnerId);
        ViewData["HRPenaltyTypesId"] = new SelectList(_context.HRPenaltyTypes.Where(p => p.CompanyId == user.CompanyId), "HRPenaltyTypesId", "HRPenaltyTypeName", hRPenalty.HRPenaltyTypesId);

        return View(hRPenalty);
    }


    // GET: Penalties/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        if (id == null || _context.HRPenalty == null)
        {
            return NotFound();
        }

        var hRPenalty = await _context.HRPenalty
             .Include(h => h.HRPenaltyTypes)
            .Include(h => h.Partner)
            .FirstOrDefaultAsync(m => m.HRPenaltyId == id);
        if (hRPenalty == null)
        {
            return NotFound();
        }

        return View(hRPenalty);
    }

    // POST: Penalties/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        if (_context.HRPenalty == null)
        {
            return Problem("Entity set 'ApplicationDbContext.HRPenalty'  is null.");
        }
        var hRPenalty = await _context.HRPenalty.FindAsync(id);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        hRPenalty.DeleteId = uid;
        hRPenalty.DeletedDate = DateTime.Now;
        hRPenalty.Active = false;
        if (hRPenalty != null)
        {
            _context.HRPenalty.Update(hRPenalty);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool HRPenaltyExists(int id)
    {
        return _context.HRPenalty.Any(e => e.HRPenaltyId == id);
    }
}
