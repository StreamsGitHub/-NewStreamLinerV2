using StreamLinerEntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class FolderPermissionDto
    {
        public bool Browse { get; set; }
        public bool Read { get; set; }
        [DisplayName("See Annotations")]
        public bool SeeAnnotations { get; set; }
        [DisplayName("Create Documents")]
        public bool CreateDocuments { get; set; }
        [DisplayName("Create Folders")]
        public bool CreateFolders { get; set; }
        [DisplayName("Append Data")]
        public bool AppendData { get; set; }
        [DisplayName("Modify Contents")]
        public bool ModifyContents { get; set; }
        public bool Rename { get; set; }
        public bool Annotate { get; set; }
        [DisplayName("Write Metadata")]
        public bool WriteMetadata { get; set; }
        [DisplayName("Delete Folder")]
        public bool DeleteFolder { get; set; }
        [DisplayName("Delete Document")]
        public bool DeleteDocument { get; set; }
        [DisplayName("Read Security")]
        public bool ReadSecurity { get; set; }
        [DisplayName("Write Security")]
        public bool WriteSecurity { get; set; }
        [DisplayName("Change Owner")]
        public bool ChangeOwner { get; set; }
    }
}
