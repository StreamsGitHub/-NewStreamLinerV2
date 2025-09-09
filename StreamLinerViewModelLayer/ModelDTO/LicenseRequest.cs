using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class LicenseRequest
    {
        // Date
        public string LicenseExpirationDate { get; set; }
        public string LicenseCreationDate { get; set; }

        // Product Info
        public string MachineId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string Version { get; set; }

        // Customer Info
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }


        // Users
        public int NumberOfAdmins { get; set; }

        public int NumberOfUsers { get; set; }
        public int NumberOfParticipants { get; set; }

        // License Info
        public string Description { get; set; }
        public string LicenseStatus { get; set; }
        public int MaxQuote { get; set; }
        public string FilePath { get; set; }
    }
}
