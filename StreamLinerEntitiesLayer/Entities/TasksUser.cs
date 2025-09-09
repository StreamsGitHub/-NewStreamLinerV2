using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StreamLinerEntitiesLayer.Entities
{
    public class TasksUser : MasterModel
    {
        [Key]
        public int TaskUserId { get; set; }

        [ForeignKey("Tasks")]
        public int? TaskId { get; set; }
        public virtual TaskList Tasks { get; set; }




        [ForeignKey("UserFrom")]
        public int? userIdFrom { get; set; }
        public virtual ApplicationUser? UserFrom { get; set; }

        [ForeignKey("UserTo")]
        public int? userIdTo { get; set; }
        public virtual ApplicationUser? UserTo { get; set; }


    }
}