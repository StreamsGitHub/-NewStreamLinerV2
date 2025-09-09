using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRPenaltyTypes : MasterModel
    {
        [Key]
        public int HRPenaltyTypesId { get; set; }
        
        [Display(Name = "Penalty Type ")]
        public string HRPenaltyTypeName { get; set; }
        public ICollection<HRPenalty>? HRPenalty { get; set; }
    }
}
