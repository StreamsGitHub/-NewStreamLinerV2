

using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRVacationRule : MasterModel
    {
        [Key]
        public int VacationRuleId { get; set; }
        [Display(Name = "Rule Name")]
        public string VacationRuleName { get; set; }

        [Display(Name = "Leave Description")]
        public string? VacationDesc { get; set; }
        public ICollection<HRVacationRuleLine>? HRVacationRuleLine { get; set; }
        public ICollection<HREmployeeRule>? HREmployeeRule { get; set; }
    }
}
