using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities
{
    public class ProductManager
    {
        public const string MachineId = ""; // Machine ID for license binding
        public const string LicUrl = $"http://localhost:5118/api/subScription/GetLicenseByKey?id=";


        // Product Information
        public const int ProductId = 2; // Product ID associated with the license
        public const string ProductName = "StreamLiner"; // Name of the product
        public const string ProductType = "CMS"; // Type of the product (e.g., software, service)
        public const string Version = "V 2.0"; // Version of the product

        // Customer Information
        public const int CompanyId = 2; // Company ID associated with the license
        public const string CompanyName = "Streams Company"; // Name of the company
    }
}
