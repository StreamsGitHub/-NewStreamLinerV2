using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class AllowanceViewModel
    {
        public int HRAllowanceId { get; set; }
        [Display(Name = "Employee")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Employee")]
        public int PartnerId { get; set; }
        
        [Display(Name = "Date")]
        [Required (ErrorMessage = "Date is required")]
        public DateTime? AllowanceDate { get; set; }
      
        [Display(Name = "Value")]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a valid Value")]
        public decimal ViewValue { get; set; }
        [Display(Name = "Allowance Type")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Allowance Type")]
        public int HRAllowanceTypeId { get; set; }
        public string? Description { get; set; }
    }
}
