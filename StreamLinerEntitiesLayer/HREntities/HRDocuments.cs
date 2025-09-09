using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRDocuments : MasterModel

    {
        [Key]
        public int HRDocumentsId { get; set; }
        public string DocumentName { get; set; }
        [Display(Name = "Document File")]
        public string DocumentFile{ get; set; }
        
        [ForeignKey("HRApplication")]
        public int ApplicationId { get; set; }
        public HRApplication? HRApplication { get; set; }

    }
}
