using StreamLinerEntitiesLayer.HREntities;
using StreamLinerViewModelLayer.HRViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.ExpensesServices
{
    public interface IExpensesService
    {
        Task<IEnumerable<HRExpenses>> GetExpensessAsync(int companyId);
        Task<HRExpenses?> GetExpensesByIdAsync(int id);
        Task CreateExpensesAsync(ExpensesViewModel model, int userId, int companyId);
        Task UpdateExpensesAsync(ExpensesViewModel model, int userId);
        Task DeleteExpensesAsync(int id, int userId);
    }
}
