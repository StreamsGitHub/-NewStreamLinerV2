using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class CreateVacationRulesViewModel
    {
        [Key]
        public int VacationRuleId { get; set; }
        [Display(Name = "Rule Name")]
        public string VacationRuleName { get; set; }
        [Display(Name = "Leave Description")]
        public string? VacationDesc { get; set; }
    }
}
