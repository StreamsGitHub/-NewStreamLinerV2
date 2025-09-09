using StreamLinerEntitiesLayer.HREntities;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerViewModelLayer.HRViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.PenaltyServices
{
    public class PenaltyService : IPenaltyService
    {
        private readonly IGenericRepository<HRPenalty> _repository;
        private readonly IGenericRepository<Partner> _partnerRepository;
        public PenaltyService(
            IGenericRepository<HRPenalty> repository,
            IGenericRepository<Partner> partnerRepository)
        {
            _repository = repository;
            _partnerRepository = partnerRepository;
        }
        public Task CreatePenaltyAsync(PenaltiesViewModel model, int userId, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task DeletePenaltyAsync(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<HRPenalty?> GetPenaltyByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HRPenalty>> GetPenaltysAsync(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePenaltyAsync(PenaltiesViewModel model, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
