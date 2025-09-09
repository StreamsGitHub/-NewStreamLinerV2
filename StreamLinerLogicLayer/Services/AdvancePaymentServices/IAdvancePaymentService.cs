using StreamLinerEntitiesLayer.HREntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.AdvancePaymentServices
{
    public interface IAdvancePaymentService
    {
        Task<IEnumerable<HRAdvancePayment>> GetAdvancePaymentsAsync(int companyId);
        Task<HRAdvancePayment?> GetAdvancePaymentByIdAsync(int id);
        Task CreateAdvancePaymentAsync(HRAdvancePayment advancePayment, int userId, int companyId);
        Task UpdateAdvancePaymentAsync(HRAdvancePayment advancePayment, int userId);
        Task DeleteAdvancePaymentAsync(int id, int userId);
       
    }
}
