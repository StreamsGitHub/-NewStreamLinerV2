using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRSource : MasterModel
    {
        [Key]
        public int SourceId { get; set; }

        [Display(Name = "Source Name")]
        public string HRSourceName { get; set; }
        public ICollection<HRApplication>? HRApplication { get; set; }
    }
}
