
using System.ComponentModel.DataAnnotations;

namespace StreamLinerEntitiesLayer.Entities
{
    public class MeetingRoom : MasterModel
    {
        [Key]
         public int MeetingRoomId { get; set; }
        public string Name { get; set; }
       public virtual ICollection<Meeting> Mettings { get; set; }

    }
}