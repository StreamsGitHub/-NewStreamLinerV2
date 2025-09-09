using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class AdminViewModel
    {
        public string? LicenseKey { get; set; }
        public string? LicenseFileName { get; set; }
        public string? LicenseFileContentBase64 { get; set; }
        public int? UsedQuote { get; set; }
    }
}
