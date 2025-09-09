using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using StreamLinerEntitiesLayer.Entities;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRAttend : MasterModel
    {
        [Key]
        public int AttendId { get; set; }

        [Display(Name = "Empolyee")]
        [ForeignKey("Partner")]
        public int PartnerId { get; set; }
        public Partner? Partner { get; set; }


        [Display(Name = "Shift")]
        [ForeignKey("HRShiftAttend")]
        public int HRShiftAttendId { get; set; }
        public HRShiftAttend? HRShiftAttend { get; set; }

        [Display(Name = "Date")]
        public DateTime? CheckDate { get; set; }

        [Display(Name = "Check In")]
        public DateTime CheckIn { get; set; }

        [Display(Name = "Check Out")]
        public DateTime CheckOut { get; set; }

        [Display(Name = "Penalty In")]
        public int Penaltyin { get; set; }

        [Display(Name = "Penalty Out")]
        public int PenaltyOut { get; set; }
        public decimal OverTime { get; set; }

        public string? MonthCode { get; set; }

        [Display(Name = "Type")]
        public int CheckTypeIn { get; set; }
        [Display(Name = "Type")]
        public int CheckTypeOut { get; set; }

        public string? IconIn { get; set; }
        public string? IconOut { get; set; }

    }
}
