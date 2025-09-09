using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class Partner : MasterModel
    {
        [Key]
        public int PartnerId { get; set; }

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
        public int FingerPrintId { get; set; }

        [Display(Name = "Birth Data")]
        public DateTime BirthData { get; set; }
        [Display(Name = "Hire Data")]
        public DateTime? HireData { get; set; }
        [Display(Name = "Image")]
        public string? EmployeeImage { get; set; }

        public string? Street { get; set; }

        [ForeignKey("ResourceCity")]
        public int CityId { get; set; }
        public ResourceCity? ResourceCity { get; set; }

        [Display(Name = "Partner Type")]
        public int PartnerType { get; set; }
        public bool Employee { get; set; } = false;
        public bool Customer { get; set; } = false;
        public bool Vendor { get; set; } = false;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Website { get; set; }
        public string? Note { get; set; }
        public int PassportNo { get; set; }

        [ForeignKey("ResourceNationality")]
        public int NationalityId { get; set; }
        public ResourceNationality? ResourceNationality { get; set; }

        [ForeignKey("ResourceGender")]
        public int GenderId { get; set; }
        public ResourceGender? ResourceGender { get; set; }

        [ForeignKey("ResourceMaritalStatus")]
        public int MaritalStatusId { get; set; }
        public ResourceMaritalStatus? ResourceMaritalStatus { get; set; }

        // HR Data
        [ForeignKey("HRDepartment")]
        public int DepartmentId { get; set; }
        public HRDepartment? HRDepartment { get; set; }

        public int JobId { get; set; }
        public int ManagerId { get; set; }
        public int? CoachId { get; set; }



        [ForeignKey("HRCompany")]
        public int CompanyId { get; set; }
        public HRCompany? HRCompany { get; set; }

        // Accounting Data
        [Display(Name = "BankAccount Number")]
        public int BankAccountNumber { get; set; } = 0;
        public decimal Salary { get; set; } = 0;
        public decimal Insurance { get; set; } = 0;
        public decimal Tax { get; set; } = 0;


        //Contract
        [Display(Name = "Contract Number")]
        public int ContractId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        //[ForeignKey("HRShiftAttend")]
        [Display(Name = "Shift ")]
        public int HRShiftAttendId { get; set; }
        //public HRShiftAttend? HRShiftAttend { get; set; }
        public ICollection<HRTeamWorkDetails>? HRTeamWorkDetails { get; set; }
        public ICollection<HRBenefits>? HRBenefits { get; set; }
        public ICollection<HRPenalty>? HRPenalty { get; set; }

        public ICollection<HRContract>? HRContract { get; set; }
        public ICollection<HRPermission>? HRPermission { get; set; }
        public ICollection<HRMission>? HRMission { get; set; }

        public ICollection<HRRequest>? HRRequest { get; set; }
    }
}
