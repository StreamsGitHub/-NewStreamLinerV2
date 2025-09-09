using StreamLinerEntitiesLayer.HREntities;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerViewModelLayer.HRViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.ExpensesServices
{
    public class ExpensesService : IExpensesService
    {
        private readonly IGenericRepository<HRExpenses> _repository;
        private readonly IGenericRepository<Partner> _partnerRepository;
        public ExpensesService(
            IGenericRepository<HRExpenses> repository,
            IGenericRepository<Partner> partnerRepository)
        {
            _repository = repository;
            _partnerRepository = partnerRepository;
        }

        public Task CreateExpensesAsync(ExpensesViewModel model, int userId, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteExpensesAsync(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<HRExpenses?> GetExpensesByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HRExpenses>> GetExpensessAsync(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateExpensesAsync(ExpensesViewModel model, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
