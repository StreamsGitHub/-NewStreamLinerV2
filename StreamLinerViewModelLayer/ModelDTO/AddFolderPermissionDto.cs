using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class AddFolderPermissionDto
    {
        public int FolderId { get; set; }
        public int UserId { get; set; }
        public int FolderPermissionId { get; set; }
        public int CreatedBy { get; set; }
        public int CompanyId { get; set; }
    }
}
