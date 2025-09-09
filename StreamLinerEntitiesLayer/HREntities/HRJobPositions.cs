using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using StreamLinerEntitiesLayer.Entities;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRJobPositions : MasterModel
    {
        [Key]
        public int JobId { get; set; }

        [Display(Name = "Job Titel")]
        public string JobTitel { get; set; }

        [ForeignKey("HRDepartment")]
        public int DepartmentId { get; set; }
        public HRDepartment? HRDepartment { get; set; }

        [Display(Name = "Expected New Employees")]
        public int ExpectedNewEmployees { get; set; }

        [Display(Name = "Job Description ")]
        public string? JobDescription { get; set; }

       

    }
}
