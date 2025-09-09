using StreamLinerEntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class DirectoryViewModel
    {
        public int FolderId { get; set; }
        public string Name { get; set; }
        public string? ParentId { get; set; }
        public List<Folder>? SubDirectories { get; set; }  
        public List<getDocumentsDTO>? SubFiles { get; set; }
        public List<FolderUserPermissionViewModel>? FolderUserPermissionViewModel { get; set; }
    
    }
}
