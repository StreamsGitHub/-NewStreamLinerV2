using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities
{
    public class FileFormat : MasterModel
    {
        [Key]
        public int FormatId { get; set; }
        public Format format { get; set; } = 0;
        public string? Extension { get; set; }
        public string? Icon { get; set; } = "fa fa-file-text-o";
        public string? Category { get; set; }

        public enum Format
        {
            Unknown = 0,
            Word = 1,
            Excel = 2,
            PowerPoint = 3,
            PDF = 4,
            Image = 5,
            Text = 6,
            Archive = 7,
            Other = 8,
            Video = 9,
            Audio = 10,
            Code = 11,
            Database = 12,
            Script = 13,
            Compressed = 14,
            Executable = 15,
            Configuration = 16,
            Font = 17,
            Backup = 18,

        }
    }
}
