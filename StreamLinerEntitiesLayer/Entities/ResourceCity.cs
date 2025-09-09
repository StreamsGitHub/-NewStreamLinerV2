using StreamLinerEntitiesLayer.HREntities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.Entities
{
    public class ResourceCity
    {
        [Key]
        public int CityId { get; set; }
        [Display(Name = "City Name")]
        public string CityName { get; set; }
        public ICollection<Partner>? Partner { get; set; }
    }
}
