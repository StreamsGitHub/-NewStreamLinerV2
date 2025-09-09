using StreamLinerEntitiesLayer.HREntities;
using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class CheckOutViewModel
    {
        // [Key]
        [Display(Name = "Employee Name")]
        public int EmployeeId { get; set; }


        [Display(Name = "Check OUT")]
        public DateTime CheckOut { get; set; }

        public IEnumerable<HRAttendances>? HRAttendances { get; set; }
    }
}
