using StreamLinerEntitiesLayer.HREntities;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerViewModelLayer.HRViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.PermissionServices
{
    public class PermissionService : IPermissionService
    {
        private readonly IGenericRepository<HRPermission> _repository;
        private readonly IGenericRepository<Partner> _partnerRepository;
        public PermissionService(
            IGenericRepository<HRPermission> repository,
            IGenericRepository<Partner> partnerRepository)
        {
            _repository = repository;
            _partnerRepository = partnerRepository;
        }
        public Task CreatePermissionAsync(PermissionViewModel model, int userId, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task DeletePermissionAsync(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<HRPermission?> GetPermissionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HRPermission>> GetPermissionsAsync(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePermissionAsync(PermissionViewModel model, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
