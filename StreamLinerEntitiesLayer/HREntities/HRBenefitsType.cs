using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRBenefitsType : MasterModel
    {
        [Key]
        public int HRBenefitsTypeId { get; set; }
        [Display(Name = "Benefit Type")]
        public string HRBenefitsTypeName { get; set; }
        public ICollection<HRBenefits>? HRBenefits { get; set; }
    }
}
