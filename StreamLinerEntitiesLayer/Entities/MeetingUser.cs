using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StreamLinerEntitiesLayer.Entities
{
    public class MeetingUser : MasterModel
    {
        [Key]
        public int MeetingUserId { get; set; }

        [ForeignKey("Meeting")]
        public int MeetingId { get; set; }
        public virtual Meeting Meeting { get; set; }


        [ForeignKey("UserFrom")]
        public int? userIdFrom { get; set; }
        public virtual ApplicationUser? UserFrom { get; set; }

        [ForeignKey("UserTo")]
        public int? userIdTo { get; set; }
        public virtual ApplicationUser? UserTo { get; set; }




        public ActionMeeting action { get; set; }
        public enum ActionMeeting
        {
            pendding,
            Accpet,
            reject,
        }
        
    }
}