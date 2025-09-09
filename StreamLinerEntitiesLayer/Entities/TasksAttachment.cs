using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StreamLinerEntitiesLayer.Entities
{
    public class TasksAttachment : MasterModel
    {
        [Key]
        public int TasksAttachmentId { get; set; }
        [Required]
        public string FileName { get; set; }
        
        
        [ForeignKey("FileType")]
        public int? FileTypeId { get; set; }
        public virtual FileFormat? FileType { get; set; }


        public decimal FileSize { get; set; } = 0;
        public string? url { get; set; } 

        [ForeignKey("Tasks")]
        public int? TaskId { get; set; }
        public virtual TaskList Tasks { get; set; }
    }
}