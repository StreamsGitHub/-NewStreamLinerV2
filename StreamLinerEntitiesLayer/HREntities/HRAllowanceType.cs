using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRAllowanceType : MasterModel
    {
        [Key]
        public int HRAllowanceTypeId { get; set; }
        [Display(Name = "Allowance Type")]
        public string HRAllowanceTypeName { get; set; }
        public ICollection<HRAllowance>? HRAllowance { get; set; }
    }
}
