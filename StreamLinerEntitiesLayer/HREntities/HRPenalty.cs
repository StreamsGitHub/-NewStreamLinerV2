using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRPenalty : MasterModel
    {

        [Key]
        public int HRPenaltyId { get; set; }
        [Display(Name = "Empolyee")]
        [ForeignKey("Partner")]
        public int PartnerId { get; set; }
        public Partner? Partner { get; set; }

        [Display(Name = "Manager Name")]
        public int ManagerId { get; set; }

        [Display(Name = "Manager Name")]
        public string? ManagerName { get; set; }
        [Display(Name = "Days")]
        public decimal PenaltyDays { get; set; }
        [Display(Name = "Date")]
        public DateTime? PenaltyDate { get; set; }
        [Display(Name = "Value")]
        public decimal PenaltyValue { get; set; }
        [Display(Name = "Hours")]
        public int PenaltyHour { get; set; }
        public string? Description { get; set; }
        public string? MonthCode { get; set; }
        public bool Approved { get; set; }

        [ForeignKey("HRPenaltyType")]
        [Display(Name = "Penalty Type")]
        public int HRPenaltyTypesId { get; set; }
        public HRPenaltyTypes? HRPenaltyTypes { get; set; }

        public bool Allowed { get; set; } // مسموح       ولا لأ

    }
}
