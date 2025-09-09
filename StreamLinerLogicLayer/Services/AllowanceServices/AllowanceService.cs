using StreamLinerEntitiesLayer.HREntities;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerViewModelLayer.HRViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.AllowanceServices
{
    public class AllowanceService : IAllowanceService
    {
        private readonly IGenericRepository<HRAllowance> _repository;
        private readonly IGenericRepository<Partner> _partnerRepository;

        public AllowanceService(IGenericRepository<HRAllowance> repository,
                                IGenericRepository<Partner> partnerRepository)
        {
            _repository = repository;
            _partnerRepository = partnerRepository;
        }

        public async Task<IEnumerable<HRAllowance>> GetAllowancesAsync(int companyId)
        {
            var results = await _repository.GetAllIncludingAsync(x => x.Partner, x => x.HRAllowanceType);
            return results.Where(x => x.Active && x.CompanyId == companyId)
                          .OrderByDescending(x => x.HRAllowanceId);
        }

        public async Task<HRAllowance?> GetAllowanceByIdAsync(int id)
        {
            var results = await _repository.GetAllIncludingAsync(x => x.Partner, x => x.HRAllowanceType);
            return results.FirstOrDefault(x => x.HRAllowanceId == id && x.Active);
        }

        public async Task CreateAllowanceAsync(AllowanceViewModel model, int userId, int companyId)
        {
            string monthCode = Convert.ToDateTime(model.AllowanceDate).ToString("yyMM");
            var emp = await _partnerRepository.GetByIdAsync(model.PartnerId);
            var manager = await _partnerRepository.GetByIdAsync(emp.ManagerId);
            int managerId = 0;
            if (manager != null)
                managerId = manager.PartnerId;
            else managerId = emp.PartnerId;

            HRAllowance allowance = new HRAllowance
            {
                PartnerId = model.PartnerId,
                ManagerId = managerId,
                ManagerName = manager?.FullName,
                AllowanceDate = model.AllowanceDate,
                AllowanceValue = model.ViewValue,
                Description = model.Description,
                MonthCode = monthCode,
                HRAllowanceTypeId = model.HRAllowanceTypeId,
                AllowanceHour = 0,
                AllowanceDays = 0,
                Allowed = true,
                Approved = true,
                Active = true,
                CompanyId = companyId,
                CreateId = userId,
                CreatedDate = DateTime.Now,
                
            };

            await _repository.AddAsync(allowance);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAllowanceAsync(AllowanceViewModel model, int userId)
        {
            var allowance = await _repository.GetByIdAsync(model.HRAllowanceId);
            if (allowance == null) return;

            allowance.AllowanceDate = model.AllowanceDate;
            allowance.Description = model.Description;
            allowance.HRAllowanceTypeId = model.HRAllowanceTypeId;
            allowance.PartnerId = model.PartnerId;
            allowance.AllowanceValue = model.ViewValue;

            allowance.UpdateId = userId;
            allowance.UpdatedDate = DateTime.Now;
            allowance.Active = true;

            _repository.Update(allowance);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAllowanceAsync(int id, int userId)
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
