using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class ResourcesCostViewModel
    {
        [Display(Name = "ID")]
        public int PartnerId { get; set; }

        [Display(Name = " Name  ")]
        public string? FullName { get; set; }
        public double Salary { get; set; } = 0;


        [Display(Name = " Insurance ")]
        public double InsuranceDibet { get; set; } = 0;

        public double Benefits { get; set; } = 0;

        public double Compensation { get; set; } = 0;

        public double Penalties { get; set; } = 0;


        public double CompanyDebit { get; set; } = 0;
    }
}
