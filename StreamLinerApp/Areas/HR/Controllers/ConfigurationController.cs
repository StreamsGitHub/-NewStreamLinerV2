using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StreamLinerDataLayer.Data;
using StreamLinerEntitiesLayer.HREntities;
using StreamLinerViewModelLayer.HRViewModel;
using System.Security.Claims;

namespace StreamLinerApp.Areas.HR.Controllers
{
    [Area("HR")]
    [Authorize]

    public class ConfigurationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        string ControllerName = "Configuration";
        string AppName = "HR Configuration";
        public ConfigurationController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;

            return View();
        }

        // GET: Departments
        public async Task<IActionResult> Departments()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Departments";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);

            var deptlist = await _context.HRDepartment.Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync();
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
        public async Task<IActionResult> CreateDepartment()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);

            ViewData["ControllerName"] = ControllerName;
            ViewData["Action"] = "Add Department";
            ViewData["ParentId"] = new SelectList(await _context.HRDepartment.Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync(), "DepartmentId", "DepartmentName");
            ViewData["ManagerId"] = new SelectList(await _context.Partner.Where(b => b.CompanyId == user.CompanyId).ToListAsync(), "PartnerId", "FullName");

            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDepartment([Bind("DepartmentId,DepartmentName,ParentId,HRManagerId,DepartmentDesc")] HRDepartment hRDepartment)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["Action"] = "Add Department";
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
                return RedirectToAction(nameof(Departments));
            }
            ViewData["ParentId"] = new SelectList(await _context.HRDepartment.Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync(), "DepartmentId", "DepartmentName");
            ViewData["ManagerId"] = new SelectList(await _context.Partner.Where(b => b.CompanyId == user.CompanyId).ToListAsync(), "PartnerId", "FullName");
            return View(hRDepartment);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> EditDepartment(int? id)
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
        public async Task<IActionResult> EditDepartment(int id, [Bind("DepartmentId,DepartmentName,ParentId,HRManagerId,DepartmentDesc")] HRDepartment hRDepartment)
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
        public async Task<IActionResult> DeleteDepartment(int? id)
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
        [HttpPost, ActionName("DeleteDepartment")]
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



        // Stages
        public async Task<IActionResult> Stages()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Stages";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);
            return View(await _context.HRStage.Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync());
        }
        // GET: Stages/Create
        public IActionResult CreateStage()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            return View();
        }

        // POST: Stages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStage([Bind("HRStageId,StageName,Requirements")] HRStage hRStage)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Stage";
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);

                hRStage.CreateId = uid;

                hRStage.CreatedDate = DateTime.Now;
                hRStage.CompanyId = user.CompanyId;
                hRStage.Active = true;

                _context.Add(hRStage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Stages));
            }
            return View(hRStage);
        }

        // GET: Stages/Edit/5
        public async Task<IActionResult> EditStage(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Stage";
            if (id == null || _context.HRStage == null)
            {
                return NotFound();
            }

            var hRStage = await _context.HRStage.FindAsync(id);
            if (hRStage == null)
            {
                return NotFound();
            }
            return View(hRStage);
        }

        // POST: Stages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStage(int id, [Bind("HRStageId,StageName,Requirements")] HRStage hRStage)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Stage";
            if (id != hRStage.HRStageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRStage.UpdateId = uid;
                hRStage.UpdatedDate = DateTime.Now;
                hRStage.CompanyId = user.CompanyId;
                hRStage.Active = true;
                try
                {
                    _context.Update(hRStage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HRStageExists(hRStage.HRStageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Stages));
            }
            return View(hRStage);
        }

        // GET: Stages/Delete/5
        public async Task<IActionResult> DeleteStage(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Stage";
            if (id == null || _context.HRStage == null)
            {
                return NotFound();
            }

            var hRStage = await _context.HRStage
                .FirstOrDefaultAsync(m => m.HRStageId == id);
            if (hRStage == null)
            {
                return NotFound();
            }

            return View(hRStage);
        }

        // POST: Stages/Delete/5
        [HttpPost, ActionName("DeleteStage")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStageConfirmed(int id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Stage";
            if (_context.HRStage == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HRStage'  is null.");
            }
            var hRStage = await _context.HRStage.FindAsync(id);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            hRStage.DeleteId = uid;
            hRStage.DeletedDate = DateTime.Now;
            hRStage.Active = false;
            if (hRStage != null)
            {
                _context.HRStage.Update(hRStage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Stages));
        }

        private bool HRStageExists(int id)
        {
            return _context.HRStage.Any(e => e.HRStageId == id);
        }

        // JobPositions
        public async Task<IActionResult> JobPositions()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Job Positions";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);
            var applicationDbContext = _context.HRJobPositions.Include(h => h.HRDepartment);
            return View(await applicationDbContext.Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync());
        }
        // GET: JobPositions/Create
        public async Task<IActionResult> CreateJobPosition()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);
            ViewData["DepartmentId"] = new SelectList(_context.HRDepartment.Where(c => c.CompanyId == user.CompanyId), "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: JobPositions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateJobPosition([Bind("JobId,JobTitel,DepartmentId,ExpectedNewEmployees,JobDescription")] HRJobPositions hRJobPositions)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;

            ViewData["Action"] = "Add Job Position";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);
            if (ModelState.IsValid)
            {

                hRJobPositions.CreateId = uid;
                hRJobPositions.CreatedDate = DateTime.Now;
                hRJobPositions.Active = true;
                hRJobPositions.CompanyId = user.CompanyId;

                _context.Add(hRJobPositions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(JobPositions));
            }
            ViewData["DepartmentId"] = new SelectList(_context.HRDepartment.Where(c => c.CompanyId == user.CompanyId), "DepartmentId", "DepartmentName", hRJobPositions.DepartmentId);
            return View(hRJobPositions);
        }

        // GET: JobPositions/Edit/5
        public async Task<IActionResult> EditJobPosition(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Job Position";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);
            if (id == null || _context.HRJobPositions == null)
            {
                return NotFound();
            }

            var hRJobPositions = await _context.HRJobPositions.FindAsync(id);
            if (hRJobPositions == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.HRDepartment.Where(d => d.CompanyId == user.CompanyId), "DepartmentId", "DepartmentName", hRJobPositions.DepartmentId);
            return View(hRJobPositions);
        }

        // POST: JobPositions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditJobPosition(int id, [Bind("JobId,JobTitel,DepartmentId,ExpectedNewEmployees,JobDescription")] HRJobPositions hRJobPositions)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Job Position";
            if (id != hRJobPositions.JobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRJobPositions.UpdateId = uid;
                hRJobPositions.UpdatedDate = DateTime.Now;
                hRJobPositions.CompanyId = user.CompanyId;
                hRJobPositions.Active = true;
                try
                {
                    _context.Update(hRJobPositions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HRJobPositionsExists(hRJobPositions.JobId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(JobPositions));
            }
            ViewData["DepartmentId"] = new SelectList(_context.HRDepartment, "DepartmentId", "DepartmentName", hRJobPositions.DepartmentId);
            return View(hRJobPositions);
        }

        // GET: JobPositions/Delete/5
        public async Task<IActionResult> DeleteJobPosition(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Job Position";
            if (id == null || _context.HRJobPositions == null)
            {
                return NotFound();
            }

            var hRJobPositions = await _context.HRJobPositions
                .Include(h => h.HRDepartment)
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (hRJobPositions == null)
            {
                return NotFound();
            }

            return View(hRJobPositions);
        }

        // POST: JobPositions/Delete/5
        [HttpPost, ActionName("DeleteJobPosition")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteJobPositionConfirmed(int id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Job Position";
            if (_context.HRJobPositions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HRJobPositions'  is null.");
            }
            var hRJobPositions = await _context.HRJobPositions.FindAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            hRJobPositions.DeleteId = uid;
            hRJobPositions.DeletedDate = DateTime.Now;
            hRJobPositions.Active = false;
            if (hRJobPositions != null)
            {
                _context.HRJobPositions.Update(hRJobPositions);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(JobPositions));
        }

        private bool HRJobPositionsExists(int id)
        {
            return _context.HRJobPositions.Any(e => e.JobId == id);
        }

        // ContractTypes

        public async Task<IActionResult> ContractTypes()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Contract Types";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);
            return View(await _context.HRContractType.Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync());
        }

        // GET: ContractTypes/Create
        public IActionResult CreateContractType()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Contract Type";
            return View();
        }

        // POST: ContractTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateContractType([Bind("HRContractTypeId,HRContractTypeName")] HRContractType hRContractType)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Contract Type";
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRContractType.CreateId = uid;
                hRContractType.CompanyId = user.CompanyId;
                hRContractType.CreatedDate = DateTime.Now;

                hRContractType.Active = true;
                _context.Add(hRContractType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ContractTypes));
            }
            return View(hRContractType);
        }

        // GET: ContractTypes/Edit/5
        public async Task<IActionResult> EditContractType(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Contract Type";
            if (id == null || _context.HRContractType == null)
            {
                return NotFound();
            }

            var hRContractType = await _context.HRContractType.FindAsync(id);
            if (hRContractType == null)
            {
                return NotFound();
            }
            return View(hRContractType);
        }

        // POST: ContractTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContractType(int id, [Bind("HRContractTypeId,HRContractTypeName")] HRContractType hRContractType)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Contract Type";
            if (id != hRContractType.HRContractTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRContractType.UpdateId = uid;
                hRContractType.CompanyId = user.CompanyId;
                hRContractType.UpdatedDate = DateTime.Now;
                hRContractType.Active = true;
                try
                {
                    _context.Update(hRContractType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HRContractTypeExists(hRContractType.HRContractTypeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ContractTypes));
            }
            return View(hRContractType);
        }

        // GET: ContractTypes/Delete/5
        public async Task<IActionResult> DeleteContractType(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Contract Type";
            if (id == null || _context.HRContractType == null)
            {
                return NotFound();
            }

            var hRContractType = await _context.HRContractType
                .FirstOrDefaultAsync(m => m.HRContractTypeId == id);
            if (hRContractType == null)
            {
                return NotFound();
            }

            return View(hRContractType);
        }

        // POST: ContractTypes/Delete/5
        [HttpPost, ActionName("DeleteContractType")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteContractTypeConfirmed(int id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            
            ViewData["Action"] = "Delete Contract Type";
            if (_context.HRContractType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HRContractType'  is null.");
            }
            var hRContractType = await _context.HRContractType.FindAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            hRContractType.DeleteId = uid;
            hRContractType.DeletedDate = DateTime.Now;
            hRContractType.Active = false;
            if (hRContractType != null)
            {
                _context.HRContractType.Update(hRContractType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ContractTypes));
        }

        private bool HRContractTypeExists(int id)
        {
            return _context.HRContractType.Any(e => e.HRContractTypeId == id);
        }

        //   Vacation Rules

        public async Task<IActionResult> VacationRules()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Leave Rules";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);
            var vacationrule = await _context.HRVacationRule
             .Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync();
            return View(vacationrule);
        }

        public async Task<IActionResult> VacationRule(int id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Leave Rule Details";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);
            VacationRulesViewModel model = new VacationRulesViewModel();

            model.HRVacationRule = await _context.HRVacationRule
                        .Where(b => b.Active == true)
                    .FirstOrDefaultAsync(c => c.VacationRuleId == id);

            model.HRVacationRuleLine = await _context.HRVacationRuleLine
                   .Where(c => c.VacationRuleId == id && c.Active == true)
                    .Include(c => c.HRVacationType)
                    .Include(c => c.HRVacationRule).ToListAsync();
            ViewData["VacationRuleId"] = id;
            ViewData["VacationTypeId"] = new SelectList(_context.HRVacationType.Where(v => v.CompanyId == user.CompanyId), "VacationTypeId", "VacationTypeName");
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(model));
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> CreateVacationRule()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Leave Rule";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVacationRule([Bind("VacationRuleId,VacationRuleName,VacationDesc")] CreateVacationRulesViewModel VacationRules)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Leave Rule";
            if (ModelState.IsValid)
            {
                HRVacationRule vacationRules = new HRVacationRule();
                vacationRules.VacationRuleId = VacationRules.VacationRuleId;
                vacationRules.VacationRuleName = VacationRules.VacationRuleName;
                vacationRules.VacationDesc = VacationRules.VacationDesc;
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                vacationRules.CreateId = uid;
                vacationRules.CompanyId = user.CompanyId;
                vacationRules.CreatedDate = DateTime.Now;

                vacationRules.Active = true;

                _context.Add(vacationRules);
                await _context.SaveChangesAsync();
                return RedirectToRoute(new { controller = "Configuration", action = "VacationRule", id = vacationRules.VacationRuleId });
            }
            return View();


        }
        public async Task<IActionResult> EditVacationRule(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Leave Rule";
            if (id == null || _context.HRVacationRule == null)
            {
                return NotFound();
            }
            var hRVacationRule = await _context.HRVacationRule.FindAsync(id);
            if (hRVacationRule == null)
            {
                return NotFound();
            }
            return View(hRVacationRule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVacationRule(int id, [Bind("VacationRuleId,VacationRuleName,VacationDesc")] HRVacationRule hRVacationRule)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Leave Rule";
            if (id != hRVacationRule.VacationRuleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRVacationRule.UpdateId = uid;
                hRVacationRule.CompanyId = user.CompanyId;
                hRVacationRule.UpdatedDate = DateTime.Now;
                hRVacationRule.Active = true;
                _context.Add(hRVacationRule);
                try
                {
                    _context.Update(hRVacationRule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(VacationRules));
            }
            return View(hRVacationRule);
        }
        public async Task<IActionResult> DeleteVacationRule(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Leave Rule";
            if (id == null || _context.HRVacationRule == null)
            {
                return NotFound();
            }

            var hRVacationRule = await _context.HRVacationRule
                .FirstOrDefaultAsync(m => m.VacationRuleId == id);
            if (hRVacationRule == null)
            {
                return NotFound();
            }

            return View(hRVacationRule);
        }
        [HttpPost, ActionName("DeleteVacationRule")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedVacationRule(int id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Leave Rule";
            if (_context.HRVacationRule == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HRVacationRule'  is null.");
            }
            var hRVacationRule = await _context.HRVacationRule.FindAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            hRVacationRule.DeleteId = uid;
            hRVacationRule.DeletedDate = DateTime.Now;
            hRVacationRule.Active = false;
            if (hRVacationRule != null)
            {
                _context.HRVacationRule.Update(hRVacationRule);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(VacationRules));
        }


        //Rule Line
        [HttpPost]
        public async Task<IActionResult> AddVacationRuleLine(int id, [Bind("VacationTypeId,VacationDays")] VacationRulesLineViewModel vacationrulesLine)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Leave Rule Details";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);
            HRVacationRuleLine myLine = new HRVacationRuleLine
            {
                VacationRuleId = id,
                Active = true,
                CompanyId = user.CompanyId,
                VacationTypeId = vacationrulesLine.VacationTypeId,
                VacationStock = vacationrulesLine.VacationDays,

            };
            _context.Add(myLine);
            await _context.SaveChangesAsync();
            return RedirectToRoute(new { controller = "Configuration", action = "VacationRule", id = myLine.VacationRuleId });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteVacationRuleLine(int id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Leave Rule Details";
            var Var = _context.HRVacationRuleLine.Find(id);
            int VarId = Var.VacationRuleId;
            _context.HRVacationRuleLine.Remove(Var);
            await _context.SaveChangesAsync();
            return RedirectToRoute(new { controller = "Configuration", action = "VacationRule", id = VarId });
        }


        //   Vacation Type

        public async Task<IActionResult> VacationTypes()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Leave Types";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);
            var applicationDbContext = await _context.HRVacationType
              .Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync();
            return View(applicationDbContext);
        }


        public IActionResult CreateVacationType()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Leave Type";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVacationType([Bind("VacationTypeId,VacationTypeName")] HRVacationType hRVacationType)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Leave Type";
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRVacationType.CreateId = uid;
                hRVacationType.CompanyId = user.CompanyId;
                hRVacationType.CreatedDate = DateTime.Now;

                hRVacationType.Active = true;
                _context.Add(hRVacationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(VacationTypes));
            }
            return View(hRVacationType);
        }
        public async Task<IActionResult> EditVacationType(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Leave Type";
            if (id == null || _context.HRVacationType == null)
            {
                return NotFound();
            }
            var hRVacationType = await _context.HRVacationType.FindAsync(id);
            if (hRVacationType == null)
            {
                return NotFound();
            }
            return View(hRVacationType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVacationType(int id, [Bind("VacationTypeId,VacationTypeName")] HRVacationType hRVacationType)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Leave Type";
            if (id != hRVacationType.VacationTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRVacationType.UpdateId = uid;
                hRVacationType.CompanyId = user.CompanyId;
                hRVacationType.UpdatedDate = DateTime.Now;
                hRVacationType.Active = true;
                try
                {
                    _context.Update(hRVacationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;

                }
                return RedirectToAction(nameof(VacationTypes));
            }
            return View(hRVacationType);
        }
        public async Task<IActionResult> DeleteVacationType(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Leave Type";
            if (id == null || _context.HRVacationType == null)
            {
                return NotFound();
            }
            var hRVacationType = await _context.HRVacationType
                .FirstOrDefaultAsync(m => m.VacationTypeId == id);
            if (hRVacationType == null)
            {
                return NotFound();
            }
            return View(hRVacationType);
        }
        [HttpPost, ActionName("DeleteVacationType")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedVacationType(int id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Leave Type";
            if (_context.HRVacationType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HRVacationType'  is null.");
            }
            var hRVacationType = await _context.HRVacationType.FindAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            hRVacationType.DeleteId = uid;
            hRVacationType.DeletedDate = DateTime.Now;
            hRVacationType.Active = false;
            if (hRVacationType != null)
            {
                _context.HRVacationType.Update(hRVacationType);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(VacationTypes));
        }


        // Degree

        // GET: Degrees
        public async Task<IActionResult> Degrees()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Degrees";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);
            return View(await _context.HRDegree.Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync());
        }


        // GET: Degrees/Create
        public IActionResult CreateDegree()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Degree";
            return View();
        }

        // POST: Degrees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDegree([Bind("HRDegreeId,DegreeName")] HRDegree hRDegree)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Degree";
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRDegree.CreateId = uid;
                hRDegree.CompanyId = user.CompanyId;
                hRDegree.CreatedDate = DateTime.Now;

                hRDegree.Active = true;
                _context.Add(hRDegree);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Degrees));
            }
            return View(hRDegree);
        }

        // GET: Degrees/Edit/5
        public async Task<IActionResult> EditDegree(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Degree";
            if (id == null || _context.HRDegree == null)
            {
                return NotFound();
            }

            var hRDegree = await _context.HRDegree.FindAsync(id);
            if (hRDegree == null)
            {
                return NotFound();
            }
            return View(hRDegree);
        }

        // POST: Degrees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDegree(int id, [Bind("HRDegreeId,DegreeName")] HRDegree hRDegree)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Degree";
            if (id != hRDegree.HRDegreeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRDegree.UpdateId = uid;
                hRDegree.CompanyId = user.CompanyId;
                hRDegree.UpdatedDate = DateTime.Now;
                hRDegree.Active = true;
                try
                {
                    _context.Update(hRDegree);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;

                }
                return RedirectToAction(nameof(Degrees));
            }
            return View(hRDegree);
        }

        // GET: Degrees/Delete/5
        public async Task<IActionResult> DeleteDegree(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Degree";
            if (id == null || _context.HRDegree == null)
            {
                return NotFound();
            }

            var hRDegree = await _context.HRDegree
                .FirstOrDefaultAsync(m => m.HRDegreeId == id);
            if (hRDegree == null)
            {
                return NotFound();
            }

            return View(hRDegree);
        }

        // POST: Degrees/Delete/5
        [HttpPost, ActionName("DeleteDegree")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDegreeConfirmed(int id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Degree";
            if (_context.HRDegree == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HRDegree'  is null.");
            }
            var hRDegree = await _context.HRDegree.FindAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            hRDegree.DeleteId = uid;
            hRDegree.DeletedDate = DateTime.Now;
            hRDegree.Active = false;
            if (hRDegree != null)
            {
                _context.HRDegree.Update(hRDegree);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Degrees));
        }


        // MissionTypes

        public async Task<IActionResult> MissionTypes()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Errand Types";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);
            return View(await _context.HRMissionType.Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync());
        }

        // GET: MissionTypes/Create
        public IActionResult CreateMissionType()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Errand Type";
            return View();
        }

        // POST: MissionTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMissionType([Bind("HRMissionTypeId,HRMissionTypeName")] HRMissionType hRMissionType)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Errand Type";
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRMissionType.CreateId = uid;
                hRMissionType.CompanyId = user.CompanyId;
                hRMissionType.CreatedDate = DateTime.Now;

                hRMissionType.Active = true;
                _context.Add(hRMissionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MissionTypes));
            }
            return View(hRMissionType);
        }

        // GET: MissionTypes/Edit/5
        public async Task<IActionResult> EditMissionType(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Errand Type";
            if (id == null || _context.HRMissionType == null)
            {
                return NotFound();
            }

            var hRMissionType = await _context.HRMissionType.FindAsync(id);
            if (hRMissionType == null)
            {
                return NotFound();
            }
            return View(hRMissionType);
        }

        // POST: MissionTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMissionType(int id, [Bind("HRMissionTypeId,HRMissionTypeName")] HRMissionType hRMissionType)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Errand Type";
            if (id != hRMissionType.HRMissionTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRMissionType.UpdateId = uid;
                hRMissionType.CompanyId = user.CompanyId;
                hRMissionType.UpdatedDate = DateTime.Now;
                hRMissionType.Active = true;
                try
                {
                    _context.Update(hRMissionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HRMissionTypeExists(hRMissionType.HRMissionTypeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MissionTypes));
            }
            return View(hRMissionType);
        }

        // GET: MissionTypes/Delete/5
        public async Task<IActionResult> DeleteMissionType(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Errand Type";
            if (id == null || _context.HRMissionType == null)
            {
                return NotFound();
            }

            var hRMissionType = await _context.HRMissionType
                .FirstOrDefaultAsync(m => m.HRMissionTypeId == id);
            if (hRMissionType == null)
            {
                return NotFound();
            }

            return View(hRMissionType);
        }

        // POST: MissionTypes/Delete/5
        [HttpPost, ActionName("DeleteMissionType")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMissionTypeConfirmed(int id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Errand Type";
            if (_context.HRMissionType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HRMissionType'  is null.");
            }
            var hRMissionType = await _context.HRMissionType.FindAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            hRMissionType.DeleteId = uid;
            hRMissionType.DeletedDate = DateTime.Now;
            hRMissionType.Active = false;
            if (hRMissionType != null)
            {
                _context.HRMissionType.Update(hRMissionType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MissionTypes));
        }

        private bool HRMissionTypeExists(int id)
        {
            return _context.HRMissionType.Any(e => e.HRMissionTypeId == id);
        }

        // ShiftAttends

        public async Task<IActionResult> ShiftAttends()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Shifts";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);
            return View(await _context.HRShiftAttend.Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync());
        }

        // GET: ShiftAttends/Create
        public IActionResult CreateShiftAttend()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Shift";
            return View();
        }

        // POST: ShiftAttends/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateShiftAttend([Bind("HRShiftAttendId,ShiftName,ShiftStart,ShiftEnd,ShiftCount")] HRShiftAttend hRShiftAttend)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Shift";

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRShiftAttend.CreateId = uid;
                hRShiftAttend.CompanyId = user.CompanyId;
                hRShiftAttend.CreatedDate = DateTime.Now;

                hRShiftAttend.Active = true;

                //hRShiftAttend.ShiftCount = hRShiftAttend.From.CompareTo(hRShiftAttend.Start);

                _context.Add(hRShiftAttend);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ShiftAttends));
            }

            return View(hRShiftAttend);
        }

        // GET: ShiftAttends/Edit/5
        public async Task<IActionResult> EditShiftAttend(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Shift";
            if (id == null || _context.HRShiftAttend == null)
            {
                return NotFound();
            }

            var hRShiftAttend = await _context.HRShiftAttend.FindAsync(id);
            if (hRShiftAttend == null)
            {
                return NotFound();
            }
            return View(hRShiftAttend);
        }

        // POST: ShiftAttends/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditShiftAttend(int id, [Bind("HRShiftAttendId,ShiftName,ShiftStart,ShiftEnd,ShiftCount")] HRShiftAttend hRShiftAttend)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Shift";
            if (id != hRShiftAttend.HRShiftAttendId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRShiftAttend.UpdateId = uid;
                hRShiftAttend.CompanyId = user.CompanyId;
                hRShiftAttend.UpdatedDate = DateTime.Now;
                hRShiftAttend.Active = true;
                try
                {
                    _context.Update(hRShiftAttend);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HRShiftAttendExists(hRShiftAttend.HRShiftAttendId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ShiftAttends));
            }
            return View(hRShiftAttend);
        }

        // GET: ShiftAttends/Delete/5
        public async Task<IActionResult> DeleteShiftAttend(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Shift";
            if (id == null || _context.HRShiftAttend == null)
            {
                return NotFound();
            }

            var hRShiftAttend = await _context.HRShiftAttend
                .FirstOrDefaultAsync(m => m.HRShiftAttendId == id);
            if (hRShiftAttend == null)
            {
                return NotFound();
            }

            return View(hRShiftAttend);
        }

        // POST: ShiftAttends/Delete/5
        [HttpPost, ActionName("DeleteShiftAttend")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteShiftAttendConfirmed(int id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Shift";
            if (_context.HRShiftAttend == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HRShiftAttend'  is null.");
            }
            var hRShiftAttend = await _context.HRShiftAttend.FindAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            hRShiftAttend.DeleteId = uid;
            hRShiftAttend.DeletedDate = DateTime.Now;
            hRShiftAttend.Active = false;
            if (hRShiftAttend != null)
            {
                _context.HRShiftAttend.Update(hRShiftAttend);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ShiftAttends));
        }

        private bool HRShiftAttendExists(int id)
        {
            return _context.HRShiftAttend.Any(e => e.HRShiftAttendId == id);
        }

        //GET: AttendRoles
        public async Task<IActionResult> AttendRules()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Attend Rules";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);
            return View(await _context.HRAttendRole.Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync());
        }

        // GET: AttendRoles/Create
        public IActionResult CreateAttendRule()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Attend Rule";
            return View();
        }

        // POST: AttendRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAttendRule([Bind("NameAttendRole,LateMin,PenaltyMin")] HRAttendRole hRAttendRole)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Attend Rule";
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRAttendRole.CreateId = uid;
                hRAttendRole.CompanyId = user.CompanyId;
                hRAttendRole.CreatedDate = DateTime.Now;

                hRAttendRole.Active = true;
                _context.Add(hRAttendRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AttendRules));
            }
            return View(hRAttendRole);
        }

        // GET: AttendRoles/Edit/5
        public async Task<IActionResult> EditAttendRule(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Attend Rule";
            if (id == null || _context.HRAttendRole == null)
            {
                return NotFound();
            }

            var hRAttendRole = await _context.HRAttendRole.FindAsync(id);
            if (hRAttendRole == null)
            {
                return NotFound();
            }
            return View(hRAttendRole);
        }

        // POST: AttendRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAttendRule(int id, [Bind("AttendRoleId,NameAttendRole,LateMin,PenaltyMin")] HRAttendRole hRAttendRole)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Attend Rule";
            if (id != hRAttendRole.AttendRoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRAttendRole.UpdateId = uid;
                hRAttendRole.CompanyId = user.CompanyId;
                hRAttendRole.UpdatedDate = DateTime.Now;
                hRAttendRole.Active = true;
                try
                {
                    _context.Update(hRAttendRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HRAttendRoleExists(hRAttendRole.AttendRoleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AttendRules));
            }
            return View(hRAttendRole);
        }

        // GET: AttendRoles/Delete/5
        public async Task<IActionResult> DeleteAttendRule(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Attend Rule";
            if (id == null || _context.HRAttendRole == null)
            {
                return NotFound();
            }

            var hRAttendRole = await _context.HRAttendRole
                .FirstOrDefaultAsync(m => m.AttendRoleId == id);
            if (hRAttendRole == null)
            {
                return NotFound();
            }

            return View(hRAttendRole);
        }

        // POST: AttendRoles/Delete/5
        [HttpPost, ActionName("DeleteAttendRule")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAttendRuleConfirmed(int id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Attend Rule";
            if (_context.HRAttendRole == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HRAttendRole'  is null.");
            }
            var hRAttendRole = await _context.HRAttendRole.FindAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            hRAttendRole.DeleteId = uid;
            hRAttendRole.DeletedDate = DateTime.Now;
            hRAttendRole.Active = false;
            if (hRAttendRole != null)
            {
                _context.HRAttendRole.Update(hRAttendRole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AttendRules));
        }

        private bool HRAttendRoleExists(int id)
        {
            return _context.HRAttendRole.Any(e => e.AttendRoleId == id);
        }


        // GET: BenefitsTypes
        public async Task<IActionResult> BenefitsTypes()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Benefits Types";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);
            return View(await _context.HRBenefitsType.Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync());
        }


        // GET: BenefitsTypes/Create
        public IActionResult CreateBenefitsType()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Benefits Type";
            return View();
        }

        // POST: BenefitsTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBenefitsType([Bind("HRBenefitsTypeId,HRBenefitsTypeName")] HRBenefitsType hRBenefitsType)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Benefits Type";
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRBenefitsType.CreateId = uid;
                hRBenefitsType.CompanyId = user.CompanyId;
                hRBenefitsType.CreatedDate = DateTime.Now;

                hRBenefitsType.Active = true;
                _context.Add(hRBenefitsType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(BenefitsTypes));
            }
            return View(hRBenefitsType);
        }

        // GET: BenefitsTypes/Edit/5
        public async Task<IActionResult> EditBenefitsType(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Benefits Type";
            if (id == null || _context.HRBenefitsType == null)
            {
                return NotFound();
            }

            var hRBenefitsType = await _context.HRBenefitsType.FindAsync(id);
            if (hRBenefitsType == null)
            {
                return NotFound();
            }
            return View(hRBenefitsType);
        }

        // POST: BenefitsTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBenefitsType(int id, [Bind("HRBenefitsTypeId,HRBenefitsTypeName")] HRBenefitsType hRBenefitsType)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Benefits Type";
            if (id != hRBenefitsType.HRBenefitsTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRBenefitsType.UpdateId = uid;
                hRBenefitsType.CompanyId = user.CompanyId;
                hRBenefitsType.UpdatedDate = DateTime.Now;
                hRBenefitsType.Active = true;
                try
                {
                    _context.Update(hRBenefitsType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HRBenefitsTypeExists(hRBenefitsType.HRBenefitsTypeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(BenefitsTypes));
            }
            return View(hRBenefitsType);
        }

        // GET: BenefitsTypes/Delete/5
        public async Task<IActionResult> DeleteBenefitsType(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Benefits Type";
            if (id == null || _context.HRBenefitsType == null)
            {
                return NotFound();
            }

            var hRBenefitsType = await _context.HRBenefitsType
                .FirstOrDefaultAsync(m => m.HRBenefitsTypeId == id);
            if (hRBenefitsType == null)
            {
                return NotFound();
            }

            return View(hRBenefitsType);
        }

        // POST: BenefitsTypes/Delete/5
        [HttpPost, ActionName("DeleteBenefitsType")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBenefitsTypeConfirmed(int id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Benefits Type";
            if (_context.HRBenefitsType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HRBenefitsType'  is null.");
            }
            var hRBenefitsType = await _context.HRBenefitsType.FindAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            hRBenefitsType.DeleteId = uid;
            hRBenefitsType.DeletedDate = DateTime.Now;
            hRBenefitsType.Active = false;
            if (hRBenefitsType != null)
            {
                _context.HRBenefitsType.Update(hRBenefitsType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(BenefitsTypes));
        }

        private bool HRBenefitsTypeExists(int id)
        {
            return _context.HRBenefitsType.Any(e => e.HRBenefitsTypeId == id);
        }

        // PenaltyTypes

        // GET: PenaltyTypes
        public async Task<IActionResult> PenaltyTypes()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Penalty Types";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);
            return View(await _context.HRPenaltyTypes.Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync());
        }



        // GET: PenaltyTypes/Create
        public IActionResult CreatePenaltyType()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Penalty Type";
            return View();
        }

        // POST: PenaltyTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePenaltyType([Bind("HRPenaltyTypesId,HRPenaltyTypeName")] HRPenaltyTypes hRPenaltyTypes)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Penalty Type";
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRPenaltyTypes.CreateId = uid;
                hRPenaltyTypes.CompanyId = user.CompanyId;
                hRPenaltyTypes.CreatedDate = DateTime.Now;
                hRPenaltyTypes.Active = true;
                _context.Add(hRPenaltyTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(PenaltyTypes));
            }
            return View(hRPenaltyTypes);
        }

        // GET: PenaltyTypes/Edit/5
        public async Task<IActionResult> EditPenaltyType(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Penalty Type";
            if (id == null || _context.HRPenaltyTypes == null)
            {
                return NotFound();
            }

            var hRPenaltyTypes = await _context.HRPenaltyTypes.FindAsync(id);
            if (hRPenaltyTypes == null)
            {
                return NotFound();
            }
            return View(hRPenaltyTypes);
        }

        // POST: PenaltyTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPenaltyType(int id, [Bind("HRPenaltyTypesId,HRPenaltyTypeName")] HRPenaltyTypes hRPenaltyTypes)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Penalty Type";
            if (id != hRPenaltyTypes.HRPenaltyTypesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _context.Users.FindAsync(uid);
                hRPenaltyTypes.UpdateId = uid;
                hRPenaltyTypes.CompanyId = user.CompanyId;
                hRPenaltyTypes.UpdatedDate = DateTime.Now;
                hRPenaltyTypes.Active = true;
                try
                {
                    _context.Update(hRPenaltyTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HRPenaltyTypesExists(hRPenaltyTypes.HRPenaltyTypesId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(PenaltyTypes));
            }
            return View(hRPenaltyTypes);
        }

        // GET: PenaltyTypes/Delete/5
        public async Task<IActionResult> DeletePenaltyType(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Penalty Type";
            if (id == null || _context.HRPenaltyTypes == null)
            {
                return NotFound();
            }

            var hRPenaltyTypes = await _context.HRPenaltyTypes
                .FirstOrDefaultAsync(m => m.HRPenaltyTypesId == id);
            if (hRPenaltyTypes == null)
            {
                return NotFound();
            }

            return View(hRPenaltyTypes);
        }

        // POST: PenaltyTypes/Delete/5
        [HttpPost, ActionName("DeletePenaltyType")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePenaltyTypeConfirmed(int id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Penalty Type";
            if (_context.HRPenaltyTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HRPenaltyTypes'  is null.");
            }
            var hRPenaltyTypes = await _context.HRPenaltyTypes.FindAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            hRPenaltyTypes.DeleteId = uid;
            hRPenaltyTypes.DeletedDate = DateTime.Now;
            hRPenaltyTypes.Active = false;
            if (hRPenaltyTypes != null)
            {
                _context.HRPenaltyTypes.Update(hRPenaltyTypes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PenaltyTypes));
        }

        private bool HRPenaltyTypesExists(int id)
        {
            return _context.HRPenaltyTypes.Any(e => e.HRPenaltyTypesId == id);
        }


        // AllowanceType

        // GET: HRAllowanceType
        public async Task<IActionResult> AllowanceType()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Allowance Types";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _context.Users.FindAsync(uid);
            return View(await _context.HRAllowanceType.Where(b => b.Active == true && b.CompanyId == user.CompanyId).ToListAsync());
        }


        // GET: HRAllowanceType/Create
        public IActionResult CreateAllowanceType()
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Allowance Type";
            return View();
        }

        // POST: HRAllowanceType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAllowanceType([Bind("HRAllowanceTypeId,HRAllowanceTypeName")] HRAllowanceType hRAllowanceType)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Add Allowance Type";
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                hRAllowanceType.CreateId = uid;

                hRAllowanceType.CreatedDate = DateTime.Now;

                hRAllowanceType.Active = true;
                _context.Add(hRAllowanceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hRAllowanceType);
        }

        // GET: HRAllowanceType/Edit/5
        public async Task<IActionResult> EditAllowanceType(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Allowance Type";

            if (id == null || _context.HRAllowanceType == null)
            {
                return NotFound();
            }

            var hRAllowanceType = await _context.HRAllowanceType.FindAsync(id);
            if (hRAllowanceType == null)
            {
                return NotFound();
            }
            return View(hRAllowanceType);
        }

        // POST: HRAllowanceType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAllowanceType(int id, [Bind("hRAllowanceTypeId,HRAllowanceTypeName")] HRAllowanceType hRAllowanceType)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Edit Allowance Type";
            if (id != hRAllowanceType.HRAllowanceTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                hRAllowanceType.UpdateId = uid;
                hRAllowanceType.UpdatedDate = DateTime.Now;
                hRAllowanceType.Active = true;
                try
                {
                    _context.Update(hRAllowanceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!hRAllowanceTypeExists(hRAllowanceType.HRAllowanceTypeId))
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
            return View(hRAllowanceType);
        }

        // GET: HRAllowanceType/Delete/5
        public async Task<IActionResult> DeleteAllowanceType(int? id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Allowance Type";
            if (id == null || _context.HRAllowanceType == null)
            {
                return NotFound();
            }

            var hRAllowanceType = await _context.HRAllowanceType
                .FirstOrDefaultAsync(m => m.HRAllowanceTypeId == id);
            if (hRAllowanceType == null)
            {
                return NotFound();
            }

            return View(hRAllowanceType);
        }

        // POST: HRAllowanceType/Delete/5
        [HttpPost, ActionName("DeleteAllowanceType")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAllowanceType(int id)
        {
            ViewData["ControllerName"] = ControllerName;
            ViewData["AppName"] = AppName;
            ViewData["Action"] = "Delete Allowance Type";
            if (_context.HRAllowanceType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.hRAllowanceType'  is null.");
            }
            var hRAllowanceType = await _context.HRAllowanceType.FindAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            hRAllowanceType.DeleteId = uid;
            hRAllowanceType.DeletedDate = DateTime.Now;
            hRAllowanceType.Active = false;
            if (hRAllowanceType != null)
            {
                _context.HRAllowanceType.Update(hRAllowanceType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool hRAllowanceTypeExists(int id)
        {
            return _context.HRAllowanceType.Any(e => e.HRAllowanceTypeId == id);
        }
    }
}
