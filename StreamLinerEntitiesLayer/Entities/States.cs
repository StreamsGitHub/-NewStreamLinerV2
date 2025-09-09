using System.ComponentModel.DataAnnotations;

namespace StreamLinerEntitiesLayer.Entities
{
    public class States : MasterModel
    {
        [Key]
        public int StateId { get; set; }
        public string? StateName { get; set; }
        public string? Description { get; set; }
        public ICollection<Projects> Projects { get; set; }
    }
}
