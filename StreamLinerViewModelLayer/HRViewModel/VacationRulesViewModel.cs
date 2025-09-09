
using StreamLinerEntitiesLayer.HREntities;
using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class VacationRulesViewModel
    {
        [Key]
        public HRVacationRule? HRVacationRule { get; set; }
        public IEnumerable<HRVacationRuleLine>? HRVacationRuleLine { get; set; }
    }
}
