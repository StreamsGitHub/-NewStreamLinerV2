using StreamLinerEntitiesLayer.HREntities;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class HRApplicationViewModel
    {
        public HRApplication HRApplication { get; set; }

        public IEnumerable<HRStage>? HRStage { get; set; }
        public IEnumerable<HRNotes>? HRNotes { get; set; }

        public int DocumentCount { get; set; }


    }
}
