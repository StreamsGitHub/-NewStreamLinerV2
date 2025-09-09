using StreamLinerEntitiesLayer.HREntities;
using StreamLinerViewModelLayer.HRViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.MissionServices
{
    public interface IMissionService
    {
        Task<IEnumerable<HRMission>> GetMissionsAsync(int companyId);
        Task<HRMission?> GetMissionByIdAsync(int id);
        Task CreateMissionAsync(MissionViewModel model, int userId, int companyId);
        Task UpdateMissionAsync(MissionViewModel model, int userId);
        Task DeleteMissionAsync(int id, int userId);
    }
}
