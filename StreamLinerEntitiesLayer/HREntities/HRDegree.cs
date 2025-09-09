using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRDegree : MasterModel
    {
        [Key]
        public int HRDegreeId { get; set; }

        [Display(Name = "Degree Name")]
        public string DegreeName { get; set; }
        public ICollection<HRApplication>? HRApplication { get; set; }
    }
}
