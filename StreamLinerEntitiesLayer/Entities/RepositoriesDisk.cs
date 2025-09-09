using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities
{
    public class RepositoriesDisk : MasterModel
    {
        [Key]
        public int RepositoryId { get; set; }
        [Required]
        public string RepositoryName { get; set; }
        [Required]
        public string Description { get; set; }
        public string? License { get; set; }
        public int SizeInMB { get; set; }
        public int OwnerId { get; set; }
        public bool IsPrivate { get; set; }

    }
}
