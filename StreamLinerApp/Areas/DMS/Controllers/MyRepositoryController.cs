using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StreamLinerDataLayer.Data;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerLogicLayer.Helper;
using StreamLinerLogicLayer.Services.RepoDocument;
using StreamLinerLogicLayer.Services.RepoFolder;
using StreamLinerViewModelLayer.ModelDTO;
using System;
using System.IO;
using System.IO.Compression;
using System.Security.Claims;
using System.Threading.Tasks;
using static NuGet.Packaging.PackagingConstants;

namespace StreamLinerApp.Areas.DMS.Controllers
{
    [Area("DMS")]
    [Authorize(Roles = "Administrator,Admin,NamedUser")]
    public class MyRepositoryController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IFolderService _folderService;
        private readonly IDocumentService _documentService;
        private readonly ApplicationDbContext _context;

        public MyRepositoryController(IWebHostEnvironment env, IFolderService folderService,
            ApplicationDbContext dbContext,
            IDocumentService documentService)
        {
            _env = env;
            _folderService = folderService;
            _context = dbContext;
            _documentService = documentService;
        }
        private async Task<(int userId, int companyId)> GetUserInfoAsync()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _context.Users.FindAsync(userId);
            return (userId, user.CompanyId);
        }

        public async Task<IActionResult> DirectToFolder(int id)
        {
            return RedirectToRoute(new { action = "Folder", id = id });
        }
        public async Task<IActionResult> Index()
        {
            var (userId, companyId) = await GetUserInfoAsync();
            // get my user id
            //var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            //if (string.IsNullOrEmpty(userId))
            //{
            //    // Handle the case where user ID is not found
            //    return RedirectToAction("Index", "Home");
            //}
            getfolderDTO getfolderDTO = await _folderService.GetMyRepositorieFolder();
            if (getfolderDTO == null)
            {
                var creationResponse = await _folderService.CreateRepositorieFolder();
            }
            getfolderDTO = await _folderService.GetMyRepositorieFolder();
            //  var folders = await _folderService.GetMyRepositoryRootFoldersAsync();

            DirectoryViewModel directory = new DirectoryViewModel();


            directory.SubDirectories = await _folderService.GetMyRepositoryRootFoldersAsync();
            // Get my folders
            ViewData["application"] = "DMS";
            ViewData["FolderId"] = getfolderDTO.Id;
            ViewData["FolderName"] = getfolderDTO.FolderName;
            ViewData["FolderPath"] = getfolderDTO.FolderPath;
            return View(directory);
        }
        public async Task<IActionResult> Folder(int id)
        {
            var (userId, companyId) = await GetUserInfoAsync();
            ViewData["UserId"] = new SelectList(_context.Users.Where(m => m.CompanyId == companyId), "Id", "FullName");
            ViewData["FolderPermissionsId"] = new SelectList(_context.PermissionType, "Id", "PermissionName");
            ViewData["PermissionsScopeId"] = new SelectList(_context.PermissionScope, "Id", "Name");
            // Check if id is empty
            if (id == 0 || id == null)
            {
                // Handle the case where no folder ID is provided
                return RedirectToAction("Index");
            }


            DirectoryViewModel directory = new DirectoryViewModel();
            var folder = await _folderService.GetFolderById(id);
            if (folder == null)
            {
                // Handle the case where the folder is not found
                return NotFound($"Folder with ID {id} not found.");
            }

            directory.FolderId = 1;
            ViewData["FolderId"] = id;
            ViewData["FolderName"] = folder.FolderName;
            ViewData["FolderPath"] = folder.FolderPath;
            ViewData["ParentId"] = folder.ParentId;

            directory.SubDirectories = await _folderService.GetSubFoldersAsync(id);
            directory.FolderUserPermissionViewModel = await _folderService.GetFolderPermissions(id);

            directory.SubFiles = await _documentService.GetAllSunDocuments(id);

            return View(directory);
        }


        [HttpPost]
        public async Task<IActionResult> CreateFolder([Bind("FolderId,Name,ParentId,FolderPath")] folderDTO model)
        {
            var (userId, companyId) = await GetUserInfoAsync();

            if (model.FolderPath == null)
            {
                model.FolderPath = "\\RepositoriesFile";
            }

            // Full path to wwwroot
            string wwwRootPath = Path.Combine(_env.WebRootPath);
            string FolderPath = Path.Combine(model.FolderPath, model.Name);
            string fullPath = wwwRootPath + FolderPath;


            // Create if it doesn't exist
            if (!System.IO.Directory.Exists(fullPath))
            {
                var result = System.IO.Directory.CreateDirectory(fullPath);
                // Optionally, you can create a folder entity in the database here
                var folderDto = new folderDTO
                {
                    Name = model.Name,
                    ParentId = model.ParentId,// Assuming this is a root folder, adjust as necessary
                    FolderPath = FolderPath,
                    OwnerId = userId
                };
                var creationResponse = await _folderService.CreateFolder(folderDto);




                var folder = await _folderService.GetFolderById(model.ParentId);
                
                
                
                if (!creationResponse.IsSuccess)
                {
                    return BadRequest($"Error creating folder: {creationResponse.Message}");
                }

                return RedirectToRoute(new { action = "Folder", id = model.ParentId });

            }


            return RedirectToAction("Index");
        }
        //   RenameFolder
        [HttpPost]
        public async Task<IActionResult> RenameFolder([Bind("NewFolderId,NewFolderName")] RenameFolderDTO model)
        {
            var (userId, companyId) = await GetUserInfoAsync();
            var folder = await _folderService.GetFolderById(model.NewFolderId);
            var parent = await _folderService.GetFolderById(folder.ParentId);
            // Directory.Move("OldFolderName", "NewFolderName");
            // Full path to wwwroot
            string wwwRootPath = Path.Combine(_env.WebRootPath);
            string FolderPath = Path.Combine(folder.FolderPath);
            string oldPath = wwwRootPath + FolderPath;
            string ParentPath = Path.Combine(parent.FolderPath, model.NewFolderName);
            string newPath = wwwRootPath + ParentPath;


            // Create if it doesn't exist
            if (System.IO.Directory.Exists(oldPath))
            {
                System.IO.Directory.Move(oldPath, newPath);
            }





            if (folder == null)
            {
                // Handle the case where the folder is not found
                return NotFound($"Folder with ID {model.NewFolderId} not found.");
            }
            folder.FolderName = model.NewFolderName;
            var folderDto = new folderDTO
            {
                FolderId = model.NewFolderId,
                Name = folder.FolderName,
                ParentId = folder.ParentId,
                FolderPath = ParentPath,
                OwnerId = folder.OwnerId
            };
            var creationResponse = await _folderService.RenameFolder(folderDto);
            if (!creationResponse.IsSuccess)
            {
                return BadRequest($"Error renaming folder: {creationResponse.Message}");
            }

            return RedirectToRoute(new { action = "Folder", id = folder.ParentId });

        }

        [HttpPost]
        public async Task<IActionResult> DeleteFolder([Bind("deletefolderid")] deletefolderDto model)
        {
            var (userId, companyId) = await GetUserInfoAsync();
            var folder = await _folderService.GetFolderById(model.deletefolderid);
            // Folder inside wwwroot you want to zip
            string wwwRootPath = Path.Combine(_env.WebRootPath);
            string FolderPath = Path.Combine(folder.FolderPath);
            string fullPath = wwwRootPath + FolderPath;

            if (System.IO.Directory.Exists(fullPath))
            {
                System.IO.Directory.Delete(fullPath, true);
            }
            var creationResponse = await _folderService.DeleteFolder(model.deletefolderid);
            if (!creationResponse.IsSuccess)
            {
                return BadRequest($"Error deleting folder: {creationResponse.Message}");
            }
            return RedirectToRoute(new { action = "Folder", id = folder.ParentId });
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([Bind("title,ParentId,document")] CreatedocumentDto model)
        {


            // Validate the model
            if (model.document == null || model.document.Length == 0)
                return BadRequest("No file uploaded.");

            // Get file extension
            var extension = Path.GetExtension(model.document.FileName).ToLowerInvariant();


            // Check if allowed ( StreamLinerLogicLayer => Helper => AllowedFileExtensions Class)
            if (!AllowedFileExtensions.All.Contains(extension))
                return BadRequest("Invalid file type. Allowed: " + string.Join(", ", AllowedFileExtensions.All));

            // Path to save file in wwwroot/uploads
            var uploadsPath = Path.Combine(_env.WebRootPath, "RepositoriesFile");
            var filePath = Path.Combine(uploadsPath, model.document.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.document.CopyToAsync(stream);
            }



            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddFolderPermission(string folderid, string userid, string folderpermissionid)
        {
            var (userId, companyId) = await GetUserInfoAsync();
            int id = Convert.ToInt32(folderid);
            AddFolderPermissionDto folderPermission = new AddFolderPermissionDto()
            {
                CompanyId = companyId,
                CreatedBy = userId,
                FolderPermissionId = Convert.ToInt32(folderpermissionid),
                UserId = Convert.ToInt32(userid),
                FolderId = Convert.ToInt32(folderid)
            };

            List<FolderUserPermissionViewModel>? FolderUserPermissionViewModel = new List<FolderUserPermissionViewModel>();
            var creationResponse = await _folderService.AddFolderPermission(folderPermission);
            if (creationResponse.IsSuccess)
            {
                FolderUserPermissionViewModel = await _folderService.GetFolderPermissions(id);
            }

            return PartialView("_MyTablePartial", FolderUserPermissionViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteFolderPermission(int id)
        {

            var (userId, companyId) = GetUserInfoAsync().Result;
            // Assuming _data is your data source (e.g., a list or database context)

            var permission = await _context.FolderUserPermissions.FindAsync(id);
            int folderId = permission.FolderId;

            var creationResponse = await _folderService.DeleteFolderPermission(id);
            List<FolderUserPermissionViewModel>? FolderUserPermissionViewModel = new List<FolderUserPermissionViewModel>();
            if (creationResponse.IsSuccess)
            {
                FolderUserPermissionViewModel = await _folderService.GetFolderPermissions(folderId);
            }

            return PartialView("_MyTablePartial", FolderUserPermissionViewModel);
        }

        public async Task<IActionResult> DownloadFolder(int id)
        {
            var (userId, companyId) = await GetUserInfoAsync();
            var folder = await _folderService.GetFolderById(id);
            // Folder inside wwwroot you want to zip
            string wwwRootPath = Path.Combine(_env.WebRootPath);
            string FolderPath = Path.Combine(folder.FolderPath);
            string fullPath = wwwRootPath + FolderPath;
            //   var folderPath = Path.Combine(_env.WebRootPath, folder.FolderPath);

            if (!System.IO.Directory.Exists(fullPath))
                return NotFound("Folder not found.");

            // Name of the zip file
            string folderName = folder.FolderName + ".zip";
            // Temp zip file path
            var zipFilePath = Path.Combine(Path.GetTempPath(), folderName);

            if (System.IO.File.Exists(zipFilePath))
                System.IO.File.Delete(zipFilePath);

            // Create zip from folder
            ZipFile.CreateFromDirectory(fullPath, zipFilePath);

            // Return zip as file download
            var fileBytes = System.IO.File.ReadAllBytes(zipFilePath);
            return File(fileBytes, "application/zip", folderName);
        }
        public async Task<IActionResult> Document()
        {
            return View();
        }

        public IActionResult UserBulkUpload()
        {
            return View();
        }
        public IActionResult Directory()
        {
            return View();
        }
    }
}
