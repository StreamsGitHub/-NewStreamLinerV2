using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class SalarySlipViewModel
    {
        [Key]
        public int UsersId { get; set; }
        public DateTime date { get; set; }
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }
        public decimal Salary { get; set; } = 0;
        public decimal TotalBenefits { get; set; }
        public decimal Incentive { get; set; }
        public decimal OverTime { get; set; }
        public decimal Allowances { get; set; }
        public decimal Bonus { get; set; }
        public decimal Transportation { get; set; }
        public decimal HouseRent { get; set; }
        public decimal TotalPenalty { get; set; }
        public decimal ProfessionalTax { get; set; }
        public decimal ProvidentFund { get; set; }
        public decimal HealthInsurance { get; set; }
        public decimal TDS { get; set; }
        public decimal LoanRecovery { get; set; }
        public decimal Total { get; set; } = 0;
    }
}
