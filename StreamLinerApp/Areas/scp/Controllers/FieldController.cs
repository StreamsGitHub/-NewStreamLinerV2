using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerLogicLayer.Services.FieldServices;
using StreamLinerLogicLayer.Services.FieldTypeServices;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerViewModelLayer.ModelDTO;

namespace StreamLinerApp.Areas.scp.Controllers
{
    [Area("scp")]
    public class FieldController : Controller
    {
        private readonly IField _field;
        private readonly IFiedlType _fielType;
        private readonly IMapper _mapper;
        string app = "Admin";
        string ctrl = "Fields";
        public FieldController(IField field, IMapper mapper, IFiedlType fielType)
        {
            _field = field;
            _mapper = mapper;
            _fielType = fielType;

        }
        public async Task<IActionResult> Index()
        {
            ViewData["application"] = app;
            ViewData["action"] = " ";
            ViewData["controller"] = ctrl;
            var fields = await _field.GetAllField();
            return View(fields);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["application"] = app;
            ViewData["action"] = " New Field";
            ViewData["controller"] = ctrl;
            var filefiels = await _fielType.GetAlltypes();
            ViewData["TypeId"] = new SelectList(filefiels, "TypeId", "Type");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FieldDTO field)
        {

            if (ModelState.IsValid)
            {
                await _field.AddField(field);
                return RedirectToAction("Index");
            }

            var fileFields = await _fielType.GetAlltypes();
            ViewData["TypeId"] = new SelectList(fileFields, "TypeId", "Type", field.TypeId);

            return View(field);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null) return NotFound();
            ViewData["application"] = app;
            ViewData["action"] = " Edit Field";
            ViewData["controller"] = ctrl;
            var field = await _field.GetFieldById(id);
            var fileFields = await _fielType.GetAlltypes();

            ViewData["TypeId"] = new SelectList(fileFields, "TypeId", "Type", field.TypeId);

            return View(field);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FieldDTO field)
        {

            if (ModelState.IsValid)
            {



                var result = _field.UpdateField(field);

                return RedirectToAction("Index");

            }
            ModelState.AddModelError("", "Error updating field");
            var fileFields = _fielType.GetAlltypes().Result;

            ViewData["TypeId"] = new SelectList(fileFields, "TypeId", "Type", field.TypeId);
            return View(field);


        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return NotFound();
            ViewData["application"] = app;
            ViewData["action"] = " Delete Field";
            ViewData["controller"] = ctrl;
            var field = await _field.GetFieldById(id);
            var fileFields = await _fielType.GetAlltypes();

            ViewData["TypeId"] = new SelectList(fileFields, "TypeId", "Type", field.TypeId);

            return View(field);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           _field.DeleteField(id);

            return RedirectToAction("Index");
        }



    }
}
