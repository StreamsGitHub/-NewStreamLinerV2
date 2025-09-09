using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class PenaltyPayrollViewModel
    {
        public int HRPenaltyId { get; set; }
        [Display(Name = "Penalty Name")]
        public string PenaltyName { get; set; }
        public decimal PenaltyValue { get; set; }

       
    }
}
