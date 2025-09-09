using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using StreamLinerDataLayer.Data;
using StreamLinerViewModelLayer.HRViewModel;
using StreamLinerEntitiesLayer.HREntities;

namespace StreamLinerApp.Areas.HR.Controllers;
//[Authorize(Roles = "PowerUser")]

[Area("HR")]
[Authorize]
public class DepartmentsController : Controller
{
    private readonly ApplicationDbContext _context;
    string ControllerName = "Departments";
    string AppName = "Human Resource";
    public DepartmentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Departments
    public async Task<IActionResult> Index()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "All Departments";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);

        var deptlist = await _context.HRDepartment.Where(b => b.Active == true  ).ToListAsync();
        List<DepartmentViewModel> modellist = new List<DepartmentViewModel>();

        foreach (var item in deptlist)
        {
            DepartmentViewModel model = new DepartmentViewModel();
            model.DepartmentName = item.DepartmentName;
            model.ParentId = item.ParentId;
            model.DepartmentId = item.DepartmentId;
            var parent = await _context.HRDepartment.FindAsync(item.ParentId);
            if (parent != null)
                model.ParentName = parent.DepartmentName;
            else
                model.ParentName = "";



            model.EmployeeCount = await _context.Partner.Where(b => b.DepartmentId == item.DepartmentId).CountAsync();
            modellist.Add(model);
        }


        return View(modellist);
    }

    // GET: Departments/Create
    public async Task<IActionResult> Create()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);

        ViewData["ControllerName"] = ControllerName;
        ViewData["Action"] = "New Department";
        ViewData["ParentId"] = new SelectList(await _context.HRDepartment.Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync(), "DepartmentId", "DepartmentName");
        ViewData["ManagerId"] = new SelectList(await _context.Partner.Where(b => b.CompanyId == user.CompanyId).ToListAsync(), "PartnerId", "FullName");

        return View();
    }

    // POST: Departments/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("DepartmentId,DepartmentName,ParentId,HRManagerId,DepartmentDesc")] HRDepartment hRDepartment)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["Action"] = "New Department";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        if (ModelState.IsValid)
        {

            hRDepartment.CreateId = uid;
            hRDepartment.CompanyId = user.CompanyId;
            hRDepartment.CreatedDate = DateTime.Now;

            hRDepartment.Active = true;

            _context.Add(hRDepartment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ParentId"] = new SelectList(await _context.HRDepartment.Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync(), "DepartmentId", "DepartmentName");
        ViewData["ManagerId"] = new SelectList(await _context.Partner.Where(b => b.CompanyId == user.CompanyId).ToListAsync(), "PartnerId", "FullName");
        return View(hRDepartment);
    }

    // GET: Departments/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["Action"] = "Edit Department";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        ViewData["ParentId"] = new SelectList(await _context.HRDepartment.Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync(), "DepartmentId", "DepartmentName");
        ViewData["ManagerId"] = new SelectList(await _context.Partner.Where(b => b.CompanyId == user.CompanyId).ToListAsync(), "PartnerId", "FullName");

        if (id == null || _context.HRDepartment == null)
        {
            return NotFound();
        }

        var hRDepartment = await _context.HRDepartment.FindAsync(id);
        if (hRDepartment == null)
        {
            return NotFound();
        }
        return View(hRDepartment);
    }

    // POST: Departments/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,DepartmentName,ParentId,HRManagerId,DepartmentDesc")] HRDepartment hRDepartment)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["Action"] = "Edit Department";

        if (id != hRDepartment.DepartmentId)
        {
            return NotFound();
        }
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        if (ModelState.IsValid)
        {

            hRDepartment.UpdateId = uid;
            hRDepartment.UpdatedDate = DateTime.Now;
            hRDepartment.CompanyId = user.CompanyId;
            hRDepartment.Active = true;
            try
            {
                _context.Update(hRDepartment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HRDepartmentExists(hRDepartment.DepartmentId))
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
        ViewData["ParentId"] = new SelectList(await _context.HRDepartment.Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync(), "DepartmentId", "DepartmentName");
        ViewData["ManagerId"] = new SelectList(await _context.Partner.Where(b => b.CompanyId == user.CompanyId).ToListAsync(), "PartnerId", "FullName");
        return View(hRDepartment);
    }

    // GET: Departments/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["Action"] = "Delete Department";
        if (id == null || _context.HRDepartment == null)
        {
            return NotFound();
        }

        var hRDepartment = await _context.HRDepartment
            .FirstOrDefaultAsync(m => m.DepartmentId == id);
        if (hRDepartment == null)
        {
            return NotFound();
        }

        return View(hRDepartment);
    }

    // POST: Departments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.HRDepartment == null)
        {
            return Problem("Entity set 'ApplicationDbContext.HRDepartment'  is null.");
        }
        var hRDepartment = await _context.HRDepartment.FindAsync(id);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        hRDepartment.DeleteId = uid;
        hRDepartment.DeletedDate = DateTime.Now;
        hRDepartment.Active = false;
        if (hRDepartment != null)
        {
            _context.HRDepartment.Update(hRDepartment);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    private bool HRDepartmentExists(int id)
    {
        return _context.HRDepartment.Any(e => e.DepartmentId == id);
    }
}
