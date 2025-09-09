using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StreamLinerEntitiesLayer.Entities
{
    public class TaskList : MasterModel
    {
        [Key]
        public int TaskListId { get; set; }

        public string TaskName { get; set; }

        public string Description { get; set; }

        public PriortyCorrespondence priorty { get; set; }

        [Required]
        [DateGreaterThan("StartDate")]
        public DateTime DueDate { get; set; }

        public DateTime StartDate { get; set; }
        public string? Note { get; set; }

        public bool Status { get; set; }

        public virtual ICollection<TasksAttachment> TasksAttachments { get; set; }

        public virtual ICollection<TasksUser> TasksUsers { get; set; } 
    }
}