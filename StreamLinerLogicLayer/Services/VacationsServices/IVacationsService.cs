using StreamLinerEntitiesLayer.HREntities;
using StreamLinerViewModelLayer.HRViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.VacationsServices
{
    public interface IVacationsService
    {
        Task<IEnumerable<HRVacations>> GetVacationsAsync(int companyId);
        Task<HRVacations?> GetVacationByIdAsync(int id);
        Task CreateVacationAsync(VacationViewModel model, int userId, int companyId);
        Task UpdateVacationAsync(VacationViewModel model, int userId);
        Task DeleteVacationAsync(int id, int userId);
    }
}
