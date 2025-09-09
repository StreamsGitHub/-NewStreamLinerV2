using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class FinancialYear : MasterModel
    {
        [Key]
        public int FinancialYearID { get; set; }

        [Display(Name = "Fiscal Positions")]
        public string FinancialYearName { get; set; }

        [Display(Name = "Start Date")]
        public DateTime FromDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime ToDate { get; set; }

        //public ICollection<FinancialJournalEntry>? JournalEntry { get; set; }
        //public ICollection<FinancialPayment>? FinancialPayment { get; set; }

    }
}
