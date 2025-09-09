using StreamLinerEntitiesLayer.HREntities;
using StreamLinerViewModelLayer.HRViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.AllowanceServices
{
    public interface IAllowanceService
    {
        Task<IEnumerable<HRAllowance>> GetAllowancesAsync(int companyId);
        Task<HRAllowance?> GetAllowanceByIdAsync(int id);
        Task CreateAllowanceAsync(AllowanceViewModel model, int userId, int companyId);
        Task UpdateAllowanceAsync(AllowanceViewModel model, int userId);
        Task DeleteAllowanceAsync(int id, int userId);
        
    }
}
