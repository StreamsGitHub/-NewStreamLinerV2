using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities
{
    public class DocTemplate : MasterModel
    {
        [Key]
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string? TemplateBody { get; set; }

    }
}
