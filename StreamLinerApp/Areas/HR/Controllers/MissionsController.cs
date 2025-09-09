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
using System.Drawing;
using System.Globalization;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Authorization;

namespace StreamLinerApp.Areas.HR.Controllers;
[Area("HR")]
[Authorize]
public class MissionsController : Controller
{
    private readonly ApplicationDbContext _context;
    string ControllerName = "Missions";
    string AppName = "HR Apps";
    public MissionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Missions
    public async Task<IActionResult> Index()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        var applicationDbContext = _context.HRMission
            .Where(b => b.Active == true)
              .Where(h => h.CompanyId == user.CompanyId)
            .Include(h => h.HRMissionType)
            .Include(h => h.Partner)
            .OrderByDescending(m=>m.HRMissionId);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Missions/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = "All Missions";
        if (id == null || _context.HRMission == null)
        {
            return NotFound();
        }

        var hRMission = await _context.HRMission
            .Include(h => h.HRMissionType)
            .Include(h => h.Partner)
            .FirstOrDefaultAsync(m => m.HRMissionId == id);
        if (hRMission == null)
        {
            return NotFound();
        }

        return View(hRMission);
    }

    // GET: Missions/Create
    public IActionResult Create()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = "New Mission";
        ViewData["ManagerId"] = new SelectList(_context.Partner, "ManagerId", "FullName");
        ViewData["HRMissionTypeId"] = new SelectList(_context.HRMissionType, "HRMissionTypeId", "HRMissionTypeName");
        ViewData["PartnerId"] = new SelectList(_context.Partner, "PartnerId", "FullName");
        return View();
    }

    // POST: Missions/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("HRMissionId,MissionName,PartnerId,MissionDescription,HRMissionTypeId,StartTime,EndTime,ManagerId,Approved")] HRMission hRMission)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = "New Mission";
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            hRMission.CreateId = uid;
            
            hRMission.CreatedDate = DateTime.Now;
           
            hRMission.Active = true;

            string startmnthcode = Convert.ToDateTime(hRMission.StartTime).ToString("yy")
                   + Convert.ToDateTime(hRMission.StartTime).ToString("MM");
            hRMission.MonthCode = startmnthcode;

            _context.Add(hRMission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ManagerId"] = new SelectList(_context.Partner, "ManagerId", "FullName", hRMission.ManagerId);
        ViewData["HRMissionTypeId"] = new SelectList(_context.HRMissionType, "HRMissionTypeId", "HRMissionTypeName", hRMission.HRMissionTypeId);
        ViewData["PartnerId"] = new SelectList(_context.Partner, "PartnerId", "FullName", hRMission.PartnerId);
        return View(hRMission);
    }

    // GET: Missions/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = "Edit Mission";
        if (id == null || _context.HRMission == null)
        {
            return NotFound();
        }

        var hRMission = await _context.HRMission.FindAsync(id);
        if (hRMission == null)
        {
            return NotFound();
        }
        ViewData["ManagerId"] = new SelectList(_context.Partner, "ManagerId", "FullName");
        ViewData["HRMissionTypeId"] = new SelectList(_context.HRMissionType, "HRMissionTypeId", "HRMissionTypeName", hRMission.HRMissionTypeId);
        ViewData["PartnerId"] = new SelectList(_context.Partner, "PartnerId", "FullName", hRMission.PartnerId);
        return View(hRMission);
    }

    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("HRMissionId,MissionName,PartnerId,MissionDescription,HRMissionTypeId,StartTime,EndTime,ManagerId,Approved")] HRMission hRMission)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = "Edit Mission";

        if (id != hRMission.HRMissionId)
        {
            return NotFound();
        }
        string startmnthcode = Convert.ToDateTime(hRMission.StartTime).ToString("yy")
   + Convert.ToDateTime(hRMission.StartTime).ToString("MM");
        hRMission.MonthCode = startmnthcode;
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            hRMission.UpdateId = uid;
            hRMission.UpdatedDate = DateTime.Now;
            hRMission.Active = true;
            try
            {
                _context.Update(hRMission);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HRMissionExists(hRMission.HRMissionId))
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
        ViewData["ManagerId"] = new SelectList(_context.Partner, "ManagerId", "FullName" , hRMission.ManagerId);
        ViewData["HRMissionTypeId"] = new SelectList(_context.HRMissionType, "HRMissionTypeId", "HRMissionTypeName", hRMission.HRMissionTypeId);
        ViewData["PartnerId"] = new SelectList(_context.Partner, "PartnerId", "FullName", hRMission.PartnerId);
        return View(hRMission);
    }

    // GET: Missions/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = "Delete Mission";
        if (id == null || _context.HRMission == null)
        {
            return NotFound();
        }

        var hRMission = await _context.HRMission
            .Include(h => h.HRMissionType)
            .Include(h => h.Partner)
            .FirstOrDefaultAsync(m => m.HRMissionId == id);
        if (hRMission == null)
        {
            return NotFound();
        }

        return View(hRMission);
    }

    // POST: Missions/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = "Delete Mission";
        if (_context.HRMission == null)
        {
            return Problem("Entity set 'ApplicationDbContext.HRMission'  is null.");
        }
        var hRMission = await _context.HRMission.FindAsync(id);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        hRMission.DeleteId = uid;
        hRMission.DeletedDate = DateTime.Now;
        hRMission.Active = false;
        if (hRMission != null)
        {
            _context.HRMission.Update(hRMission);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool HRMissionExists(int id)
    {
        return _context.HRMission.Any(e => e.HRMissionId == id);
    }
}
