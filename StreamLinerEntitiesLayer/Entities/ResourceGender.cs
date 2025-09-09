using StreamLinerEntitiesLayer.HREntities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.Entities
{
    public class ResourceGender
    {
        [Key]
        public int GenderId { get; set; }

        [Display(Name = "Gender Name")]
        public string GenderName { get; set; }
        public ICollection<Partner>? Partner { get; set; }
    }
}
