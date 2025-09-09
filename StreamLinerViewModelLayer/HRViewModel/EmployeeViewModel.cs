using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class EmployeeViewModel
    {
        public int UsersId { get; set; }
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        [Required]
        public string MiddleName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Birth Data")]
        public DateTime BirthData { get; set; }
        [Display(Name = "Hire Data")]
        public DateTime? HireData { get; set; }

        //[Display(Name = "Image")]
        //public IFormFile EmployeeImage { get; set; }
        public string? Title { get; set; }
        public string? Street { get; set; }

        public int CityId { get; set; }

        [Display(Name = "Users Type")]
        public int UsersType { get; set; }
        public bool Employee { get; set; } = false;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Website { get; set; }
        public string? Note { get; set; }
        public int PassportNo { get; set; }

        public int NationalityId { get; set; }

        public int GenderId { get; set; }

        public int MaritalStatusId { get; set; }


        // HR Data
        public int DepartmentId { get; set; }

        public int JobId { get; set; }
        public int? ManagerId { get; set; }
        public int? CoachId { get; set; }

        public int SpecialistLevelId { get; set; }

        public int CompanyId { get; set; }

        // Accounting Data
        [Display(Name = "BankAccount Number")]
        public int BankAccountNumber { get; set; } = 0;
        public decimal Salary { get; set; } = 0;


        public int HRShiftAttendId { get; set; }
    }
}
