using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRNotes : MasterModel
    {
        [Key]
        public int HRApplicationNotesId { get; set; }
        [Display(Name = "Application Note")]
        public string? ApplicationNote { get; set; }
     
        [ForeignKey("HRApplication")]
        public int ApplicationId { get; set; }
        public HRApplication? HRApplication { get; set; }
    }
}
