using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamLinerEntitiesLayer.Entities
{
    public class Projects : MasterModel
    {
        [Key]
        public int ProjectId { get; set; }
        public string Project_Name { get; set; }
        public DateTime Project_Date { get; set; }
        public bool Closed { get; set; }
    }
}
