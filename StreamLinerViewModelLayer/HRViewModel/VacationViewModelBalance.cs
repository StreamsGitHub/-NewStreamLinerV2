using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class VacationViewModelBalance
    {
       [Key]
        public long id { get; set; }
        public int EmployeeId  { get; set; }
       public decimal Balance  { get; set; }
       public string VacationTypeName  { get; set; }
       public bool Approved { get; set; }
    }
}
