using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using StreamLinerEntitiesLayer.Entities;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRAdvancePayment : MasterModel
    {
        [Key]
        public int HRAdvancePaymentId { get; set; }

        [Display(Name = "Empolyee")]
        [ForeignKey("Partner")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Employee")]
        public int PartnerId { get; set; }
        public Partner? Partner { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Date is required")]
        public DateTime? AdvancePaymentDate { get; set; }
        [Display(Name = "Value")]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a valid Value")]
        public decimal AdvancePaymentValue { get; set; }
        public string? Description { get; set; }
        public string? MonthCode { get; set; }
        public bool Approved { get; set; }
        public bool Paided { get; set; }

    }
}
