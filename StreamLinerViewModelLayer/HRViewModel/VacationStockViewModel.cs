using StreamLinerEntitiesLayer.HREntities;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class VacationStockViewModel
    {
        public IEnumerable<HRVacations>? HRVacations { get; set; }

        public IEnumerable<VactionStockViewModel>? VactionStockViewModel { get; set; }
    }
}
