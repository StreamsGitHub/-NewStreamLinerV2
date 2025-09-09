using StreamLinerEntitiesLayer.HREntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class PermissionViewModel
    {
        public int HRPermissionId { get; set; }

        [Display(Name = " Permission Name  ")]
        public string PermissionName { get; set; }

        [Display(Name = "Empolyee")]
        public int PartnerId { get; set; }

        public string? Reason { get; set; }

        [Display(Name = "Month Code")]
        public string? MonthCode { get; set; }
        [Display(Name = "Start Time ")]
        public DateTime? StartTime { get; set; }
        [Display(Name = "  End Time")]
        public DateTime? EndTime { get; set; }
        public bool Approved { get; set; }
        [Display(Name = "  State ")]

        public int PermissionState { get; set; }


        public bool Allowed { get; set; } // مسموح       ولا لأ
    }
}
