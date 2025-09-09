using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRTeamWork : MasterModel
    {
        [Key]
        public int HRTeamWorkId { get; set; }

        [Display(Name = "Team Work Name")]
        public string HRTeamWorkName { get; set; }
        public string? Description { get; set; }

        public ICollection<HRTeamWorkDetails>? HRTeamWorkDetails { get; set; }
    }
}
