using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRVacationType : MasterModel
    {

        [Key]
        public int VacationTypeId { get; set; }
        [Display(Name = "Type Name")]
        public string VacationTypeName { get; set; }
        public ICollection<HRVacationRuleLine>? HRVacationRuleLine { get; set; }
        public ICollection<HRVacations>? HRVacations { get; set; }


    }
}
