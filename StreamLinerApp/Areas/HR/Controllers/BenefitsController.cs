using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StreamLinerDataLayer.Data;
using StreamLinerEntitiesLayer.HREntities;
using StreamLinerLogicLayer.Services.BenefitServices;
using StreamLinerViewModelLayer.HRViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StreamLinerApp.Areas.HR.Controllers;

[Area("HR")]
[Authorize]
public class BenefitsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IBenefitService _benefitService;
    string ControllerName = "Benefits";
    string AppName = "HR Apps";
    public BenefitsController(ApplicationDbContext context, IBenefitService benefitService)
    {
        _context = context;
        _benefitService = benefitService;
    }
    private async Task<(int userId, int companyId)> GetUserInfoAsync()
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var user = await _context.Users.FindAsync(userId);
        return (userId, user.CompanyId);
    }

    // GET: Benefits
    public async Task<IActionResult> Index(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "All  ";
        var (userId, companyId) = await GetUserInfoAsync();
        var benefits = await _benefitService.GetAllBenefitsAsync(companyId);
        return View(benefits);
    }

    // GET: Benefits/Details/5
    public async Task<IActionResult> Details(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "Details  ";
        var benefit = await _benefitService.GetBenefitByIdAsync(id);
        if (benefit == null)
        {
            return RedirectToAction("Index");
        }
        return View(benefit);
    }

    // GET: Benefits/Create
    public async Task<IActionResult> Create()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "New";
        var (userId, companyId) = await GetUserInfoAsync();
        ViewData["HRBenefitsTypeId"] = new SelectList(_context.HRBenefitsType.Where(a => a.CompanyId == companyId), "HRBenefitsTypeId", "HRBenefitsTypeName");
        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(a => a.CompanyId == companyId), "PartnerId", "FullName");
        ViewData["ManagerId"] = new SelectList(_context.Partner.Where(a => a.CompanyId == companyId), "PartnerId", "FullName");
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BenefitsViewModel model)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "New";
        var (userId, companyId) = await GetUserInfoAsync();
        if (ModelState.IsValid)
        {
            await _benefitService.CreateBenefitAsync(model, userId, companyId);
            return RedirectToAction(nameof(Index));
        }
        else
        {
            ViewData["PartnerId"] = new SelectList(_context.Partner.Where(a => a.CompanyId == companyId), "PartnerId", "FullName", model.PartnerId);
            ViewData["HRBenefitsTypeId"] = new SelectList(_context.HRBenefitsType.Where(a => a.CompanyId == companyId), "HRBenefitsTypeId", "HRBenefitsTypeName", model.HRBenefitsTypeId);
            return View(model);
        }
        return View(model);
    }


    // GET: Benefits/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "Edit";
        var (userId, companyId) = await GetUserInfoAsync();
        var bnifits = await _benefitService.GetBenefitByIdAsync(id);
        if (bnifits == null)
        {
            return RedirectToAction("Index");
        }

        BenefitsViewModel benefitsViewModel = new BenefitsViewModel()
        {
            HRBenefitsId = bnifits.HRBenefitsId,
            PartnerId = bnifits.PartnerId,
            HRBenefitsTypeId = bnifits.HRBenefitsTypeId,
            ViewValue = bnifits.BenefitValue,
            BenefitDate = bnifits.BenefitDate,
            Description = bnifits.Description,
        };
        ViewData["HRBenefitsTypeId"] = new SelectList(_context.HRBenefitsType.Where(a => a.CompanyId == companyId), "HRBenefitsTypeId", "HRBenefitsTypeName", bnifits.HRBenefitsTypeId);
        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(a => a.CompanyId == companyId), "PartnerId", "FullName", bnifits.PartnerId);
        ViewData["ManagerId"] = new SelectList(_context.Partner.Where(a => a.CompanyId == companyId), "PartnerId", "FullName", bnifits.ManagerId);
        return View(benefitsViewModel);
    }

    // POST: Benefits/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, BenefitsViewModel model)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "Edit";
        var (userId, companyId) = await GetUserInfoAsync();
        if (!ModelState.IsValid)
        {
            ViewData["HRBenefitsTypeId"] = new SelectList(_context.HRBenefitsType.Where(a => a.CompanyId == companyId), "HRBenefitsTypeId", "HRBenefitsTypeId", model.HRBenefitsTypeId);
            ViewData["PartnerId"] = new SelectList(_context.Partner.Where(a => a.CompanyId == companyId), "PartnerId", "FullName", model.PartnerId);
            return View(model);
        }

        await _benefitService.UpdateBenefitAsync(model, userId , companyId);
        return RedirectToAction(nameof(Index));
    }

    // GET: Benefits/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "Delete";
        var benefit = await _benefitService.GetBenefitByIdAsync(id);
        if (benefit == null)
        {
            return RedirectToAction("Index");
        }
        return View(benefit);
    }

    // POST: Benefits/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var (userId, _) = await GetUserInfoAsync();
        await _benefitService.DeleteBenefitAsync(id, userId);
        return RedirectToAction(nameof(Index));

    }

   
}
