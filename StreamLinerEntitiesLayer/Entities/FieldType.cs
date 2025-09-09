using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities
{
    public class FieldType : MasterModel
    {
        [Key]
         public int TypeId { get; set; }
        public string Type { get; set; }
       ICollection<Field> Fields { get; set; }
    }

}
