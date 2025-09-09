using StreamLinerEntitiesLayer.HREntities;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerViewModelLayer.HRViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.VacationsServices
{
    public class VacationsService : IVacationsService
    {
        private readonly IGenericRepository<HRVacations> _repository;
        private readonly IGenericRepository<Partner> _partnerRepository;
        public VacationsService(
            IGenericRepository<HRVacations> repository,
            IGenericRepository<Partner> partnerRepository)
        {
            _repository = repository;
            _partnerRepository = partnerRepository;
        }
        public Task CreateVacationAsync(VacationViewModel model, int userId, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteVacationAsync(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<HRVacations?> GetVacationByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HRVacations>> GetVacationsAsync(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateVacationAsync(VacationViewModel model, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
