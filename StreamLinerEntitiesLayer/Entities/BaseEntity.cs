using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using StreamLinerEntitiesLayer.Entities.IEntity;


namespace StreamLinerEntitiesLayer.Entities
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public int Id { get; set; } 

        public string? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
