using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class UploadFileViewModel
    {
        [Display(Name = "Document Name")]
        public string DocumentName { get; set; }
        [Display(Name = "Document File")]
        public IFormFile? DocumentFile { get; set; }

        //public IFormFile? File { get; set; }
        public string? Extension { get; set; }
    }
}
