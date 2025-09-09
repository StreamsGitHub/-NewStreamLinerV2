using StreamLinerEntitiesLayer.HREntities;
using StreamLinerViewModelLayer.HRViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.PenaltyServices
{
    public interface IPenaltyService
    {
        Task<IEnumerable<HRPenalty>> GetPenaltysAsync(int companyId);
        Task<HRPenalty?> GetPenaltyByIdAsync(int id);
        Task CreatePenaltyAsync(PenaltiesViewModel model, int userId, int companyId);
        Task UpdatePenaltyAsync(PenaltiesViewModel model, int userId);
        Task DeletePenaltyAsync(int id, int userId);
    }
}
