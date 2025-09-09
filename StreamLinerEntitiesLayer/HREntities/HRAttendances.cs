using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using StreamLinerEntitiesLayer.Entities;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRAttendances : MasterModel
    {
        [Key]
        public int AttendanceId { get; set; }

        [Display(Name = "Empolyee")]
        [ForeignKey("Partner")]
        public int PartnerId { get; set; }
        public Partner? Partner { get; set; }

        [Display(Name = "Shift")]
        [ForeignKey("HRShiftAttend")]
        public int HRShiftAttendId { get; set; }
        public HRShiftAttend? HRShiftAttend { get; set; }

        [Display(Name = "Date")]
        public DateTime AttendDate { get; set; }

        [Display(Name = "Check In")]
        public DateTime CheckIn { get; set; }

        [Display(Name = "Check Out")]
        public DateTime CheckOut { get; set; }


        [Display(Name = "Type")]
        public int CheckType { get; set; }


        [Display(Name = "Penalty Min")]
        public int PenaltyMin{ get; set; }

        public string? MonthCode { get; set; }
        public double INLatitude { get; set; } 
        public double INLongitude { get; set; }
        public double OUTLatitude { get; set; }
        public double OUTLongitude { get; set; }

        // fa fa-map-marker OR fa fa-hand-o-up OR fa fa-laptop
        public string? icon { get; set; }

        

    }
}
