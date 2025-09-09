using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class TeamWorkDetailsViewModel
    {
        [Key]
        public int HRTeamWorkId { get; set; }
        public int UsersId { get; set; }
    }
}
