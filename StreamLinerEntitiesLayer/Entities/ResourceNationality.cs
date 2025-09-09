using StreamLinerEntitiesLayer.HREntities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.Entities
{
    public class ResourceNationality
    {
        [Key]
        public int NationalityId { get; set; }
        [Display(Name = "Nationality Name")]
        public string NationalityName { get; set; }
        public ICollection<Partner>? Partner { get; set; }
    }
}
