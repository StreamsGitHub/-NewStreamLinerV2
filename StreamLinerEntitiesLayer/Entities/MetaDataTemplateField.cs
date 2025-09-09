using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamLinerEntitiesLayer.Entities
{
    public class MetaDataTemplateField : MasterModel
    {
        [Key]
        public int MetaDataTemplateFieldId { get; set; }
        [Required]
        [ForeignKey("Field")]
        [Display(Name = "Field")]

        public int FieldId { get; set; }
        public Field Field { get; set; }

         
        [Required]
        [ForeignKey("MetaDataTemplate")]
        [Display(Name = " MetaDataTemplate")]

        public int MetaDataTemplateId { get; set; }
        public MetaDataTemplate MetaDataTemplate { get; set; }
         

    }
}
