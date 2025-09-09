using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class GetMCViewModel
    {
        [Range(1, 100000000, ErrorMessage = " Financial Year   !")]
        public int FinancialYearID { get; set;}

        [Range(1, 12, ErrorMessage = "Month   !")]
        public int Month { get; set; }
    }
}
