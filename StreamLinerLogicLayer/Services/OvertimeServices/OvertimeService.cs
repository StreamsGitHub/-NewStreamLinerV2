using StreamLinerEntitiesLayer.HREntities;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerViewModelLayer.HRViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.OvertimeServices
{
    public class OvertimeService : IOvertimeService
    {
        private readonly IGenericRepository<HROverTimes> _repository;
        private readonly IGenericRepository<Partner> _partnerRepository;
        public OvertimeService(
            IGenericRepository<HROverTimes> repository,
            IGenericRepository<Partner> partnerRepository)
        {
            _repository = repository;
            _partnerRepository = partnerRepository;
        }
        public Task CreateOvertimeAsync(OvertimeViewModel model, int userId, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOvertimeAsync(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<HROverTimes?> GetOvertimeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HROverTimes>> GetOvertimesAsync(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOvertimeAsync(OvertimeViewModel model, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
