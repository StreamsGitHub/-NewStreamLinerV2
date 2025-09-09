using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StreamLinerEntitiesLayer.Entities
{
    public class Meeting : MasterModel
    {
        [Key]
        public int MeetingId { get; set; }

        public string? Name { get; set; }

        public string Description { get; set; }

        public PriortyCorrespondence priorty { get; set; }

        public TimeSpan startTimeMeeting { get; set; }

        public DateTime StartDateMeeting { get; set; }
        public DateTime duedatemeeting { get; set; }

        public DateTime? StartTime { get; set; }

        public string? Note { get; set; }

        public string? Link { get; set; }

        public MeetingRoom? MettingRoom { get; set; }

        [ForeignKey("MettingRoom")]

        public int MettingRoomId { get; set; }
        public virtual ICollection<MeetingUser> MettingUsers { get; set; }

        public bool syncOutlook { get; set; }
        public string? eventOutlookId { get; set; }
    }
}