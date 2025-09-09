using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities
{
    public class PermissionType : MasterModel
    {
        [Key]
        public int Id { get; set; }
        public string PermissionName { get; set; }
        public bool ForDocument { get; set; }
        public bool ForFolder { get; set; }
    }
}
