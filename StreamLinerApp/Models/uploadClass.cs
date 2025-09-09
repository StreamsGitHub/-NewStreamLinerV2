using System.ComponentModel.DataAnnotations;

namespace StreamLinerApp.Models
{
    public class uploadClass
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
