using StreamLinerEntitiesLayer.HREntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class PenaltiesViewModel
    {
 

        [Display(Name = "Employee")]
      
        public int PartnerId { get; set; }
 
        [Display(Name = "Manager")]
       
        public DateTime? PenaltyDate { get; set; }
        public int PenaltyType { get; set; } // ( 1 => value , 2 => Days , 3 => Hour )
        public decimal ViewValue { get; set; }
        public int PenaltyHour { get; set; }
        public string? Description { get; set; }
        public int HRPenaltyTypesId { get; set; }




    }
}
