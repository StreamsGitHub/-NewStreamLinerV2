using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class CreateEmployeeViewModel
    {
        [Display(Name = " Employee ID  ")]
        [Required]
        public int FingerPrintId { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name ")]
        [Required]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name ")]
        [Required]
        public string? LastName { get; set; }

        // --------------------------------

        [Display(Name = " Mobile  ")]
        [Required]
        public string? Mobile { get; set; }

        //----------------------------------------

        [Display(Name = " Birth Data")]
        [Required]
        public DateTime BirthData { get; set; }

        [Display(Name = "Gender")]
        [Range(1, 10, ErrorMessage = "Gender !")]
        public int GenderId { get; set; } = 0;

        [Display(Name = "  Marital Status    ")]
        [Range(1, 10, ErrorMessage = " Marital Status     !")]
        public int MaritalStatusId { get; set; } = 0;

        //----------------------------------------

        [Display(Name = " Employee Image    ")]
        public IFormFile? EmployeeImage { get; set; }

        //----------------------------------------



        [EmailAddress]
        public string? Email { get; set; }

        //----------------------------------------

      

        

        [Display(Name = " Address ")]
        [Required]
        public string? Street { get; set; }

        [Display(Name = "City")]
        [Range(1, 100000000, ErrorMessage = "City !")]
        public int CityId { get; set; }

        //----------------------------------------


  


        // HR Data
        [Display(Name = " Department ")]
        [Range(1, 100000000, ErrorMessage = "Department !")]
        public int DepartmentId { get; set; }

        [Display(Name = " Manager  ")]
       
        public int ManagerId { get; set; }

        //----------------------------------------


        [Display(Name = "  Job Titel ")]
        [Range(1, 100000000, ErrorMessage = "Job  !")]
        public int JobId { get; set; }


        

        [Display(Name = "Shift Attend")]
        [Range(1, 100000000, ErrorMessage = " Shift Attend !")]
        public int HRShiftAttendId { get; set; }

        //----------------------------------------



        // Accounting Data

        [Display(Name = "  Salary ")]
        [Required]
        public decimal Salary { get; set; } = 0;

        [Display(Name = "Bank Account Number  ")]
        [Required]
        public int BankAccountNumber { get; set; } = 0;

      

        //----------------------------------------



        [Display(Name = "  Insurance ")]
        [Required]
        public decimal Insurance { get; set; }

        [Display(Name = "  Income Tax ")]
        [Required]
        public decimal Tax { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? pwd { get; set; }

        [Required(ErrorMessage = "User Type is required")]
        [Display(Name = "User Type")]
        public string? Role { get; set; }



        [Display(Name = "Volume Quota (MB)")]
        [Range(1, 1000000, ErrorMessage = "Volume quota must be between 1 MB and 1 TB")]
        public decimal? Volumequota { get; set; } = 0;
        [Display(Name = "Quota (per File)")]
        [Range(1, 1000000, ErrorMessage = "Max quota per file must be between 1 MB and 1 TB")]
        public decimal? Maxquota { get; set; } = 0;



        public bool DMS { get; set; }
        public bool CMS { get; set; }
        public bool BPM { get; set; }
        public bool Oper { get; set; }
        public bool CRM { get; set; }
        public bool HRM { get; set; }
    }
}

