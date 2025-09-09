using StreamLinerEntitiesLayer.HREntities;
using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class CheckInViewModel
    {
        //[Key]
        [Display(Name = "Employee Name")]
        public int EmployeeId { get; set; }

        [Display(Name = "Check IN")]
        public DateTime CheckIn { get; set; }

        public IEnumerable<HRAttendances>? HRAttendances { get; set; }
    }
}
