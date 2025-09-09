using StreamLinerEntitiesLayer.HREntities;
using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class TeamWorkViewModel
    {
        [Key]
        public HRTeamWork? HRTeamWork { get; set; }
        public IEnumerable<HRTeamWorkDetails>? HRTeamWorkDetails { get; set; }
    }
}
