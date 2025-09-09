using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using StreamLinerEntitiesLayer.Entities;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRMission : MasterModel
    {
        [Key]
        public int HRMissionId { get; set; }

        [Display(Name = "Mission Name ")]
        public string MissionName { get; set; }

        [Display(Name = "Empolyee")]
        [ForeignKey("Partner")]
        public int PartnerId { get; set; }
        public Partner? Partner { get; set; }

        [Display(Name = " Description  ")]
        public string? MissionDescription { get; set; }

        [ForeignKey("HRMissionType")]
        [Display(Name = " Type")]
        public int HRMissionTypeId { get; set; }
        public HRMissionType? HRMissionType { get; set; }
        public string? MonthCode { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }
     
        
        [Display(Name = "Manager")]
        public int? ManagerId { get; set; }

        public bool Approved { get; set; }

        [Display(Name = "State")]
        public int MissionState { get; set; }


        public bool Allowed { get; set; } // مسموح       ولا لأ

    }
}
