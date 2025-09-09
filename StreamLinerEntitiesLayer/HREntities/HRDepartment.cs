using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRDepartment : MasterModel
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required]
        [Display(Name = " Department    ")]
        public string DepartmentName { get; set; }

        [Display(Name = " Parent   ")]
        public int? ParentId { get; set; }

        [Display(Name = "  Manager")]
        public int? HRManagerId { get; set; }
       

        public string? DepartmentDesc { get; set; }

        public ICollection<HRApplication>? HRApplication { get; set; }
        public ICollection<HRJobPositions>? HRJobPositions { get; set; }
        public ICollection<ApplicationUser>? Users { get; set; }

    }
}
