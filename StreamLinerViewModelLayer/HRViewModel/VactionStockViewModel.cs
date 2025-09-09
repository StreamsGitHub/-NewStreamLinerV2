using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class VactionStockViewModel
    {
        [Key]
        public int UsersId { get; set; }
        [Display(Name = "Name")]
        public string UsersName { get; set; }

        public int Stock { get; set; }
        public int Vacations { get; set; }

        public int Balance { get; set; }

        [Display(Name = " Vacation Type  ")]
        public string? VacationTypeName { get; set; }
    }
}
