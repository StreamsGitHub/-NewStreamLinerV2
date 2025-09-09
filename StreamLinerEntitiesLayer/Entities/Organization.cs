using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities
{
    public class Organization : MasterModel
    {
        [Key]
        public int OrganizationId { get; set; }
        [Required]
        public string OrganizationName { get; set; }
 
        public string EmailAddress { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string phone { get; set; }
        public string Mobile { get; set; }
        public string? logo { get; set; } = string.Empty;
        public int PerantId { get; set; }
        public int level { get; set; }
    }
}
