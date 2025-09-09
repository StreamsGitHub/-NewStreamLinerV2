using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRRequestState : MasterModel
    {
        [Key]
        public int HRRequestStateId { get; set; }

        [Display(Name = "Request State ")]
        public string RequestStateName { get; set; }
      
    }
}
