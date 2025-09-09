using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class UpdateRepositoryDto
    {
        public int RepositoryId { get; set; }
        public string RepositoryName { get; set; }
        public string Description { get; set; }
        public int SizeInMB { get; set; }
        public string License { get; set; }
        public int OwnerId { get; set; }
        public bool IsPrivate { get; set; }
    }
}
