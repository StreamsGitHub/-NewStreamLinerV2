using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities
{
    public class RepoPermissionType : MasterModel
    {
        [Key]
        public int RepoPermissionTypeId { get; set; }
        public string PermissionTypeName { get; set; }
    }
}
