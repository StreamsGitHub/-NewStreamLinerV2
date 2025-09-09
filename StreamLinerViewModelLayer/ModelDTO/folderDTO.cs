using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class folderDTO
    {
        public string Name { get; set; }
        public int ParentId { get; set; }
        public int FolderId { get; set; }
        public string? FolderPath { get; set; }
        public int OwnerId { get; set; }
    }
}
