using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities
{
    public class FolderUserPermissionScope : MasterModel
    {
        [Key]
        public int Id { get; set; }
        public int FolderId { get; set; }
        public int UserId { get; set; }
        public int PermissionScopeId { get; set; }
    }
}
