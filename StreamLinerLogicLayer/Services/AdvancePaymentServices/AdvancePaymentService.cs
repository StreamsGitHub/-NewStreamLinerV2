using StreamLinerEntitiesLayer.HREntities;
using StreamLinerRepositoryLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.AdvancePaymentServices
{
    public class AdvancePaymentService : IAdvancePaymentService
    {
        private readonly IGenericRepository<HRAdvancePayment> _repository;

        public AdvancePaymentService(IGenericRepository<HRAdvancePayment> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<HRAdvancePayment>> GetAdvancePaymentsAsync(int companyId)
        {
            return await _repository.GetAllIncludingAsync(x => x.Partner)
                           .ContinueWith(task =>
                               task.Result.Where(x => x.Active && x.CompanyId == companyId));
           // return await _repository.FindAsync(x => x.Active && x.CompanyId == companyId);
        }

        public async Task<HRAdvancePayment?> GetAdvancePaymentByIdAsync(int id)
        {
            var results = await _repository.GetAllIncludingAsync(x => x.Partner);
            return results.FirstOrDefault(x => x.HRAdvancePaymentId == id && x.Active);
          //  return await _repository.GetFindAsync(x => x.HRAdvancePaymentId == id && x.Active &&);
        }

        public async Task CreateAdvancePaymentAsync(HRAdvancePayment advancePayment, int userId, int companyId)
        {
            advancePayment.CreateId = userId;
            advancePayment.CreatedDate = DateTime.Now;
            advancePayment.CompanyId = companyId;
            advancePayment.Active = true;
            advancePayment.Approved = true;
            advancePayment.MonthCode = Convert.ToDateTime(advancePayment.AdvancePaymentDate).ToString("yyMM");
                                                         
            await _repository.AddAsync(advancePayment);  
            await _repository.SaveChangesAsync();        
        }                                                
                                                         
        public async Task UpdateAdvancePaymentAsync(HRAdvancePayment advancePayment, int userId)
        {
            advancePayment.UpdateId = userId;
            advancePayment.UpdatedDate = DateTime.Now;
            advancePayment.Updated = true;
            advancePayment.Active = true;

            _repository.Update(advancePayment);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAdvancePaymentAsync(int id, int userId)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return;

            entity.DeleteId = userId;
            entity.DeletedDate = DateTime.Now;
            entity.Active = false;

            _repository.Update(entity);
            await _repository.SaveChangesAsync();
        }

        
    }
}

