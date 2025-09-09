using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities
{
    public class UsersGroup : MasterModel
    {
        [Key]
        public int UsersGroupId { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public virtual Groups Group { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
