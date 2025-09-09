using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class AllowancePayrollViewModel
    {
        public int AllowanceId { get; set; }
        [Display(Name = "Allowance Name")]
        public string AllowanceName { get; set; }
        public decimal AllowanceValue { get; set; }
    }
}
