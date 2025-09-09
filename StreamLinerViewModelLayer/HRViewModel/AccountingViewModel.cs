using System.ComponentModel.DataAnnotations;

// StreamLinerViewModelLayer.HRViewModels
namespace StreamLinerViewModelLayer.HRViewModel
{
    public class AccountingViewModel
    {
        public int PartnerId { get; set; }
        [Display(Name = "BankAccount Number")]
        public int BankAccountNumber { get; set; } = 0;
        public decimal Salary { get; set; } = 0;
     
    }
}
