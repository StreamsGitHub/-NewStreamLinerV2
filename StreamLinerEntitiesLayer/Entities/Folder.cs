using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities
{
    public class Folder : MasterModel
    {

        [Key]
        public int FolderId { get; set; }
        public string? FolderName { get; set; }
        public string? FolderPath { get; set; }
        public int Level { get; set; }


     //   [ForeignKey("Owner")]
        public int OwnerId { get; set; }
       // public virtual ApplicationUser? Owner { get; set; }

     

        
        public int ParentId { get; set; }
        //[ForeignKey("SubFolders")]   
        //public ICollection<Folder>? SubFolders { get; set; }

        public ICollection<Document>? Documents { get; set; }
        public FolderStatus? status { get; set; } = FolderStatus.normal;
       
    }
    public enum FolderStatus
    {
        normal = 1,
        bulk = 2,
        shared = 3
    }
}
