using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using StreamLinerEntitiesLayer.Entities;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRTeamWorkDetails : MasterModel
    {
        [Key]
        public int HRTeamWorkDetailsId { get; set; }

        [ForeignKey("HRTeamWork")]
        [Display(Name = "HRTeam Work")]
        public int HRTeamWorkId { get; set; }
        public HRTeamWork? HRTeamWork { get; set; }

        [Display(Name = "Empolyee")]
        [ForeignKey("User")]
        public int userId { get; set; }
        public ApplicationUser? User { get; set; }

    }
}
