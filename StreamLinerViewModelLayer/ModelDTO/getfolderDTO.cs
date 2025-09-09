using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class getfolderDTO
    {
        public int Id { get; set; }
        public string? FolderName { get; set; }
        public string? FolderPath { get; set; }
        public int Level { get; set; }
        public int ParentId { get; set; }

        public int OwnerId { get; set; }

    }
}
