using StreamLinerEntitiesLayer.HREntities;
using StreamLinerViewModelLayer.HRViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.PermissionServices
{
    public interface IPermissionService
    {
        Task<IEnumerable<HRPermission>> GetPermissionsAsync(int companyId);
        Task<HRPermission?> GetPermissionByIdAsync(int id);
        Task CreatePermissionAsync(PermissionViewModel model, int userId, int companyId);
        Task UpdatePermissionAsync(PermissionViewModel model, int userId);
        Task DeletePermissionAsync(int id, int userId);
    }
}
