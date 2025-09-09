using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using StreamLinerEntitiesLayer.Entities;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRBenefits : MasterModel
    {

        [Key]
        public int HRBenefitsId { get; set; }

        [Display(Name = "Empolyee")]
        [ForeignKey("Partner")]
        public int PartnerId { get; set; }
        public Partner? Partner { get; set; }

        [Display(Name = "Manager Name")]
        public int ManagerId { get; set; }
        [Display(Name = "Manager Name")]
        public string? ManagerName { get; set; }

        [Display(Name = "Days")]
        public decimal BenefitDays { get; set; } 

        [Display(Name = "Date")]
        public DateTime? BenefitDate { get; set; }
        [Display(Name = "Value")]

        public decimal BenefitValue { get; set; }
        [Display(Name = "Hours")]

        public int BenefitHour { get; set; }

        public string? Description { get; set; }
        public string MonthCode { get; set; }

        public bool Approved { get; set; }

        [ForeignKey("HRBenefitsType")]
        [Display(Name = " Type")]
        public int HRBenefitsTypeId { get; set; }
        public HRBenefitsType? HRBenefitsType { get; set; }


        public bool Allowed { get; set; } // مسموح       ولا لأ
    }
   
}
