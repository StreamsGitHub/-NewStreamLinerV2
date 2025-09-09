using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StreamLinerEntitiesLayer.Entities
{
    public class Document : MasterModel
    {
        [Key]
        public int DocumentId { get; set; }

        public string DocumentName { get; set; }
        public string? Subject { get; set; }
        public DateTime DocumentDate { get; set; } = DateTime.Now;
        public statusDocument status { get; set; }
        public bool IsActive { get; set; }
        public string ContentType { get; set; }
        public decimal Size { get; set; }
        public bool IsmetaData { get; set; }
        public bool IsSigned { get; set; } = false;
        public string? url { get; set; }
        public int? DocumentSerial { get; set; }
        public string? FinalSerial { get; set; }

        public int OwnerId { get; set; }


        [ForeignKey("folder")]
        public int FolderId { get; set; }
        public virtual Folder? folder { get; set; }
         

        [ForeignKey("DocumentTypes")]
        public int DocumentTypeId { get; set; }
        public virtual FileFormat? DocumentType { get; set; }


    }

    public enum statusDocument
    {
        ScanSingle = 1,
        ScanBulik = 2,
        Upload = 3,
        CreateFile = 4,
        BulikUpload = 5,
        Uploaded_from_office = 6

    }
}