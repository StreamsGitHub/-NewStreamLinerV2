using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRMissionType : MasterModel
    {
        [Key]
        public int HRMissionTypeId { get; set; }

        [Display(Name = "Mission Type")]
        public string HRMissionTypeName { get; set; }
        public ICollection<HRMission>? HRMission { get; set; }
    }
}
