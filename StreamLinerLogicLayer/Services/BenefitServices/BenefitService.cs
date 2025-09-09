using Microsoft.EntityFrameworkCore;
using StreamLinerEntitiesLayer.HREntities;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerViewModelLayer.HRViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.BenefitServices
{
    public class BenefitService : IBenefitService
    {
        private readonly IGenericRepository<HRBenefits> _repository;
        private readonly IGenericRepository<Partner> _partnerRepository;
        private readonly IGenericRepository<HRBenefitsType> _benefitsTypRepository;

        public BenefitService(
            IGenericRepository<HRBenefits> repository,
            IGenericRepository<Partner> partnerRepository,
            IGenericRepository<HRBenefitsType> benefitsTypRepository)
        {
            _repository = repository;
            _partnerRepository = partnerRepository;
            _benefitsTypRepository = benefitsTypRepository;
        }

        public async Task<IEnumerable<HRBenefits>> GetAllBenefitsAsync(int companyId)
        {
            var results = await _repository.GetAllIncludingAsync(x => x.Partner, x => x.HRBenefitsType);
            return results.Where(x => x.Active && x.CompanyId == companyId).ToList();
        }

        public async Task<HRBenefits?> GetBenefitByIdAsync(int id)
        {
            var results = await _repository.GetAllIncludingAsync(x => x.Partner, x => x.HRBenefitsType);
            return results.FirstOrDefault(x => x.HRBenefitsId == id && x.Active);
        }

        public async Task CreateBenefitAsync(BenefitsViewModel model, int userId, int companyId)
        {
            string monthCode = Convert.ToDateTime(model.BenefitDate).ToString("yyMM");
            var employee = await _partnerRepository.GetByIdAsync(model.PartnerId);
            var manager = await _partnerRepository.GetByIdAsync(employee.ManagerId);
            int managerId = 0;
            if (manager != null)
                managerId = manager.PartnerId;
            else managerId = employee.PartnerId;

            HRBenefits Benefits = new HRBenefits
            {
                PartnerId = model.PartnerId,
                ManagerId = managerId,
                ManagerName = manager?.FullName,
                BenefitDate = model.BenefitDate,
                BenefitValue = model.ViewValue,
                Description = model.Description,
                MonthCode = monthCode,
                HRBenefitsTypeId = model.HRBenefitsTypeId,
                Allowed = true,
                Approved = true,
                Active = true,
                BenefitDays = 0,
                BenefitHour = 0,
                CompanyId = companyId,
            };

            // ViewValue = القيمة اللي جايلك من الفيو
            // PenaltyValue = القيمة اللي في الموديل

             
            decimal salary = employee.Salary;



            //  int HRshiftId = employee.siftId
            // var Shift = _context.HRShift.FindAsync(HRshiftId) ;
            // int countOfHourShift  = Shift.hour ;

            if (model.BenefitType == 1)
            {
                //  ViewValue = فلوس
                Benefits.BenefitValue = model.ViewValue;

            }
            else if (model.BenefitType == 2)
            {
                // ViewValue  =     ايام
                // PenaltyValue  =  salary / 30 * ViewValue

                Benefits.BenefitDays = model.ViewValue;
                // Benefits.BenefitHour = model.ViewValue / countOfHourShift;
                Benefits.BenefitValue = salary / 30 * model.ViewValue;
            }
            else if (model.BenefitType == 3)
            {
                // ViewValue=   ساعات 
                //   PenaltyValue = Salary / 30 / countOfHourShift * ViewValue

                //model.ViewValue = Benefits.BenefitHour;
                //var newvalue = salary * 30 / Penalty.PenaltyDays;
                Benefits.BenefitValue = salary / 30 / 8 * model.ViewValue;


            }


            Benefits.CreateId = userId;

            Benefits.CreatedDate = DateTime.Now;

            Benefits.Active = true;
            await _repository.AddAsync(Benefits);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateBenefitAsync(BenefitsViewModel model, int userId, int companyId)
        {
            var hRBenefits = await GetBenefitByIdAsync(model.HRBenefitsId);

            hRBenefits.HRBenefitsId = model.HRBenefitsId;
            hRBenefits.PartnerId = model.PartnerId;
            hRBenefits.HRBenefitsTypeId = model.HRBenefitsTypeId;
            hRBenefits.BenefitDate = model.BenefitDate;
            hRBenefits.Description = model.Description;
            hRBenefits.UpdateId = userId;
            hRBenefits.UpdatedDate = DateTime.Now;
            hRBenefits.Active = true;

            var employee = await _partnerRepository.GetByIdAsync(model.PartnerId);
            decimal salary = employee.Salary;
            if (model.BenefitType == 1)
            {
                //  ViewValue = فلوس
                hRBenefits.BenefitValue = model.ViewValue;

            }
            else if (model.BenefitType == 2)
            {
                // ViewValue  =     ايام
                // PenaltyValue  =  salary / 30 * ViewValue

                hRBenefits.BenefitDays = model.ViewValue;
                // Benefits.BenefitHour = model.ViewValue / countOfHourShift;
                hRBenefits.BenefitValue = salary / 30 * model.ViewValue;
            }
            else if (model.BenefitType == 3)
            {
                // ViewValue=   ساعات 
                //   PenaltyValue = Salary / 30 / countOfHourShift * ViewValue

                // model.ViewValue = hRBenefits.BenefitHour;
                //var newvalue = salary * 30 / Penalty.PenaltyDays;
                hRBenefits.BenefitValue = salary / 30 / 8 * model.ViewValue;


            }

            await _repository.AddAsync(hRBenefits);
            await _repository.SaveChangesAsync();
           
        }
        public async Task DeleteBenefitAsync(int id, int userId)
        {
            var entity = await _benefitsTypRepository.GetByIdAsync(id);
            if (entity == null) return;
            entity.DeleteId = userId;
            entity.DeletedDate = DateTime.Now;
            entity.Active = false;
            _benefitsTypRepository.Update(entity);
            await _benefitsTypRepository.SaveChangesAsync();

        }

    }
}
