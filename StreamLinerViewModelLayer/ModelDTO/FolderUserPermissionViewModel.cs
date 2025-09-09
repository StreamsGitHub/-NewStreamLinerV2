using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class FolderUserPermissionViewModel
    {
        public int Id { get; set; }
        public int FolderId { get; set; } 
        public int UserId { get; set; } 
        public int FolderPermissionId { get; set; }
        
        public string? Username { get; set; }
        public string? Permission { get; set; }
        public string? FolderName { get; set; }

    }
}
