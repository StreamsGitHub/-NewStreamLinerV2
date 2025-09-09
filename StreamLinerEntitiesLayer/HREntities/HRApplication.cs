using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRApplication : MasterModel
    {
        [Key]
        public int ApplicationId { get; set; }
        [Display(Name = "Application Name")]
        public string ApplicationName { get; set; }  
        
        [Display(Name = "Applicant's Name")]
       
        public string? ApplicantName { get; set; }        
        public string? Tags { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string Mobile { get; set; }

        [Range(1, 3)]
        public decimal Appreciation { get; set; } = 0;

        [ForeignKey("HRDegree")]
        [Display(Name = "Degree")]
        public int HRDegreeId { get; set; }
        public HRDegree? HRDegree { get; set; }

        [Display(Name = "Next Action")]
        public DateTime? NextAction { get; set; }

        [ForeignKey("HRSource")]
        [Display(Name = "Source")]
        public int SourceId { get; set; }
        public HRSource? HRSource { get; set; }
        [Display(Name = "Referred By")]
        public string? ReferredBy { get; set; }

        //job
      
        public int JobId { get; set; }
       
        [ForeignKey("HRDepartment")]
        public int DepartmentId { get; set; }
        public HRDepartment? HRDepartment { get; set; }

        //Contract
        [Display(Name = "Expected Salary")]
        public decimal ExpectedSalary { get; set; }
        [Display(Name = "Proposed Salary")]
        public decimal? ProposedSalary { get; set; }
        public DateTime? Availability { get; set; }

        [ForeignKey("HRStage")]
        public int HRStageId { get; set; }
        public HRStage? HRStage { get; set; }
        public ICollection<HRDocuments>? HRDocuments { get; set; }
        public ICollection<HRNotes>? HRNotes { get; set; }

    }
}
