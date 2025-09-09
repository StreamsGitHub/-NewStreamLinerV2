using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HREmployeeRule : MasterModel
    {
        [Key]
        public int EmployeeRuleId { get; set; }

        [Display(Name = "Empolyee")]
        [ForeignKey("Partner")]
        public int PartnerId { get; set; }
        public Partner? Partner { get; set; }

        [ForeignKey("HRVacationRule")]
        [Display(Name = " Rule ")]
        public int VacationRuleId { get; set; }
        public HRVacationRule? HRVacationRule { get; set; }
        
        [ForeignKey("FinancialYear")]
        [Display(Name = "Financial Year")]
        public int FinancialYearID { get; set; }
        public FinancialYear? FinancialYear { get; set; }
    }
}
