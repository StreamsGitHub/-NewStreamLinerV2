using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StreamLinerLogicLayer.Services;
using StreamLinerViewModelLayer.ModelDTO;
using System.Threading.Tasks;

namespace StreamLinerApp.Areas.Archive.Controllers
{
    [Area("Archive")]
    public class ProjectsController : Controller
    {
        private readonly IProject _project;
        private readonly IMapper _mapper;

        public ProjectsController(IProject project, IMapper mapper)
        {
            _project = project;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var projects = await _project.GetAllProjects();
            return View(projects);
        }
    }
}
