using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StreamLinerDataLayer.Data;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerViewModelLayer.ModelDTO;
using System.Net.Http.Json;
using System.Text.Json;

namespace StreamLinerLogicLayer.Helper.LicenseServices
{
    public class LicenseService : ILicenseService
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        //private readonly IWebHostEnvironment _env;
        public LicenseService(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext )
        {
            _httpClient = new HttpClient();
            _userManager = userManager;
            _dbContext = dbContext;
           // _env = env;
            //   _productManager = productManager;
        }

        // Request File License
        public async Task<bool> RequestFileLicense(string LicenseKey)
        {
            if (string.IsNullOrWhiteSpace(LicenseKey))
                return false; // No license key provided
            string apiUrl = ProductManager.LicUrl + LicenseKey;

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                    return false; // API call failed

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"License info: {content}");

                // Deserialize API response into AdminViewModel
                var licenseInfo = JsonSerializer.Deserialize<AdminViewModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (licenseInfo == null)
                    return false; // Could not parse response

                // Ensure directory exists
                string downloadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "licenses");
                //if (!Directory.Exists(downloadsPath))
                //    Directory.CreateDirectory(downloadsPath);

                // Save license file
                string filePath = Path.Combine(downloadsPath, licenseInfo.LicenseFileName);
                byte[] fileBytes = Convert.FromBase64String(licenseInfo.LicenseFileContentBase64);
                await File.WriteAllBytesAsync(filePath, fileBytes);

                Console.WriteLine("✅ License downloaded successfully.");
                //IFormFile licenseFormFile;
                //using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                //{
                //    licenseFormFile = new Microsoft.AspNetCore.Http.Internal.FormFile(stream, 0, stream.Length, "licenseFile", licenseInfo.LicenseFileName);
                //}

                // 
                // UploadLicenseFile ( filePath , LicenseKey)
                var licenserequest = await UploadLicenseFile(licenseInfo.LicenseFileName, LicenseKey);
                if (licenseInfo == null)
                {
                    return false;
                }
                //save license file to database 
                try
                {
                    // Map LicenseRequest to Strlcs before calling InsertFileRecord
                    var strlcsModel = new Strlcs
                    {
                        ExpirationDate = DateTime.Parse(licenserequest.LicenseExpirationDate),
                        CreationDate = DateTime.Parse(licenserequest.LicenseCreationDate),
                        LicenseKey = LicenseKey,
                        FileName = licenseInfo.LicenseFileName,
                        FilePath = licenserequest.FilePath,

                        NumberOfAdmins = licenserequest.NumberOfAdmins,
                        NumberOfUsers = licenserequest.NumberOfUsers,
                        NumberOfParticipants = licenserequest.NumberOfParticipants,

                        LicenseStatus = "Active",
                        MaxQuote = licenserequest.MaxQuote
                    };

                    var isInserted = await InsertFileRecord(strlcsModel);
                    // return RedirectToAction("Login", "Account");
                    return true; // Success
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error processing license info: {ex.Message}");
                    return false; // Failure
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error requesting license: {ex.Message}");
                return false; // Failure
            }
        }
        // Upload License File     Path File
        public async Task<LicenseRequest> UploadLicenseFile(string filename, string LicenseKey)
        {
            if (filename == null || filename.Length == 0)
                throw new ArgumentException("Please upload a valid license file.");

            if (string.IsNullOrWhiteSpace(LicenseKey))
                throw new ArgumentException("Please enter the license key.");

            // Optional: Save license locally
            var licensePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "licenses" , filename);
            //if (!System.IO.File.Exists(licensePath))
            //    throw new FileNotFoundException("License file not found on disk.");
            // === Save uploaded file in wwwroot / licenses

            //var uploadsFolder = Path.Combine(_env.WebRootPath, "licenses");

            // Ensure directory exists
            //if (!Directory.Exists(licensePath))
            //    Directory.CreateDirectory(licensePath);

            // Full file path
            //var filePath = Path.Combine(licensePath, filename);

            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    await licenseFile.CopyToAsync(stream);
            //}
            // Read file contents
            //string licenseText;
            //using (var reader = new StreamReader(licenseFile.OpenReadStream()))
            //{
            //    licenseText = await reader.ReadToEndAsync();
            //}
            string licenseText = await System.IO.File.ReadAllTextAsync(licensePath);

            // Deserialize JSON to validate if needed
            //var json = JsonSerializer.Deserialize<Dictionary<string, string>>(licenseText);
            Dictionary<string, string>? json;
            try
            {
                json = JsonSerializer.Deserialize<Dictionary<string, string>>(licenseText);
            }
            catch (JsonException)
            {
                throw new ApplicationException("Invalid license file format.");
            }


            // Call external API
            string apiUrl = $"http://localhost:5118/api/subScription/LicenseInfo?id={LicenseKey}";
            using var content = new MultipartFormDataContent();

            // Add file
            //using var fileStream = licenseFile.OpenReadStream();
            //var fileContent = new StreamContent(fileStream);
            //fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            //content.Add(fileContent, "licenseFile",filename);

            // Add license key
            content.Add(new StringContent(LicenseKey), "licenseKey");

            // Send request
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"API Error: {error}");
            }
            // Deserialize response into LicenseRequest
            var licenseResponse = await response.Content.ReadFromJsonAsync<LicenseRequest>();

            if (licenseResponse == null)
                throw new ApplicationException("Failed to parse license information from API.");
            licenseResponse.FilePath = licensePath;
            return licenseResponse;
        }

        // Insert File Record into Database
        public async Task<bool> InsertFileRecord(Strlcs model)
        {
            // 1. Validate input model
            if (model == null || string.IsNullOrWhiteSpace(model.LicenseKey))
                return false;

            // 2. Check if a record with the same LicenseKey already exists
            var existing = _dbContext.Strlcs.FirstOrDefault(l => l.LicenseKey == model.LicenseKey);
            if (existing != null)
                return false; // Prevent duplicate license records

            // 3. Add the new license record
            await _dbContext.Strlcs.AddAsync(model);

            // 4. Save changes to the database
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Update File Record in Database
        public async Task<bool> UpdateFileRecord(string LicenseKey)
        {
            if (string.IsNullOrWhiteSpace(LicenseKey))
                return false;
            //Get All data from api 


            string apiUrl = $"http://localhost:5118/api/subScription/LicenseInfo?id={LicenseKey}";
            var response = await _httpClient.PostAsync(apiUrl, null);

            response.EnsureSuccessStatusCode();

            var license = await response.Content.ReadFromJsonAsync<LicenseRequest>();

            // Find the existing license record by LicenseKey
            var existingRecord = _dbContext.Strlcs.FirstOrDefault(l => l.LicenseKey == LicenseKey);
            if (existingRecord == null)
                return false;

            // Update properties
            existingRecord.FileName = "";
            existingRecord.FilePath = "";
            existingRecord.ExpirationDate = DateTime.Parse(license.LicenseExpirationDate);
            existingRecord.CreationDate = DateTime.Parse(license.LicenseCreationDate);
            existingRecord.NumberOfAdmins = license.NumberOfAdmins;
            existingRecord.NumberOfUsers = license.NumberOfUsers;
            existingRecord.NumberOfParticipants = license.NumberOfParticipants;
            existingRecord.LicenseStatus = license.LicenseStatus;
            existingRecord.MaxQuote = license.MaxQuote;

            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Update Quota in Database
        public async Task<bool> UpdateQuota(Strlcs model)
        {
            // 1. Validate input model.
            if (model == null || string.IsNullOrWhiteSpace(model.LicenseKey))
                return false;

            // 2. Retrieve the license record from the database using model.LicenseKey.
            var licenseRecord = _dbContext.Strlcs.FirstOrDefault(l => l.LicenseKey == model.LicenseKey);

            // 3. If found, update the MaxQuote property with model.MaxQuote.
            if (licenseRecord != null)
            {
                licenseRecord.MaxQuote = model.MaxQuote;

                // 4. Save changes to the database.
                try
                {
                    await _dbContext.SaveChangesAsync();
                    // 5. Return true if update is successful
                    return true;
                }
                catch
                {
                    // Return false if save fails
                    return false;
                }
            }

            // 5. Return false if license not found
            return false;
        }

        // Check Product Information
        public async Task<bool> CheckLicenseInformation(string LicenseKey)
        {
            //// 1. Validate input
            //if (string.IsNullOrWhiteSpace(LicenseKey))
            //    return false;

            //// 2. Build API URL from ProductManager
            //string apiUrl = $"http://localhost:5118/api/subScription/LicenseInfo?id={LicenseKey}";

            //try
            //{
            //    // 3. Call API
            //    var response = await _httpClient.PostAsync(apiUrl);
            //    if (!response.IsSuccessStatusCode)
            //        return false;

            //    var content = await response.Content.ReadAsStringAsync();

            //    // 4. Deserialize JSON into LicenseRequest
            //    var licenseInfo = JsonSerializer.Deserialize<LicenseRequest>(content,
            //        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            //    if (licenseInfo == null)
            //        return false;

            //    // 5. Compare with expected values in ProductManager
            //    bool isValid =
            //        licenseInfo.ProductId == ProductManager.ProductId &&
            //        licenseInfo.ProductName == ProductManager.ProductName &&
            //        licenseInfo.ProductType == ProductManager.ProductType &&
            //        licenseInfo.Version == ProductManager.Version;
            //    licenseInfo.CompanyId = ProductManager.CompanyId;
            //    licenseInfo.CompanyName = ProductManager.CompanyName;


            //    return isValid;
            //}
            //catch
            //{
            //    // API call failed or data was invalid
            //    return false;
            //}
            if (string.IsNullOrWhiteSpace(LicenseKey))
                return false;

            // URL مع الـ Query string
            string apiUrl = $"http://localhost:5118/api/subScription/LicenseInfo?id={LicenseKey}";

            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);


                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return false;

                var content = await response.Content.ReadAsStringAsync();

                var licenseInfo = JsonSerializer.Deserialize<LicenseRequest>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (licenseInfo == null)
                    return false;

                bool isValid =
                    licenseInfo.ProductId == ProductManager.ProductId &&
                    licenseInfo.ProductName == ProductManager.ProductName &&
                    licenseInfo.ProductType == ProductManager.ProductType &&
                    licenseInfo.Version == ProductManager.Version &&
                    licenseInfo.CompanyId == ProductManager.CompanyId &&
                    licenseInfo.CompanyName == ProductManager.CompanyName;

                return isValid;
            }
            catch
            {
                return false;
            }



        }

        // Check Customer Information
        //public async Task<bool> CheckCustomerInformation(AdminViewModel model)
        //{
        //    // 1. Validate input
        //    if (model == null || string.IsNullOrWhiteSpace(model.LicenseKey))
        //        return false;

        //    // 2. Build API URL to get license info
        //    string apiUrl = $"http://localhost:5118/api/subScription/LicenseInfo?id={model.LicenseKey}";

        //    try
        //    {
        //        // 3. Call API
        //        var licenseInfo = await _httpClient.GetFromJsonAsync<LicenseRequest>(apiUrl);
        //        if (licenseInfo == null)
        //            return false;

        //        // 4. Check required customer information
        //        bool hasCompanyId = licenseInfo.CompanyId > 0;
        //        bool hasCompanyName = !string.IsNullOrWhiteSpace(licenseInfo.CompanyName);

        //        // 5. Return true if both are present
        //        return hasCompanyId && hasCompanyName;
        //    }
        //    catch
        //    {
        //        // API call failed or data was invalid
        //        return false;
        //    }
        //}


        // Check License Expiration Date 
        public async Task<bool> CheckLicenseExpirationDate(string LicenseKey)
        {
            // Check License Expiration Date


            var license = _dbContext.Strlcs.Where(x => x.LicenseKey == LicenseKey).OrderByDescending(l => l.Id).FirstOrDefault();
            if (license == null) return false;



            // Check if the license is expired
            if (license.ExpirationDate < DateTime.Now)
            {
                return false;
            }
            return true;
        }

        // Check License Quota
        public async Task<bool> CheckLicenseQuota(AdminViewModel model)
        {
            //Check License Number of quota
            string apiUrl = ProductManager.LicUrl + model.LicenseKey;

            var license = _httpClient.GetFromJsonAsync<LicenseRequest>(apiUrl).Result;
            if (license == null) return false;
            // Check if the license is expired
            if (model.UsedQuote > license.MaxQuote)
            {
                return false;
            }
            return true;

        }
        // Check License Status
        public async Task<bool> CheckLicenseStatus(string LicenseKey)
        {
            //check Status of the license
            //string apiUrl = $"http://localhost:5118/api/subScription/GetLicenseByKey?id={model.LicenseKey}";
            string apiUrl = ProductManager.LicUrl + LicenseKey;
            var license = await _httpClient.GetFromJsonAsync<LicenseRequest>(apiUrl);
            if (license == null) return false;
            // Check if the license is active
            if (license.LicenseStatus != "Active")
            {
                return false;
            }
            return true;
        }
        // Check License Users
        public async Task<bool> CheckLicenseUsers(string licenseKey, string roleName)
        {


            // 1. Get license record by key

            var license = _dbContext.Strlcs.FirstOrDefault(l => l.LicenseKey == licenseKey);

            if (license == null)
                return false;

            // 2. Count active users in the given role
            var allActiveUsers = _userManager.Users
                .Where(u => u.IsDeleted)
                .ToList();

            int activeRoleCount = 0;
            foreach (var user in allActiveUsers)
            {
                if (await _userManager.IsInRoleAsync(user, roleName))
                {
                    activeRoleCount++;
                }
            }

            // 3. Compare count with correct license quota
            if (roleName.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                return activeRoleCount < license.NumberOfAdmins;

            if (roleName.Equals("User", StringComparison.OrdinalIgnoreCase))
                return activeRoleCount < license.NumberOfUsers;
            if (roleName.Equals("Participate", StringComparison.OrdinalIgnoreCase))
                return activeRoleCount < license.NumberOfParticipants;


            return false;
        }

        //Get Last License key in database 
        public async Task<string?> GetLastLicenseKeyAsync()
        {
            return _dbContext.Strlcs
                .OrderByDescending(l => l.Id)
                .Select(l => l.LicenseKey)
                .FirstOrDefault();
        }
        public async Task<List<Strlcs>> GetAll()
        {
            return await _dbContext.Strlcs.ToListAsync();
        }
    }
}
