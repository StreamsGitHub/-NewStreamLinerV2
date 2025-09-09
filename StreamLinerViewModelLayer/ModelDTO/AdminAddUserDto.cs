using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class AdminAddUserDto
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }

        [Required]
        public string? Position { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public int? Status { get; set; }

        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Password { get; set; }
        public string? ConfirmedPassword { get; set; }
        //[Required]
        public int OrganizationId { get; set; }
        public List<string>? RoleName { get; set; }

        public string? ExpireLincesedate { get; set; }

        public string? LinceseKey { get; set; }
        public bool? isActive { get; set; }

        public DateTime? Created_at { get; set; }

        public decimal? Volumequota { get; set; }
        public decimal? Maxquota { get; set; }
    }
}
