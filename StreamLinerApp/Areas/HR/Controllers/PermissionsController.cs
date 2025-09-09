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
using Microsoft.AspNetCore.Authorization;

namespace StreamLinerApp.Areas.HR.Controllers;

[Area("HR")]
[Authorize]
public class PermissionsController : Controller
{
    private readonly ApplicationDbContext _context;
    string ControllerName = "Permissions";
    string AppName = "HR Apps";
    public PermissionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Permissions
    public async Task<IActionResult> Index()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        var applicationDbContext = await _context.HRPermission
            .Where(b => b.PermissionState < 3)
             .Where(h => h.CompanyId == user.CompanyId)
            .Include(h => h.Partner)
            .OrderByDescending(h => h.HRPermissionId).ToListAsync();
        return View(applicationDbContext);
    }

    // Rejected
    public async Task<IActionResult> Rejected(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        var applicationDbContext = await _context.HRPermission
            .Where(b => b.PermissionState > 3)
              .Where(h => h.CompanyId == user.CompanyId)
            .Include(h => h.Partner).ToListAsync();
        return View(applicationDbContext);
    }

    // Approved
    public async Task<IActionResult> Approved(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        var applicationDbContext = await _context.HRPermission
            .Where(b => b.PermissionState ==  3)
             .Where(h => h.CompanyId == user.CompanyId)
              .Include(h => h.Partner).ToListAsync();
        return View(applicationDbContext);
    }


    // GET: Permissions/Details/5
    public async Task<IActionResult> Permission(int? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        if (id == null || _context.HRPermission == null)
        {
            return NotFound();
        }

        var hRPermission = await _context.HRPermission
            .Include(h => h.Partner)
            .FirstOrDefaultAsync(m => m.HRPermissionId == id);
        if (hRPermission == null)
        {
            return NotFound();
        }

        return View(hRPermission);
    }



    // GET: Permissions/Create
    public IActionResult Create()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["PartnerId"] = new SelectList(_context.Partner, "PartnerId", "FullName");
        return View();
    }


    // POST: Permissions/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("HRPermissionId,PermissionName,PartnerId,Reason,StartTime,EndTime,Approved")] HRPermission hRPermission)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            hRPermission.CreateId = uid;
       
            hRPermission.CreatedDate = DateTime.Now;
    
            hRPermission.Active = true;

            string startmnthcode = Convert.ToDateTime(hRPermission.StartTime).ToString("yy")
                 + Convert.ToDateTime(hRPermission.StartTime).ToString("MM");
            hRPermission.MonthCode = startmnthcode;
            
            _context.Add(hRPermission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["PartnerId"] = new SelectList(_context.Partner, "PartnerId", "FullName", hRPermission.PartnerId);
        return View(hRPermission);
    }

    // GET: Permissions/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        if (id == null || _context.HRPermission == null)
        {
            return NotFound();
        }

        var hRPermission = await _context.HRPermission.FindAsync(id);
        if (hRPermission == null)
        {
            return NotFound();
        }
        ViewData["PartnerId"] = new SelectList(_context.Partner, "PartnerId", "FirstName", hRPermission.PartnerId);
        return View(hRPermission);
    }

    // POST: Permissions/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("HRPermissionId,PermissionName,PartnerId,Reason,StartTime,EndTime,Approved")] HRPermission hRPermission)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        if (id != hRPermission.HRPermissionId)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            hRPermission.UpdateId = uid;
            hRPermission.UpdatedDate = DateTime.Now;
            hRPermission.Active = true;
            try
            {
                _context.Update(hRPermission);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HRPermissionExists(hRPermission.HRPermissionId))
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
        ViewData["PartnerId"] = new SelectList(_context.Partner, "PartnerId", "FullName", hRPermission.PartnerId);
        return View(hRPermission);
    }

    // GET: Permissions/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        if (id == null || _context.HRPermission == null)
        {
            return NotFound();
        }

        var hRPermission = await _context.HRPermission
            .Include(h => h.Partner)
            .FirstOrDefaultAsync(m => m.HRPermissionId == id);
        if (hRPermission == null)
        {
            return NotFound();
        }

        return View(hRPermission);
    }

    // POST: Permissions/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        if (_context.HRPermission == null)
        {
            return Problem("Entity set 'ApplicationDbContext.HRPermission'  is null.");
        }
        var hRPermission = await _context.HRPermission.FindAsync(id);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        hRPermission.DeleteId = uid;
        hRPermission.DeletedDate = DateTime.Now;
        hRPermission.Active = false;
        if (hRPermission != null)
        {
            _context.HRPermission.Update(hRPermission);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool HRPermissionExists(int id)
    {
        return _context.HRPermission.Any(e => e.HRPermissionId == id);
    }
}
