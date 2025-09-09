using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class UploadFile : MasterModel
    {
        [Key]
        public int UploadFileId { get; set; }
        [Display(Name = "Document Name")]
        public string? DocumentName { get; set; }
        [Display(Name = "Document File")]
        public string? DocumentFile { get; set; }
        public string? Extension { get; set; }

    }
}
