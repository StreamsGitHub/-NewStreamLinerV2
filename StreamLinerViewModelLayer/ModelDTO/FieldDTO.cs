using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
   public  class FieldDTO
    {
        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public int TypeId { get; set; }
        public string? TypeName { get; set; }
        public bool IsRequired { get; set; }
        public string? Placeholder { get; set; }

    }
}
