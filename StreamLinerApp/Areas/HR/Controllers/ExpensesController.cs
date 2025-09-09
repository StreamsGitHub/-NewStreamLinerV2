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
public class ExpensesController : Controller
{
    private readonly ApplicationDbContext _context;
    string ControllerName = "Expenses";
    string AppName = "HR Apps";
    public ExpensesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Expenses
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


        var applicationDbContext =await _context.HRExpenses
            .Where(b => b.Active == true && b.CompanyId == user.CompanyId)
            .Include(h => h.Partner)
             .OrderByDescending(h => h.HRExpensesId)
             .ToListAsync();

        return View(applicationDbContext);
    }

    // GET: Expenses/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        if (id == null || _context.HRExpenses == null)
        {
            return NotFound();
        }

        var hRExpenses = await _context.HRExpenses
            .Include(h => h.Partner)
            .FirstOrDefaultAsync(m => m.HRExpensesId == id);
        if (hRExpenses == null)
        {
            return NotFound();
        }

        return View(hRExpenses);
    }

    // GET: Expenses/Create
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
    public async Task<IActionResult> Create([Bind("PartnerId,ExpensesDate,HRExpensesValue,Description")] HRExpenses hRExpenses)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        //string mnthcode = Convert.ToDateTime(expenses.da).ToString("yy")
        //           + Convert.ToDateTime(expenses.BenefitDate).ToString("MM");
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);

        string mnthcode = Convert.ToDateTime(hRExpenses.ExpensesDate).ToString("yy")
                 + Convert.ToDateTime(hRExpenses.ExpensesDate).ToString("MM");
        var emp = await _context.Partner.FindAsync(hRExpenses.PartnerId);
        var Manager = await _context.Partner.FindAsync(emp.ManagerId);
        hRExpenses.ManagerId = Manager.PartnerId;
        hRExpenses.ManagerName = Manager.FullName;
        hRExpenses.CreateId = uid;
        hRExpenses.CreatedDate = DateTime.Now;
        hRExpenses.Approved = true;
        hRExpenses.Allowed = true;
        hRExpenses.Active = true;
        hRExpenses.MonthCode = mnthcode;
        hRExpenses.CompanyId = user.CompanyId;

        _context.Add(hRExpenses);
        await _context.SaveChangesAsync();
        return RedirectToRoute(new { controller = "Expenses", action = "Index" });

        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == user.CompanyId), "PartnerId", "FullName", hRExpenses.PartnerId);
        return View(hRExpenses);
    }


    // GET: Expenses/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        if (id == null || _context.HRExpenses == null)
        {
            return NotFound();
        }

        var hRExpenses = await _context.HRExpenses.FindAsync(id);
        if (hRExpenses == null)
        {
            return NotFound();
        }

        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == user.CompanyId), "PartnerId", "FullName", hRExpenses.PartnerId);

        return View(hRExpenses);
    }

    // POST: Expenses/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("HRExpensesId,PartnerId,ExpensesDate,HRExpensesValue,Description")] HRExpenses hRExpenses)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        if (id != hRExpenses.HRExpensesId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {

            hRExpenses.UpdateId = uid;
            hRExpenses.UpdatedDate = DateTime.Now;
            hRExpenses.Active = true;
            try
            {
                _context.Update(hRExpenses);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HRExpensesExists(hRExpenses.HRExpensesId))
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
        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == user.CompanyId), "PartnerId", "FullName", hRExpenses.PartnerId);
        return View(hRExpenses);
    }

    // GET: Expenses/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        if (id == null || _context.HRExpenses == null)
        {
            return NotFound();
        }

        var hRExpenses = await _context.HRExpenses
            .Include(h => h.Partner)
            .FirstOrDefaultAsync(m => m.HRExpensesId == id);
        if (hRExpenses == null)
        {
            return NotFound();
        }

        return View(hRExpenses);
    }

    // POST: Expenses/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        if (_context.HRExpenses == null)
        {
            return Problem("Entity set 'ApplicationDbContext.HRExpenses'  is null.");
        }
        var hRExpenses = await _context.HRExpenses.FindAsync(id);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        hRExpenses.DeleteId = uid;
        hRExpenses.DeletedDate = DateTime.Now;
        hRExpenses.Active = false;
        if (hRExpenses != null)
        {
            _context.HRExpenses.Remove(hRExpenses);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool HRExpensesExists(int id)
    {
        return _context.HRExpenses.Any(e => e.HRExpensesId == id);
    }
}
