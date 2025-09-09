using StreamLinerEntitiesLayer.HREntities;
using StreamLinerViewModelLayer.HRViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.BenefitServices
{
    public interface IBenefitService
    {
 
        Task<IEnumerable<HRBenefits>> GetAllBenefitsAsync(int companyId);
        Task<HRBenefits?> GetBenefitByIdAsync(int id);
        Task CreateBenefitAsync(BenefitsViewModel model, int userId, int companyId);
        Task UpdateBenefitAsync(BenefitsViewModel model, int userId, int companyId);
        Task DeleteBenefitAsync(int id, int userId);
       

    }
}
