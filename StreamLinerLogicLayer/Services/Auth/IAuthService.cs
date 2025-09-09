
using StreamLinerEntitiesLayer.Entities;
using StreamLinerViewModelLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.Auth
{
    public interface IAuthService
    {
        Task<bool> AdminUpdateUser(AdminAddUserDto model);
        Task<bool> AdminAddUser(AdminAddUserDto user);
        Task<bool> LoginAsync(LoginViewModel model);
        Task<bool> LogoutAsync();
        Task<List<UsersDtoResult>> GetAllUsersAsync();
        Task<OperationResult> RegisterAsync(RegisterViewModel model);
        // Task<IdentityResult> EditUserAsync(EditUserViewModel model);
        Task<OperationResult> ChangePasswordAsync(ChangePasswordRequest request);
        Task<OperationResult> ResetPasswordAsync(ResetPasswordRequest request);
        Task<OperationResult> ForgotPasswordAsync(ForgotPasswordRequest request);

        Task<ApplicationUser> AddUser(userDto model);

        Task<string> GeneratePasswordAsync(int length = 12);



    }
}
