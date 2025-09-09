using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StreamLinerLogicLayer.Services.MeetingRoomServices;
using StreamLinerLogicLayer.Services.TemplateServices;
using System.Threading.Tasks;

namespace StreamLinerApp.Areas.scp.Controllers
{
    [Area("scp")]
    public class DocTempController : Controller
    {
        string app = "Admin";
        string ctrl = " Document Template";
        private readonly IDocTemplateService _docTemplateService;
        public DocTempController(IDocTemplateService docTemplateService)
        {
            _docTemplateService = docTemplateService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["application"] = app;
            ViewData["action"] = "Document Template";
            ViewData["controller"] = ctrl;
            var templates = await _docTemplateService.GetAllTemplate();
            return View(templates);
        }
        public IActionResult CreateTemplate()
        {
            ViewData["application"] = app;
            ViewData["action"] = "Create Document Template";
            ViewData["controller"] = ctrl;
            return View();
        }
        [HttpPost]
        public IActionResult Create(string name, string description)
        {
            // Logic to create a document template
            // This could involve saving the template to a database or file system
            // For now, we will just redirect to the index view
            return RedirectToAction("Index");
        }
        public IActionResult Edit()
        {
            ViewData["application"] = app;
            ViewData["action"] = "Edit Document Template";
            ViewData["controller"] = ctrl;
            return View();
        }
        public IActionResult Details()
        {
            ViewData["application"] = app;
            ViewData["action"] = "Document Template Details";
            ViewData["controller"] = ctrl;
            return View();
        }
         

    }
}
