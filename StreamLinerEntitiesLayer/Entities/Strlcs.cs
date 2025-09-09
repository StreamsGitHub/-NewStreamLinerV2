using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities
{
    public class Strlcs
    {
        [Key]
        public int Id { get; set; }
        public string LicenseKey { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreationDate { get; set; }

        public int NumberOfAdmins { get; set; }
        public int NumberOfUsers { get; set; }
        public int NumberOfParticipants { get; set; }

        public string LicenseStatus { get; set; }
        public int MaxQuote { get; set; }
    }
}
