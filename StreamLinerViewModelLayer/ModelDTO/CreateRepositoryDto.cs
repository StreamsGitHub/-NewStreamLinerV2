using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class CreateRepositoryDto
    {
        [Required]
        [Display(Name = "Repository Name")]
        public string RepositoryName { get; set; }

        [Required]
        public string Description { get; set; }
        
        public string? License { get; set; }

        [Display(Name = "Size in MB")]
        [Range(1024, int.MaxValue, ErrorMessage = "Size must be more than 1024 MB")]
        public int SizeInMB { get; set; } = 1024;
        public int OwnerId { get; set; }

        [Display(Name = "Permission Type")]
        public bool IsPrivate { get; set; }

    }
}
