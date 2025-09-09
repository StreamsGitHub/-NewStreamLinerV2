using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StreamLinerLogicLayer.Services.RepoFolder;
using StreamLinerViewModelLayer.ModelDTO;
using System.IO;


namespace StreamLinerApp.Areas.CMS.Controllers
{
    [Area("CMS")]
    [Authorize(Roles = "Administrator,Admin,NamedUser")]
    public class FolderController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IFolderService _folderService;

        public FolderController(IWebHostEnvironment env, IFolderService folderService)
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
                    ParentId = model.ParentId ,// Assuming this is a root folder, adjust as necessary

                };
                var creationResponse = await _folderService.CreateFolder(folderDto);
                if (!creationResponse.IsSuccess)
                {
                    return BadRequest($"Error creating folder: {creationResponse.Message}");
                }

                return RedirectToAction("Index" );
            }


            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Inbound()
        {
            return View();
        }
        public IActionResult Outbound()
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
