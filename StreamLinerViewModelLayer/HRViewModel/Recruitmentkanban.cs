using StreamLinerEntitiesLayer.HREntities;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class Recruitmentkanban
    {
        public HRStage HRStage { get; set; }

        public IEnumerable<HRApplication>? HRApplication { get; set; }
    }
}
