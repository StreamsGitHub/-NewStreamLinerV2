using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRPermission : MasterModel
    {
        [Key]
        public int HRPermissionId { get; set; }

        [Display(Name = " Permission Name  ")]
        public string PermissionName { get; set; }

        [Display(Name = "Empolyee")]
        [ForeignKey("Partner")]
        public int PartnerId { get; set; }
        public Partner? Partner { get; set; }

        public string? Reason { get; set; }

        [Display(Name = "Month Code")]
        public string? MonthCode { get; set; }
        [Display(Name = "Start Time ")]
        public DateTime? StartTime { get; set; }
        [Display(Name = "  End Time")]
        public DateTime? EndTime { get; set; }
        public bool Approved { get; set; }
        [Display(Name = "  State ")]

        public int PermissionState { get; set; }


        public bool Allowed { get; set; } // مسموح       ولا لأ

    }
}
