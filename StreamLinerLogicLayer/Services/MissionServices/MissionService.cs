using StreamLinerEntitiesLayer.HREntities;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerViewModelLayer.HRViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.MissionServices
{
    public class MissionService : IMissionService
    {
        private readonly IGenericRepository<HRMission> _repository;
        private readonly IGenericRepository<Partner> _partnerRepository;
        public MissionService(
            IGenericRepository<HRMission> repository,
            IGenericRepository<Partner> partnerRepository)
        {
            _repository = repository;
            _partnerRepository = partnerRepository;
        }
        public Task CreateMissionAsync(MissionViewModel model, int userId, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMissionAsync(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<HRMission?> GetMissionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HRMission>> GetMissionsAsync(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMissionAsync(MissionViewModel model, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
