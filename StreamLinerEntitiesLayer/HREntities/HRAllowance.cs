using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using StreamLinerEntitiesLayer.Entities;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRAllowance  : MasterModel
    {
        [Key]
        public int HRAllowanceId { get; set; }
        [Display(Name = "Empolyee")]
        [ForeignKey("Partner")]
        public int PartnerId { get; set; }
        public Partner? Partner { get; set; }


        [Display(Name = "Manager")]
        public int ManagerId { get; set; }
        [Display(Name = "Manager")]
        public string? ManagerName { get; set; }

        [Display(Name = "Days")]
        public decimal AllowanceDays { get; set; }

        [Display(Name = "Date")]
        public DateTime? AllowanceDate { get; set; }
        [Display(Name = "Value")]

        public decimal AllowanceValue { get; set; }
        [Display(Name = "Hours")]

        public int AllowanceHour { get; set; }

        public string? Description { get; set; }
        public string MonthCode { get; set; }

        public bool Approved { get; set; }

        [ForeignKey("HRAllowanceType")]
        [Display(Name = " Allowance Type")]
        public int HRAllowanceTypeId { get; set; }
        public HRAllowanceType? HRAllowanceType { get; set; }


        public bool Allowed { get; set; } // مسموح       ولا لأ
    }
}
