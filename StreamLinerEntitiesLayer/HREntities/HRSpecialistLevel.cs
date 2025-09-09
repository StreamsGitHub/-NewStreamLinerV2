using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRSpecialistLevel : MasterModel
    {
        [Key]
        public int SpecialistLevelId { get; set; }

        [Display(Name = "Specialist Level Name")]
        public string SpecialistLevelName { get; set; }
        //public ICollection<ApplicationUser>? Users { get; set; }
    }
}
