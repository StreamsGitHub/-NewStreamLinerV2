using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities
{
    public class FolderUserPermission : MasterModel
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Folder")]
        public int FolderId { get; set; }
        public virtual Folder Folder { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("PermissionType")]
        public int PermissionTypeId { get; set; }
        public virtual PermissionType PermissionType { get; set; }

    }
}
