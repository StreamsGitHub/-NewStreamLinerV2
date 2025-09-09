using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRCompany : MasterModel
    {
        [Key]
        public int CompanyId { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        public string Companylogo { get; set; } = "img/Defaultlogo.jpg";
        public ICollection<ApplicationUser>? Users { get; set; }
    }
}
