using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities.IEntity
{
    public interface IBaseEntity
    {
        [Key]
        int Id { get; set; }
        
        // string CreatedBy { get; set; }

        // DateTime CreatedOn { get; set; }

        // string? UpdatedBy { get; set; }

        //  DateTime? UpdatedOn { get; set; }

        bool IsDeleted { get; set; }
    }
}
