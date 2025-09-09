using StreamLinerEntitiesLayer.HREntities;
using StreamLinerViewModelLayer.HRViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.OvertimeServices
{
    public interface IOvertimeService
    {
        Task<IEnumerable<HROverTimes>> GetOvertimesAsync(int companyId);
        Task<HROverTimes?> GetOvertimeByIdAsync(int id);
        Task CreateOvertimeAsync(OvertimeViewModel model, int userId, int companyId);
        Task UpdateOvertimeAsync(OvertimeViewModel model, int userId);
        Task DeleteOvertimeAsync(int id, int userId);
    }
}
