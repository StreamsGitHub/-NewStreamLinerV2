using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StreamLinerLogicLayer.Helper;
using StreamLinerLogicLayer.Services.RepoFolder;
using StreamLinerViewModelLayer.ModelDTO;


namespace StreamLinerApp.Areas.DMS.Controllers
{
    [Area("DMS")]
    [Authorize(Roles = "Administrator,Admin,NamedUser")]
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IFolderService _folderService;
      

        public HomeController(IWebHostEnvironment env, IFolderService folderService)
        {
            _env = env;
            _folderService = folderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFolder([Bind("Name,ParentId")] folderDTO model)
        {
            // Full path to wwwroot
            string wwwRootPath = Path.Combine(_env.WebRootPath, "RepositoriesFile");
            string fullPath = Path.Combine(wwwRootPath, model.Name);

            // Create if it doesn't exist
            if (!System.IO.Directory.Exists(fullPath))
            {
                System.IO.Directory.CreateDirectory(fullPath);
                // Optionally, you can create a folder entity in the database here
                var folderDto = new folderDTO
                {
                    Name = model.Name,
                    ParentId = model.ParentId,// Assuming this is a root folder, adjust as necessary

                };
                var creationResponse = await _folderService.CreateFolder(folderDto);
                if (!creationResponse.IsSuccess)
                {
                    return BadRequest($"Error creating folder: {creationResponse.Message}");
                }

                return RedirectToAction("Index");
            }


            return RedirectToAction("Index");
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Favorites()
        {
            
            return View();
        }
        public async Task<IActionResult> Folder(int id)
        {
            DirectoryViewModel directory = new DirectoryViewModel();

            directory.FolderId = id;
            directory.SubDirectories = await _folderService.GetSubFoldersAsync(id) ;

            return View(directory);
        }


        public IActionResult PDFfile()
        {
            return View();
        }

    }
}
