using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRRequestType : MasterModel
    {
        [Key]
        public int HRRequestTypeId { get; set; }

        [Display(Name = "Request Type")]
        public string RequestTypeName { get; set; }
        public ICollection<HRRequest>? HRRequest { get; set; }
    }
}
