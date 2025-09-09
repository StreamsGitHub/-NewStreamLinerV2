using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerLogicLayer.Services.FieldServices;
using StreamLinerLogicLayer.Services.MetaDataServices;
using StreamLinerLogicLayer.Services.MetaDataTemplateServices;
using StreamLinerViewModelLayer.ModelDTO;
using System.Threading.Tasks;

namespace StreamLinerApp.Areas.scp.Controllers
{
    [Area("scp")]
    public class MetaDataController : Controller
    {
        private readonly IMetaDataService _metaData;
        private readonly IMetaDataTemplateService _metaDataTemplateService;
        private readonly IField _field;
        string app = "Admin";
        string ctrl = "MetaData";
        public MetaDataController(IMetaDataService metaData , IField field , IMetaDataTemplateService metaDataTemplateService)
        {
            _metaData = metaData;
            _metaDataTemplateService = metaDataTemplateService;
            _field = field;

        }

        public async Task<IActionResult> Index()
        {
            ViewData["application"] = app;
            ViewData["action"] = " ";
            ViewData["controller"] = ctrl;
            var templates = await _metaData.GetAllTemplatesAsync();
            return View(templates);
        }

        // create 
        [HttpPost]
        public async Task<IActionResult> CreateTemplate([Bind("Name,Description")] MetaDataDTO model)
        {
            ViewData["application"] = app;
            ViewData["action"] = " New Field";
            ViewData["controller"] = ctrl;
            var template = await _metaData.Create(model);
             

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> add(int id)
        {
            ViewData["application"] = app;
            ViewData["action"] = " Add Fields";
            ViewData["controller"] = ctrl;
            MetaDataViewModel model = new MetaDataViewModel();
            model.MetaData = await _metaData.GetTemplateByIdAsync(id);
            model.Fields = await _metaDataTemplateService.GetFieldsByTemplateIdAsync(id);
            var fiels = await _field.GetAllField();
            ViewData["fielId"] = new SelectList(fiels, "FieldId", "FieldName");
            return View(model);
        }

        // Add Field

        [HttpPost]
        public async Task<IActionResult> AddField([Bind("FieldId,MetaDataTemplateId")] MetaDataTemplateField metaDataTemplateField)
        {
            // create _metaDataTemplateService
            var model = await _metaDataTemplateService.AddField(metaDataTemplateField);
            return RedirectToAction("add",  new { id = metaDataTemplateField.MetaDataTemplateId });
        }

        // delete Field

        [HttpPost]
        public async Task<IActionResult> DeleteField(int id , int MetaDataTemplateId)
        {
            // create _metaDataTemplateService
           
            var model = await _metaDataTemplateService.DeleteField(id , MetaDataTemplateId);

            return RedirectToAction("add", new { id = MetaDataTemplateId });
        }

        // update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null) return NotFound();
            ViewData["application"] = app;
            ViewData["action"] = " Edit MetaData ";
            ViewData["controller"] = ctrl;
            var metaData = await _metaData.GetTemplateByIdAsync(id);
         
            return View(metaData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description")] MetaDataDTO model)
        {

            if (ModelState.IsValid)
            {
                var result =await _metaData.UpdateTemplateAsync(id,model);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Error updating field");

            return View(model);


        }


        // Delete 

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            _metaData.Delete(id);

            return RedirectToAction("Index");
        }

        // 
    }

}
