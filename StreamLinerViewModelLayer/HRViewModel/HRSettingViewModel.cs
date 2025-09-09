using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class HRSettingViewModel
    {
        [Required]
        public int PartnerId { get; set; }
        [Display(Name = "Department")]

        public int DepartmentId { get; set; }
        [Display(Name = "Shift")]
        public int HRShiftAttendId { get; set; }

        public int JobId { get; set; }
        public int ManagerId { get; set; }
            [Display(Name = "Specialist Level")]
        public int SpecialistLevelId { get; set; }

        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Display(Name = "Finger Print Id")]
        [Required]
        public int FingerPrintId { get; set; }

    }
}
