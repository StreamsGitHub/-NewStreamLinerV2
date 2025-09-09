using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRDayType : MasterModel
    {
        [Key]
        public int HRDayTypeId { get; set; }

        [Display(Name = "Day Type")]
        public string HRDayTypeName { get; set; }
    }
}
