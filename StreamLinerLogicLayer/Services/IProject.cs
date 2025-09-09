using StreamLinerViewModelLayer.ModelDTO;

namespace StreamLinerLogicLayer.Services
{
    public interface IProject
    {
         Task<ProjectDto> GetProjectById(int id);
         Task<List<ProjectDto>> GetAllProjects();
         Task<bool> AddProject(ProjectDto projectDto);
        //// Task<bool> UpdateProject(ProjectDto projectDto);
        //// Task<bool> DeleteProject(int id);
        //// Task<bool> CloseProject(int id);
        //// Task<bool> OpenProject(int id);
    }
}
