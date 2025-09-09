using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRVacationRuleLine : MasterModel
    {
        [Key]
        public int VacationRuleLineId { get; set; }
        [ForeignKey("HRVacationRule")]
        [Display(Name = "Leave Rule ")]
        public int VacationRuleId { get; set; }
        public HRVacationRule? HRVacationRule { get; set; }

        [ForeignKey("HRVacationType")]
        [Display(Name = "Leave Type")]
        public int VacationTypeId { get; set; }
        public HRVacationType? HRVacationType { get; set; }

        [Display(Name = "Leave Stock")]
        public int VacationStock { get; set; }
    }
}
