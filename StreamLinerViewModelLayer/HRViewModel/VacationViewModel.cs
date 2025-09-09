using StreamLinerEntitiesLayer.HREntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class VacationViewModel
    {
        [Display(Name = "Empolyee")]
 
        public int PartnerId { get; set; }
    

        [Display(Name = " Vacation Type  ")]
        public int VacationTypeId { get; set; }
       
        [Range(1, 5)]
        [Display(Name = "Day  ")]
        public int VacationDays { get; set; }
        [Display(Name = "Date")]
        public DateTime VacationDate { get; set; }
        public string? Reason { get; set; }

    }
}
