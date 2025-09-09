using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class getDocumentsDTO
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public bool IsActive { get; set; }
        public string ContentType { get; set; }
        public decimal Size { get; set; }
        public bool IsmetaData { get; set; }
        public bool IsSigned { get; set; } = false;
        public string? url { get; set; }

    }
}
