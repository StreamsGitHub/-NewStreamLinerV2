using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamLinerViewModelLayer.ModelDTO    ;
using StreamLinerEntitiesLayer.Entities;

namespace StreamLinerLogicLayer.Helper.LicenseServices
{
    public interface ILicenseService
    {
        // Request File License
        Task<bool> RequestFileLicense(string LicenseKey);

        // Upload License File
        Task<LicenseRequest> UploadLicenseFile(string filename, string LicenseKey);

        // Insert File Record into Database
        Task<bool> InsertFileRecord(Strlcs model);

        // Update File Record in Database
        Task<bool> UpdateFileRecord(string LicenseKey);

        // Update Quota in Database
        Task<bool> UpdateQuota(Strlcs model);

        // Check Product Information
        Task<bool> CheckLicenseInformation(string LicenseKey);

        // Check Customer Information
        //Task<bool> CheckCustomerInformation(AdminViewModel model);

        // Check License Quota
        Task<bool> CheckLicenseQuota(AdminViewModel model);


        // Check License Expiration Date

        Task<bool> CheckLicenseExpirationDate(string LicenseKey);
        //Check License Users
        Task<bool> CheckLicenseUsers(string licenseKey, string roleName);



        // Task<bool> CheckLicenseExpiration(AdminViewModel model);
        // Check Status
        Task<bool> CheckLicenseStatus(string LicenseKey);
        //Get Last key License in database 
        Task<string?> GetLastLicenseKeyAsync();
        Task<List<Strlcs>> GetAll();
    }
}
