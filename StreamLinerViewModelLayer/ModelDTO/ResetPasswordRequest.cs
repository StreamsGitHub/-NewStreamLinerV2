using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class ResetPasswordRequest
    {
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty; // Reset token (from email)
        public string NewPassword { get; set; } = string.Empty;
    }
}
