using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace StreamLinerViewModelLayer.HRViewModel
{
    public class ApplicationDocumentsViewModel 
    {
        //[Display(Name = "Document File")]
        //public IFormFile? DocumentFile { get; set; }
        public string DocumentName { get; set; }
       
        public int ApplicationId { get; set; }
        public int DocumentCount { get; set; }
    }
}
