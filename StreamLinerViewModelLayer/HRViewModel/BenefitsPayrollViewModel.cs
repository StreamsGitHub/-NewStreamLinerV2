using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class BenefitsPayrollViewModel
    {
        public int HRBenefitsTypeId { get; set; }
        [Display(Name = "Benefits Name")]
        public string BenefitsName { get; set; }
        public decimal BenefitValue { get; set; }
    }
}
