using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRContractType : MasterModel
    {
        [Key]
        public int HRContractTypeId { get; set; }

        [Display(Name = "Specialist Level Name")]
        public string HRContractTypeName { get; set; }
        public ICollection<HRContract>? HRContract { get; set; }
    }
}
