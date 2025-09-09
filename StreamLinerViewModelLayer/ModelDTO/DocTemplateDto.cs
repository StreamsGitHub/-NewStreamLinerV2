using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class DocTemplateDto
    {
        public int Id { get; set; } 
        [Display(Name = "Template Name")]
        public string? TemplateName { get; set; }
        public string? TemplateBody { get; set; }
    }
}
