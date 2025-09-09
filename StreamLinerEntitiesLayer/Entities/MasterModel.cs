using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities
{
    public class MasterModel
    {
        public bool Active { get; set; } = true;
        public int CreateId { get; set; } = 0;
        public DateTime? CreatedDate { get; set; }

        public bool Updated { get; set; } = false;
        public int Version { get; set; } = 0;
        public int UpdateId { get; set; } = 0;
        public DateTime? UpdatedDate { get; set; }

        public bool Deleted { get; set; } = false;
        public int DeleteId { get; set; } = 0;
        public DateTime? DeletedDate { get; set; }

        public int CompanyId { get; set; } = 0;
    }
}
