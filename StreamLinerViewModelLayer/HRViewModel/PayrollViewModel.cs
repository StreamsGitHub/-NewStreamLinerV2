using System;
using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class PayrollViewModel
    {
        [Display(Name = "Partner Id")]
        public int PartnerId { get; set; }

        [Display(Name = " Full Name  ")]
        public string? FullName { get; set; }
        public double Salary { get; set; } = 0;

        public decimal Late { get; set; } = 0;
        [Display(Name = " Over Time ")]
        public decimal TotalOverTime { get; set; } = 0;

        public double Insurance { get; set; } = 0;
        public double Tax { get; set; } = 0;
        public IEnumerable<AllowancePayrollViewModel>? AllowancePayrollViewModel { get; set; }
        public IEnumerable<BenefitsPayrollViewModel>? BenefitsPayrollViewModel { get; set; }
        public IEnumerable<PenaltyPayrollViewModel>? PenaltyPayrollViewModel { get; set; }

        [Display(Name = "  Total Allowance ")]
        public decimal TotalAllowance { get; set; } = 0;
        [Display(Name = " Total Benefits   ")]
        public decimal TotalBenefits { get; set; } = 0;

        [Display(Name = " Total Penalty   ")]
        public decimal TotalPenalty { get; set; } = 0;

        [Display(Name = "  Grand Total  ")]
        public decimal GrandTotal { get; set; } = 0;

    }
}
