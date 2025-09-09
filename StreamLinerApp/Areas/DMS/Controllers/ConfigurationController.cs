using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StreamLinerDataLayer.Data;
using StreamLinerLogicLayer.Services.RepoDocument;
using StreamLinerLogicLayer.Services.RepoFolder;
using StreamLinerLogicLayer.Services.RepositoryServices;
using StreamLinerViewModelLayer.ModelDTO;
using System.Security.Claims;



namespace StreamLinerApp.Areas.DMS.Controllers
{
    [Area("DMS")]
    [Authorize(Roles = "Administrator,Admin")]
    public class ConfigurationController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IRepoService _repoService;
        private readonly IFolderService _folderService;
        private readonly ApplicationDbContext _context;


        public ConfigurationController(IWebHostEnvironment env, IRepoService repoService,
            IFolderService folderService, ApplicationDbContext context)
        {
            _env = env;
            _repoService = repoService;
            _folderService = folderService;
            _context = context;
        }

        private async Task<(int userId, int companyId)> GetUserInfoAsync()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _context.Users.FindAsync(userId);
            return (userId, user.CompanyId);
        }


        public IActionResult Index()
        {
            ViewData["Action"] = "";
            return View();
        }

        // Repositories
        public async Task<IActionResult> Repositories()
        {
            var (userId, companyId) = await GetUserInfoAsync();
            ViewData["UserId"] = new SelectList(_context.Users.Where(m => m.CompanyId == companyId), "Id", "FullName");
            ViewData["Action"] = " - Repositories";
            // Get all repositories        GetAllRepositories
            var repos = await _repoService.GetAllRepositories();
            ViewData["RepoCount"] = repos.Count();

            return View(repos);
        }

        // Create Repository
        [Route("DMS/Configuration/Repository/Create")]
        public async Task<IActionResult> CreateRepository()
        {
            ViewData["Action"] = " - Create Repository";
            var (userId, companyId) = await GetUserInfoAsync();
            ViewData["UserId"] = new SelectList(_context.Users.Where(m => m.CompanyId == companyId), "Id", "FullName");
            int repoCount = await _repoService.GetTotalRepositoryCount();
            ViewData["RepoCount"] = "Repository " + (repoCount + 1).ToString();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("DMS/Configuration/Repository/Create")]
        public async Task<IActionResult> CreateRepository(CreateRepositoryDto model)
        {
            var (userId, companyId) = await GetUserInfoAsync();




            if (ModelState.IsValid)
            {
                folderDTO folderDTO = new folderDTO();
                string FolderPath = "\\RepositoriesFile";
                string wwwRootPath = Path.Combine(_env.WebRootPath);
                string repoPath = Path.Combine(FolderPath, model.RepositoryName);
                string fullPath = wwwRootPath + repoPath;

                bool isExists = await _repoService.IsRepositoryExists(model.RepositoryName, companyId);

                if (Directory.Exists(fullPath) && isExists)
                {
                    ModelState.AddModelError("RepoName", "Repository name already exists.");
                    ViewData["UserId"] = new SelectList(_context.Users.Where(m => m.CompanyId == companyId), "Id", "FullName", model.OwnerId);
                    return View(model);
                }

                Directory.CreateDirectory(fullPath);
                string filePath = Path.Combine(fullPath, "readme.txt");

                if (!System.IO.File.Exists(filePath))
                {
                    string[] lines =
                                     {
                                          "############## Repository Information ####################",
                                          "Repository Name : " + model.RepositoryName,
                                          "Repository Description : " + model.Description ,
                                          "Is Private is : " + model.IsPrivate + " "+ model.License,
                                          "Size in MB : " + model.SizeInMB,
                                          model.RepositoryName + " Created on " + DateTime.Now.ToString("F"),
                                          "Created by User ID : " + userId,
                                            "#########################################################"
                                      };
                    string content = "- Repo Name : " + model.RepositoryName + " \\n" + " - Description : " + model.Description + " \\n";
                    System.IO.File.WriteAllLines(filePath, lines);
                }
                CreationResponse creationResponse = await _repoService.CreateRepository(model);
                return RedirectToAction(nameof(Repositories));
            }
            ViewData["UserId"] = new SelectList(_context.Users.Where(m => m.CompanyId == companyId), "Id", "FullName", model.OwnerId);
            return View();
        }

        // edit Repository
        [Route("DMS/Configuration/Repository/Edit/{id}")]
        public async Task<IActionResult> EditRepository(int id)
        {
            var (userId, companyId) = await GetUserInfoAsync();
            var repo = await _repoService.GetRepositoryById(id);
            if (repo == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users.Where(m => m.CompanyId == companyId), "Id", "FullName", repo.OwnerId);
            ViewData["Action"] = " - Edit Repository";
            return View(repo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRepository(UpdateRepositoryDto model)
        {
            var (userId, companyId) = await GetUserInfoAsync();
            if (ModelState.IsValid)
            {
                bool isExists = await _repoService.IsRepositoryExists(model.RepositoryName, companyId);
                if (isExists)
                {
                    ModelState.AddModelError("RepoName", "Repository name already exists.");
                    ViewData["UserId"] = new SelectList(_context.Users.Where(m => m.CompanyId == companyId), "Id", "FullName", model.OwnerId);
                    return View(model);
                }
                CreationResponse creationResponse = await _repoService.UpdateRepository(model);
                if (creationResponse.IsSuccess)
                {
                    return RedirectToAction(nameof(Repositories));
                }
                ModelState.AddModelError("", "Failed to update repository. Please try again.");
            }
            ViewData["UserId"] = new SelectList(_context.Users.Where(m => m.CompanyId == companyId), "Id", "FullName", model.OwnerId);
            return View(model);
        }




        public IActionResult AccessRights()
        {
            ViewData["Action"] = " - Access Rights";
            return View();
        }
        public IActionResult Quota()
        {
            ViewData["Action"] = " - Quota";
            return View();
        }
        public IActionResult Storge()
        {
            ViewData["Action"] = " - Storge";
            return View();
        }
        public IActionResult Spam()
        {
            ViewData["Action"] = " - Spam";
            return View();
        }
        public IActionResult Deleted()
        {
            ViewData["Action"] = " - File Deleted";
            return View();
        }
    }
}
