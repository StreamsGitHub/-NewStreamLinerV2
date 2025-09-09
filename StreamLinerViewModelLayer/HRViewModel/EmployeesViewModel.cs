using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class EmployeesViewModel
    {
        [Key]
        public int PartnerId { get; set; }
        
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "  Middle Name")]
        [Required]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name ")]
        [Required]
        public string? LastName { get; set; }

        [Display(Name = "Birth Data  ")]
        [Required]
        public DateTime BirthData { get; set; }
        [Display(Name = " Hire Data   ")]
        public DateTime? HireData { get; set; }
      
      
 
        [Display(Name = " Address ")]
        public string? Street { get; set; }

        [Display(Name = "City")]
        public int CityId { get; set; }
        [Display(Name = " User Type    ")]
        public int UsersType { get; set; }
        public bool Employee { get; set; } = false;
        public bool Customer { get; set; } = false;
        public bool Vendor { get; set; } = false;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }

        public string? Website { get; set; }
        public string? Note { get; set; }
        public int PassportNo { get; set; } = 0;

 

        [Display(Name = "Gender")]
        public int GenderId { get; set; } = 0;

        [Display(Name = " Marital Status    ")]
        public int MaritalStatusId { get; set; } = 0;

        // HR Data
        [Display(Name = " Department ")]
        public int DepartmentId { get; set; }        
        [Display(Name = "Shift Attend")]
        public int HRShiftAttendId { get; set; }
        [Display(Name = "  Job ")]
        public int JobId { get; set; }
        [Display(Name = " Manager  ")]
        public int ManagerId { get; set; }
        [Display(Name = "  Coach ")]
        public int? CoachId { get; set; }
 

        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        // Accounting Data
        [Display(Name = " Bank Account Number  ")]
        public int BankAccountNumber { get; set; } = 0;
        public decimal Salary { get; set; } = 0;
        public string? Insurance { get; set; }
        [Display(Name = "  Tax Incom  ")]
        public decimal Tax { get; set; }
    }
}
