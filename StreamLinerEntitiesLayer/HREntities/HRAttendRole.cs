using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRAttendRole : MasterModel
    {

        [Key]
        public int AttendRoleId { get; set; }
        [Display(Name = "Name Attend Role")]
        public string NameAttendRole { get; set; }

        [Display(Name = "Late Min")]
        public int LateMin { get; set; }
        [Display(Name = "Penalty Min")]
        public int PenaltyMin { get; set; }


    
    }
}
