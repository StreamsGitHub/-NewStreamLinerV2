using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRVacations : MasterModel
    {
        [Key]
        public int VacationId { get; set; }

        [Display(Name = "Empolyee")]
        [ForeignKey("Partner")]
        public int PartnerId { get; set; }
        public Partner? Partner { get; set; }


        [Display(Name = "State")]
        public int VacationState { get; set; } // الحالة 


        [ForeignKey("HRVacationType")]
        [Display(Name = " Leave Type   ")]
        public int VacationTypeId { get; set; }  //  نوع الاجازة
        public HRVacationType? HRVacationType { get; set; }  

        [Display(Name = "Stock    ")]
        public int VacationStock { get; set; }   // عدد رصيد الاجازات

        [Display(Name = "Days")]
        public int VacationDays { get; set; }   // عدد ايام الاجازات

        [Display(Name = " Date ")]
        public DateTime VacationDate { get; set; }   // التاريخ

      
        [Display(Name = "Year")]
        public int FinancialYearID { get; set; }  // السنة 
        public string? Reason { get; set; } // سبب الاجازة

        [Display(Name = "Manager")]
        public int? ManagerId { get; set; } // المدير

        public bool Approved { get; set; } // المدير وافق ولا لأ



        public bool Allowed { get; set; } // مسموح انه ياخد اجازة ولا لأ

        [Display(Name = "  Allowed Reason  ")]
      
        public string? AllowedReason { get; set; } // سبب الاجازة

        [Display(Name = " Approved Reason  ")]
        public string? ApprovedReason { get; set; } // سبب الاجازة
    }
}
