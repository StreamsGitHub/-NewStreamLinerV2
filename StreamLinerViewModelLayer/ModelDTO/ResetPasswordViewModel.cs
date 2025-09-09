using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password), Compare("NewPassword")]
        [Display(Name = "Confirm New Password")]
        public string ConfirmPassword { get; set; }
    }
}
