using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
	public class RegisterViewModel
	{

        [Required(ErrorMessage = "*")]
        [Display(Name = " First Name")]
         public string FirstName { get; set; }


        [Display(Name = "Middle Name ")]
        [Required(ErrorMessage = "*")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = " Last Name ")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "*")]
        [EmailAddress]
        public string Email { get; set; }

		[Required(ErrorMessage = "*")]
		public string Mobile { get; set; }

		[Required(ErrorMessage = "*")]
		 
		[DataType(DataType.Password)]
		[Display(Name = " Password  ")]
		public string Password { get; set; }

		[Required(ErrorMessage = "*")]
		[DataType(DataType.Password)]
		[Display(Name = "     Confirm Password   ")]
		[Compare("Password", ErrorMessage = " the password not ")]
		[MaxLength(16)]
		public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "*")]
        public string? Company { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = " Job Titel    ")]
        public string? JobTitel { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "City ")]
        public int? CityId { get; set; }
    }
}
