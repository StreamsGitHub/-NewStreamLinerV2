using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class InsuranceViewModel
    {
        [Display(Name = "Id")]
        public int PartnerId { get; set; }

        [Display(Name = " Full Name  ")]
        public string? FullName { get; set; }
        public double Salary { get; set; } = 0;

       
        [Display(Name = " Insurance Dibet  ")]
        public double InsuranceDibet { get; set; } = 0;
        [Display(Name = "  Company Dibet    ")]
        public double CompanyDibet { get; set; } = 0;
    }
}
