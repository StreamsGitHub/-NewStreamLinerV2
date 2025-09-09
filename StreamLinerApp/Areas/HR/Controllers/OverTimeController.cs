using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StreamLinerDataLayer.Data;
using StreamLinerEntitiesLayer.HREntities;
using System.Security.Claims;
using StreamLinerViewModelLayer.HRViewModel;
using Microsoft.AspNetCore.Authorization;

namespace StreamLinerApp.Areas.HR.Controllers;
[Area("HR")]
[Authorize]
public class OverTimeController : Controller
{
    private readonly ApplicationDbContext _context;
    string ControllerName = "OverTime";
    string AppName = "HR Apps";
    public OverTimeController(ApplicationDbContext context)
    {
        _context = context;
    }


    // GET: OverTime
    public async Task<IActionResult> Index(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);

        //Partner partner = await _context.Partner.Where(c => c.PartnerId == id).FirstOrDefaultAsync();
        //if (partner.CompanyId != user.CompanyId)
        //{
        //    return RedirectToRoute(new { controller = "Employees", action = "Index" });
        //}


        var applicationDbContext = _context.HROverTimes
            .Where(b => b.Active == true && b.CompanyId == user.CompanyId)
            .Include(h => h.Partner)
            .OrderByDescending(h => h.OverTimesId);

        return View(await applicationDbContext.ToListAsync());
    }

    // GET: OverTime/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        if (id == null || _context.HROverTimes == null)
        {
            return NotFound();
        }

        var HROverTimes = await _context.HROverTimes
            .Include(h => h.Partner)
            .FirstOrDefaultAsync(m => m.OverTimesId == id);
        if (HROverTimes == null)
        {
            return NotFound();
        }

        return View(HROverTimes);
    }

    // GET: OverTime/Create
    public async Task<IActionResult> Create()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);

        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == user.CompanyId), "PartnerId", "FullName");
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PartnerId,OverTimeDate,OverTimeValue,Description")] HROverTimes HROverTimes)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        //string mnthcode = Convert.ToDateTime(OverTime.da).ToString("yy")
        //           + Convert.ToDateTime(OverTime.BenefitDate).ToString("MM");
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);

        string mnthcode = Convert.ToDateTime(HROverTimes.OverTimeDate).ToString("yy")
                 + Convert.ToDateTime(HROverTimes.OverTimeDate).ToString("MM");
        var emp = await _context.Partner.FindAsync(HROverTimes.PartnerId);
        var Manager = await _context.Partner.FindAsync(emp.ManagerId);
        HROverTimes.ManagerId = Manager.PartnerId;
        HROverTimes.ManagerName = Manager.FullName;
        HROverTimes.CreateId = uid;
        HROverTimes.CreatedDate = DateTime.Now;
        HROverTimes.Approved = true;
        HROverTimes.Allowed = true;
        HROverTimes.Active = true;
        HROverTimes.MonthCode = mnthcode;
        HROverTimes.CompanyId = user.CompanyId;

        _context.Add(HROverTimes);
        await _context.SaveChangesAsync();
        return RedirectToRoute(new { controller = "OverTime", action = "Index" });

        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == user.CompanyId), "PartnerId", "FullName", HROverTimes.PartnerId);
        return View(HROverTimes);
    }


    // GET: OverTime/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        if (id == null || _context.HROverTimes == null)
        {
            return NotFound();
        }

        var HROverTimes = await _context.HROverTimes.FindAsync(id);
        if (HROverTimes == null)
        {
            return NotFound();
        }

        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == user.CompanyId), "PartnerId", "FullName", HROverTimes.PartnerId);

        return View(HROverTimes);
    }

    // POST: OverTime/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("OverTimesId,PartnerId,OverTimeDate,OverTimeValue,Description")] HROverTimes HROverTimes)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        if (id != HROverTimes.OverTimesId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {

            HROverTimes.UpdateId = uid;
            HROverTimes.UpdatedDate = DateTime.Now;
            HROverTimes.Active = true;
            try
            {
                _context.Update(HROverTimes);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HROverTimesExists(HROverTimes.OverTimesId))
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
        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == user.CompanyId), "PartnerId", "FullName", HROverTimes.PartnerId);
        return View(HROverTimes);
    }

    // GET: OverTime/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        if (id == null || _context.HROverTimes == null)
        {
            return NotFound();
        }

        var HROverTimes = await _context.HROverTimes
            .Include(h => h.Partner)
            .FirstOrDefaultAsync(m => m.OverTimesId == id);
        if (HROverTimes == null)
        {
            return NotFound();
        }

        return View(HROverTimes);
    }

    // POST: OverTime/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        if (_context.HROverTimes == null)
        {
            return Problem("Entity set 'ApplicationDbContext.HROverTimes'  is null.");
        }
        var HROverTimes = await _context.HROverTimes.FindAsync(id);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        HROverTimes.DeleteId = uid;
        HROverTimes.DeletedDate = DateTime.Now;
        HROverTimes.Active = false;
        if (HROverTimes != null)
        {
            _context.HROverTimes.Remove(HROverTimes);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool HROverTimesExists(int id)
    {
        return _context.HROverTimes.Any(e => e.OverTimesId == id);
    }
}
