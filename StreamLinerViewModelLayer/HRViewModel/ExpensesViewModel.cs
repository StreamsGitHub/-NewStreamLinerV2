using StreamLinerEntitiesLayer.HREntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class ExpensesViewModel
    {
        public int HRExpensesId { get; set; }

        [Display(Name = "Empolyee")]
        public int PartnerId { get; set; }
      


        [Display(Name = "Manager Name")]
        public int ManagerId { get; set; }
        [Display(Name = "Manager Name")]
        public string? ManagerName { get; set; }

        [Display(Name = "Date")]
        public DateTime? ExpensesDate { get; set; }

        [Display(Name = "Value")]
        public decimal HRExpensesValue { get; set; }
        public string? Description { get; set; }

        public string MonthCode { get; set; }

        public bool Approved { get; set; }


        public bool Allowed { get; set; } // مسموح       ولا لأ
    }
}
