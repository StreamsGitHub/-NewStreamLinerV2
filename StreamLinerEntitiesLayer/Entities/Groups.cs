using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities
{
    public class Groups: MasterModel
    {
        [Key]

        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }

        #region
        public virtual ICollection<UsersGroup> UsersGroup { get; set; }
        #endregion
    }
}
