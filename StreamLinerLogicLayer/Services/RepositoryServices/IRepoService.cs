using StreamLinerEntitiesLayer.Entities;
using StreamLinerViewModelLayer.ModelDTO;

namespace StreamLinerLogicLayer.Services.RepositoryServices
{
    public interface IRepoService
    {
        Task<CreationResponse> CreateRepository(CreateRepositoryDto Model);
        Task<CreationResponse> UpdateRepository(UpdateRepositoryDto repositoryDto);
        Task<CreationResponse> DeleteRepository(int repoId);
        Task<List<ListRepositoriesViewModel>> GetAllRepositories();
        Task<RepositoriesDisk> GetRepositoryById(int repoId);
        Task<List<ListRepositoriesViewModel>> GetRepositoriesByOwnerId(int ownerId);
        Task<int> GetTotalRepositoryCount();

        // IsRepositoryExists
        Task<bool> IsRepositoryExists(string repoName, int companyId);

    }
}
