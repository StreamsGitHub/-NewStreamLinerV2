using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamLinerEntitiesLayer.Entities
{
    public class RepoUserPermission : MasterModel
    {
        [Key]
        public int RepoUserPermissionId { get; set; }

        [ForeignKey("RepositoriesDisk")]
        public int RepositoryId { get; set; }
        public virtual RepositoriesDisk RepositoriesDisk { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("RepoPermissionType")]
        public int RepoPermissionTypeId { get; set; }
        public virtual RepoPermissionType RepoPermissionType { get; set; }
    }
}
