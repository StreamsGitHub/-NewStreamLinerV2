using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StreamLinerEntitiesLayer.Entities
{
    public class MetaDataTemplate : MasterModel
    {
        [Key]
        public int MetaDataTemplateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public DateTime LastModifiedTime { get; set; }

        public virtual List<MetaDataTemplateField> MetaDataTemplateFields { get; set; }
    }
}