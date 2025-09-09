using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities
{
    
    public class Field : MasterModel
    {
        [Key]
        public int FieldId { get; set; }
        
        public string FieldName { get; set; }

        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public virtual FieldType? Type { get; set; }
      

        public bool IsRequired { get; set; }
        public string? Placeholder { get; set; }

        public List<MetaDataTemplateField> MetaDataTemplateFields { get; set; }
    }
}
