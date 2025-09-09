using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities
{
    public class TemplateFields : MasterModel
    {
        [Key]
        public int TemplateFieldId { get; set; }
        [Required]
        [ForeignKey("Field")]
        [Display(Name = "Field")]

        public int FieldId { get; set; }
        public Field Field { get; set; }


        [Required]
        [ForeignKey("DocTemplate")]
        [Display(Name = "Document Template")]

        public int DocTemplateId { get; set; }
        public DocTemplate DocTemplate { get; set; }
    }
}
