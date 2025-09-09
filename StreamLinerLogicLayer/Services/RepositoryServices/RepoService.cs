using StreamLinerEntitiesLayer.Entities;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerViewModelLayer.ModelDTO;

namespace StreamLinerLogicLayer.Services.RepositoryServices
{
    public class RepoService : IRepoService
    {
        private readonly IGenericRepository<RepositoriesDisk> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RepoService(IUnitOfWork unitOfWork , IGenericRepository<RepositoriesDisk> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = unitOfWork.RepositoriesDisk;
        }
        public Task<CreationResponse> CreateRepository(CreateRepositoryDto Model)
        {
            var creationResponse = new CreationResponse();
            var sameRepo = _genericRepository.GetAll().FirstOrDefault(f => f.RepositoryName == Model.RepositoryName);
            if (sameRepo == null)
            {
                var repos = _genericRepository.GetAll();
                if (repos.Count() >= 9)
                {
                    creationResponse.IsSuccess = false;
                    creationResponse.Message = "You have reached the maximum number of repositories allowed.";
                   // creationResponse.Message = "You cannot create more than 9 Repositories on this Licensing";
                    return Task.FromResult(creationResponse);
                }

                RepositoriesDisk repositories = new RepositoriesDisk()
                {
                    RepositoryName = Model.RepositoryName,
                    Description = Model.Description,
                    License = Model.License,
                    SizeInMB = Model.SizeInMB,
                    OwnerId = Model.OwnerId,
                    IsPrivate = Model.IsPrivate
                };
                _genericRepository.AddAsync(repositories);
                _unitOfWork.Save();
                creationResponse.IsSuccess = true;
                creationResponse.Message = "Folder deleted successfully";
            }
            else
            {
                creationResponse.IsSuccess = false;
                creationResponse.Message = "Repository with the same name already exists";
            }
            return Task.FromResult(creationResponse);
        }

        public Task<CreationResponse> DeleteRepository(int repoId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ListRepositoriesViewModel>> GetAllRepositories()
        {
            var repos = _genericRepository.GetAll();
            var repoList = repos.Select(r => new ListRepositoriesViewModel
            {
                RepositoryId = r.RepositoryId,
                RepositoryName = r.RepositoryName,
                Description = r.Description,
            }).ToList();
            return Task.FromResult(repoList);
        }

        public Task<List<ListRepositoriesViewModel>> GetRepositoriesByOwnerId(int ownerId)
        {
            var repos = _genericRepository.GetAll().Where(r=>r.OwnerId == ownerId);
            var repoList = repos.Select(r => new ListRepositoriesViewModel
            {
                RepositoryId = r.RepositoryId,
                RepositoryName = r.RepositoryName
            }).ToList();
            return Task.FromResult(repoList);
        }

        public Task<RepositoriesDisk> GetRepositoryById(int repoId)
        {
            var repo = _genericRepository.GetAll().FirstOrDefault(r => r.RepositoryId == repoId);
            return Task.FromResult(repo);
        }

        public Task<int> GetTotalRepositoryCount()
        {
            var count = _genericRepository.GetAll().Count();
            return Task.FromResult(count);
        }

        public Task<bool> IsRepositoryExists(string repoName, int companyId)
        {
            var repo = _genericRepository.GetAll().FirstOrDefault(r => r.RepositoryName == repoName);
            return Task.FromResult(repo != null);
        }

        public Task<CreationResponse> UpdateRepository(UpdateRepositoryDto repositoryDto)
        {
            var creationResponse = new CreationResponse();
            var repo = _genericRepository.GetAll().FirstOrDefault(r => r.RepositoryId == repositoryDto.RepositoryId);
            if (repo != null)
            {
                repo.RepositoryName = repositoryDto.RepositoryName;
                repo.Description = repositoryDto.Description;
                repo.License = repositoryDto.License;
                repo.IsPrivate = repositoryDto.IsPrivate;
                repo.SizeInMB = repositoryDto.SizeInMB;
                _genericRepository.Update(repo);
                _unitOfWork.Save();
                creationResponse.IsSuccess = true;
                creationResponse.Message = "Repository updated successfully";
            }
            else
            {
                creationResponse.IsSuccess = false;
                creationResponse.Message = "Repository not found";
            }
            return Task.FromResult(creationResponse);

        }
    }
}
