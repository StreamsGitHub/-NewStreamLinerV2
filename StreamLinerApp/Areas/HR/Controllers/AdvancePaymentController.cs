using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StreamLinerDataLayer.Data;
using StreamLinerEntitiesLayer.HREntities;
using StreamLinerLogicLayer.Services.AdvancePaymentServices;
using System.Security.Claims;

namespace StreamLinerApp.Areas.HR.Controllers;

[Area("HR")]
[Authorize]
public class AdvancePaymentController : Controller
{
    private readonly IAdvancePaymentService _advancePaymentService;
    private readonly ApplicationDbContext _context; // only if you still need Users
    private readonly string ControllerName = "Advance Payment";
    private readonly string AppName = "Human Resource";

    public AdvancePaymentController(IAdvancePaymentService service, ApplicationDbContext context)
    {
        _advancePaymentService = service;
        _context = context;
    }
    private async Task<(int userId, int companyId)> GetUserInfoAsync()
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var user = await _context.Users.FindAsync(userId);
        return (userId, user.CompanyId);
    }

    // GET: AdvancePayment
    public async Task<IActionResult> Index()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "All  ";

        var (userId, companyId) = await GetUserInfoAsync();
        var payments = await _advancePaymentService.GetAdvancePaymentsAsync(companyId);

        return View(payments);
    }
    // GET: AdvancePayment/Details/5
    public async Task<IActionResult> Details(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "  Details";
        if (id == null )
        {
            return RedirectToAction("Index");
        }
        var payment = await _advancePaymentService.GetAdvancePaymentByIdAsync(id);
        if (payment == null)
        {
            return RedirectToAction("Index");
        }
        return View(payment);
    }

    public async Task<IActionResult> Create()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "New ";
        var (userId, companyId) = await GetUserInfoAsync();
        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == companyId), "PartnerId", "FullName");
        return View();
    }

    // GET: AdvancePayment/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HRAdvancePayment advancePayment)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "New ";
        var (userId, companyId) = await GetUserInfoAsync();
        if (ModelState.IsValid)
        {
            await _advancePaymentService.CreateAdvancePaymentAsync(advancePayment, userId, companyId);
        }
        else
        {
            ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == companyId), "PartnerId", "FullName", advancePayment.PartnerId);
            return View(advancePayment);
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: AdvancePayment/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "Edit";
        var (userId, companyId) = await GetUserInfoAsync();
        var payment = await _advancePaymentService.GetAdvancePaymentByIdAsync(id);
        if (payment == null)
            return RedirectToAction(nameof(Index));
        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == companyId), "PartnerId", "FullName", payment.PartnerId);
        return View(payment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, HRAdvancePayment advancePayment)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "Edit";
        var (userId, companyId) = await GetUserInfoAsync();
        if (id != advancePayment.HRAdvancePaymentId)
        {
            return RedirectToAction(nameof(Index));
        }
        if (ModelState.IsValid)
        {
            await _advancePaymentService.UpdateAdvancePaymentAsync(advancePayment, userId);
        }
        else
        {
            ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == companyId), "PartnerId", "FullName", advancePayment.PartnerId);
            return View(advancePayment);
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: AdvancePayment/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["Action"] = "Delete";
        if (id == null)
        {
            return RedirectToAction(nameof(Index));
        }
        var payment = await _advancePaymentService.GetAdvancePaymentByIdAsync(id);
        if (payment == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(payment);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var (userId, _) = await GetUserInfoAsync();
        await _advancePaymentService.DeleteAdvancePaymentAsync(id, userId);
        return RedirectToAction(nameof(Index));
    }
}
