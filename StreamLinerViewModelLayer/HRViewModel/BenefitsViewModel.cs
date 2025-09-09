using StreamLinerEntitiesLayer.HREntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class BenefitsViewModel
    {
        public int HRBenefitsId { get; set; }

        [Display(Name = "Empolyee")]
        public int PartnerId { get; set; }
  
       
        [Display(Name = "Date")]
        public DateTime? BenefitDate { get; set; }
        [Display(Name = " Category   ")]
        public int BenefitType { get; set; } // ( 1 => value , 2 => Days , 3 => Hour )
        [Display(Name = "Value")]
        public decimal ViewValue { get; set; }
        [Display(Name = " Type  ")]
        public int HRBenefitsTypeId { get; set; }
        public string? Description { get; set; }
 
    }
}
