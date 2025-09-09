using AutoMapper;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerViewModelLayer.ModelDTO;


namespace StreamLinerLogicLayer.Services
{
    public class ProjectService : IProject
    {
        private readonly IGenericRepository<Projects> _projectRepository;
        private readonly IMapper _mapper;
        public ProjectService(IGenericRepository<Projects> projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;

        }
        public Task<bool> AddProject(ProjectDto projectDto)
        {
            throw new NotImplementedException();
        }

       
        public Task<ProjectDto> GetProjectById(int id)
        {
            ProjectDto projectDto = new ProjectDto();
            return Task.FromResult(projectDto);
        }

        Task<List<ProjectDto>> IProject.GetAllProjects()
        {
            var projects = _projectRepository.GetAllAsync().Result.ToList();
            if (projects == null || projects.Count == 0)
            {

            }

            var projectlist = _mapper.Map<List<ProjectDto>>(projects);
            return Task.FromResult(projectlist);
        }


    }
}
