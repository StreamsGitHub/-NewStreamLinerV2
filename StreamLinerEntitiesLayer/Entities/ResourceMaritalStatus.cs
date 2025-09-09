using StreamLinerEntitiesLayer.HREntities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.Entities
{
    public class ResourceMaritalStatus
    {
        [Key]
        public int MaritalStatusId { get; set; }

        [Display(Name = "Marital Status Name")]
        public string MaritalStatusName { get; set; }
        public ICollection<Partner>? Partner { get; set; }
    }
}
