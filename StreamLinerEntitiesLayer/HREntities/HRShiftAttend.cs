using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRShiftAttend : MasterModel
    {
        [Key]
        public int HRShiftAttendId { get; set; }

        [Display(Name = "Shift Name")]
        public string ShiftName { get; set; }
        [Display(Name = "Start")]
        public TimeSpan ShiftStart { get; set; }
        [Display(Name = "End")]
        public TimeSpan ShiftEnd { get; set; }
        [Display(Name = "Shift Count")]
        public decimal? ShiftCount { get; set; }
        //public ICollection<Users>? Users { get; set; }       
        public ICollection<HRAttendances>? HRAttendances { get; set; }

    }
}
