using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRStage : MasterModel
    {
        [Key]
        public int HRStageId { get; set; }

        [Display(Name = "Stage Name")]
        public string StageName { get; set; }
        public string? Requirements { get; set; }

        public ICollection<HRApplication>? HRApplication { get; set; }
    }
}
