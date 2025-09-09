using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class userDto
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? pwd { get; set; }

        [Required(ErrorMessage = "User Type is required")]
        [Display(Name = "User Type")]
        public string? Role { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
        public int DepartmentId { get; set; }
        public string? JobTitle { get; set; }

        public DateTime? BirthDate { get; set; } = DateTime.Now;
        public DateTime? HireDate { get; set; } = DateTime.Now;


        [Display(Name = "Volume Quota (MB)")]
        [Range(1, 1000000, ErrorMessage = "Volume quota must be between 1 MB and 1 TB")]
        public decimal? Volumequota { get; set; } = 0;
        [Display(Name = "Quota (per File)")]
        [Range(1, 1000000, ErrorMessage = "Max quota per file must be between 1 MB and 1 TB")]
        public decimal? Maxquota { get; set; } = 0;

        public IFormFile ProfileImage { get; set; }



        public bool DMS { get; set; }
        public bool CMS { get; set; }
        public bool BPM { get; set; }
        public bool Oper { get; set; }
        public bool CRM { get; set; }
        public bool HRM { get; set; }



    }
}
