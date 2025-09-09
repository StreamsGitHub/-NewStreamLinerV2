using StreamLinerEntitiesLayer.HREntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class CreateMissionViewModel
    {
        [Key]
        public int HRMissionId { get; set; }


        [Display(Name = "Mission Name  ")]
        public string MissionName { get; set; }


        [Display(Name = "Empolyee")]
        public int PartnerId { get; set; }



        [Display(Name = " Description  ")]
        public string? MissionDescription { get; set; }


        [Display(Name = "  Type   ")]
        public int HRMissionTypeId { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
