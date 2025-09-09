using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class EmpChangePhotoViewModel
    {
        public int PartnerId { get; set; }

        [Display(Name = "Change Photo")]
        public IFormFile EmployeeImage { get; set; }


    }
}
