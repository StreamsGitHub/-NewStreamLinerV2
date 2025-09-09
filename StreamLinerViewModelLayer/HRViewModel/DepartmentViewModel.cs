//using Microsoft.CodeAnalysis.Options;

using StreamLinerEntitiesLayer.HREntities;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class DepartmentViewModel :HRDepartment
    {
        public int DepartmentId { get; set; }
        public string ParentName { get; set; }
        public int EmployeeCount { get; set; }
    }
}
