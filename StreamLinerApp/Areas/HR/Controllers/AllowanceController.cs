using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StreamLinerDataLayer.Data;
using StreamLinerEntitiesLayer.HREntities;
using StreamLinerLogicLayer.Services.AllowanceServices;
using StreamLinerViewModelLayer.HRViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StreamLinerApp.Areas.HR.Controllers;

[Area("HR")]
[Authorize]
public class AllowanceController : Controller
{
    private readonly ApplicationDbContext _context;
    string ControllerName = "Allowance";
    string AppName = "HR Apps";
    private readonly IAllowanceService _allowanceService;

    public AllowanceController(IAllowanceService service, ApplicationDbContext context)
    {
        _allowanceService = service;
        _context = context;
    }

    private async Task<(int userId, int companyId)> GetUserInfoAsync()
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var user = await _context.Users.FindAsync(userId);
        return (userId, user.CompanyId);
    }

    // GET: Allowance
    public async Task<IActionResult> Index()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "All  ";
        var (userId, companyId) = await GetUserInfoAsync();
        var allowances = await _allowanceService.GetAllowancesAsync(companyId);
        return View(allowances);
    }

    // GET: Allowance/Details/5
    public async Task<IActionResult> Details(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "Details  ";
        var allowance = await _allowanceService.GetAllowanceByIdAsync(id);
        if (allowance == null)
        {
            return RedirectToAction("Index");
        }
        return View(allowance);
    }

    // GET: Allowance/Create
    public async Task<IActionResult> Create()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "New";
        var (userId, companyId) = await GetUserInfoAsync();
        ViewData["HRAllowanceTypeId"] = new SelectList(_context.HRAllowanceType.Where(a => a.CompanyId == companyId), "HRAllowanceTypeId", "HRAllowanceTypeName");
        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(a => a.CompanyId == companyId), "PartnerId", "FullName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AllowanceViewModel model)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "New";
        var (userId, companyId) = await GetUserInfoAsync();
        if (ModelState.IsValid)
        {
            await _allowanceService.CreateAllowanceAsync(model, userId, companyId);
        }
        else
        {
            ViewData["HRAllowanceTypeId"] = new SelectList(_context.HRAllowanceType.Where(a => a.CompanyId == companyId), "HRAllowanceTypeId", "HRAllowanceTypeName", model.HRAllowanceTypeId);
            ViewData["PartnerId"] = new SelectList(_context.Partner.Where(a => a.CompanyId == companyId), "PartnerId", "FullName", model.PartnerId);
            return View(model);
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Allowance/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "Edit";
        var (userId, companyId) = await GetUserInfoAsync();
        var allowance = await _allowanceService.GetAllowanceByIdAsync(id);
        if (allowance == null)
        {
            return RedirectToAction("Index");
        }

        var viewModel = new AllowanceViewModel
        {
            HRAllowanceId = allowance.HRAllowanceId,
            PartnerId = allowance.PartnerId,
            HRAllowanceTypeId = allowance.HRAllowanceTypeId,
            AllowanceDate = allowance.AllowanceDate,
            Description = allowance.Description,
            ViewValue = allowance.AllowanceValue
        };

        ViewData["HRAllowanceTypeId"] = new SelectList(_context.HRAllowanceType.Where(a => a.CompanyId == companyId), "HRAllowanceTypeId", "HRAllowanceTypeName", allowance.HRAllowanceTypeId);
        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(a => a.CompanyId == companyId), "PartnerId", "FullName", allowance.PartnerId);

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AllowanceViewModel model)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "Edit";
        var (userId, companyId) = await GetUserInfoAsync();

        if (id != model.HRAllowanceId)
        {
            return RedirectToAction("Index");
        }

        if (!ModelState.IsValid)
        {
            ViewData["HRAllowanceTypeId"] = new SelectList(_context.HRAllowanceType.Where(a => a.CompanyId == companyId), "HRAllowanceTypeId", "HRAllowanceTypeName", model.HRAllowanceTypeId);
            ViewData["PartnerId"] = new SelectList(_context.Partner.Where(a => a.CompanyId == companyId), "PartnerId", "FullName", model.PartnerId);
            return View(model);
        }

        await _allowanceService.UpdateAllowanceAsync(model, userId);
        return RedirectToAction(nameof(Index));
    }
    // GET: Allowance/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "Delete";
        var allowance = await _allowanceService.GetAllowanceByIdAsync(id);
        if (allowance == null)
        {
            return RedirectToAction("Index");
        }
        return View(allowance);
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var (userId, _) = await GetUserInfoAsync();
        await _allowanceService.DeleteAllowanceAsync(id, userId);
        return RedirectToAction(nameof(Index));
    }

}
