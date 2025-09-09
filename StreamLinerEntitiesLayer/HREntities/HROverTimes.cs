using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using StreamLinerEntitiesLayer.Entities;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HROverTimes : MasterModel
    {
        [Key]
        public int OverTimesId { get; set; }

        [Display(Name = "Empolyee")]
        [ForeignKey("Partner")]
        public int PartnerId { get; set; }
        public Partner? Partner { get; set; }
        [Display(Name = "Manager Name")]
        public int ManagerId { get; set; }
        [Display(Name = "Manage rName")]
        public string? ManagerName { get; set; }
 

        [Display(Name = "Date")]
        public DateTime? OverTimeDate { get; set; }
        [Display(Name = "Value")]

        public decimal OverTimeValue { get; set; }
 
        public string? Description { get; set; }
        
        public string MonthCode { get; set; }
        
        public bool Approved { get; set; }

        public bool Allowed { get; set; } // مسموح       ولا لأ
    }
}
