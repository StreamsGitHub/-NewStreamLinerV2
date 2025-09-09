using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerLogicLayer.Helper.EmailService;
using StreamLinerViewModelLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
              IMapper mapper,
            IConfiguration configuration,
           IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _mapper = mapper;
        }




        public Task<bool> AdminAddUser(AdminAddUserDto user)
        {
            // Implementation for adding a user
            throw new NotImplementedException();
        }
        public Task<bool> AdminUpdateUser(AdminAddUserDto model)
        {
            // Implementation for updating a user
            throw new NotImplementedException();
        }


        public async Task<OperationResult> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return OperationResult.Fail("User not found.");

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded)
                return OperationResult.Fail(result.Errors.Select(e => e.Description).ToArray());

            return OperationResult.Ok("Password changed successfully.");
        }


        public async Task<OperationResult> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return OperationResult.Fail("User not found.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // TODO: send token via email
            // e.g. _emailService.SendPasswordResetLink(user.Email, token)

            return OperationResult.Ok("Password reset token has been generated.");
        }

        //public async Task<List<UsersDtoResult>> GetAllUsersAsync()
        //{
        //    var users = _userManager.Users;

        //    //var Fieldslist = _mapper.Map<List<FieldDTO>>(Fields);
        //    var usersdto = _mapper.Map<List<UsersDtoResult>>(users);

        //    return usersdto;

        //}

        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return false;

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, model.RememberMe);
            if (!result.Succeeded)
                return false;

            // sign in the user
            await _signInManager.SignInAsync(user, model.RememberMe);


            //if (!await _userManager.IsEmailConfirmedAsync(user))
            //    return false;

            return true;
        }

        public async Task<bool> LogoutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<OperationResult> RegisterAsync(RegisterViewModel model)
        {

            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return Task.FromResult(OperationResult.Fail("Email and password are required."));
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                ACC = true,
                BPM = true,
                CRM = true,
                DMS = true,
                HRM = true,
                Oper = true,
                CMS = true,
                SlS = true,
                PRC = true,
                INV = true,
                FirstName = "Joseph",
                LastName = "Bishara",
                PhoneNumber = "012",
                Address = "123 Main",
                BirthDate = DateTime.Now,
                Volumequota = 500,
                Maxquota = 1000,
                DepartmentId = 1,
                JobTitle = "Software Engineer",
                



            };
            var result = _userManager.CreateAsync(user, model.Password).Result;
            if (!result.Succeeded)
                return Task.FromResult(OperationResult.Fail(result.Errors.Select(e => e.Description).ToArray()));
            // Optionally send confirmation email here
            // await _emailSender.SendEmailConfirmationAsync(user);
            return Task.FromResult(OperationResult.Ok("User registered successfully."));
        }

        public async Task<OperationResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return OperationResult.Fail("User not found.");

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            if (!result.Succeeded)
                return OperationResult.Fail(result.Errors.Select(e => e.Description).ToArray());

            return OperationResult.Ok("Password has been reset.");
        }

        public async Task<List<UsersDtoResult>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();

            if (users == null)
            {
                throw new Exception("Entity not found");
            }
            var usersdto = _mapper.Map<List<UsersDtoResult>>(users);

            return usersdto;
        }

        public async Task<ApplicationUser> AddUser(userDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            // Map DTO to Entity
            var newUser = new ApplicationUser
            {
                UserName = model.EmailAddress,
                Email = model.EmailAddress,
                FullName = $"{model.FirstName} {model.LastName}",
                DepartmentId = model.DepartmentId,
                JobTitle = model.JobTitle,
                BirthDate = model.BirthDate,
                Volumequota = 0,
                Maxquota = model.Maxquota,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                ProfileImage = "",

            };
            var result = await _userManager.CreateAsync(newUser, model.pwd);
            if (result.Succeeded)
            {

                // ✅ Assign Role (ensure role exists first)
                if (!string.IsNullOrWhiteSpace(model.Role))
                {
                    var roleExists = await _roleManager.RoleExistsAsync(model.Role);
                    if (!roleExists)
                    {
                        throw new InvalidOperationException($"Role '{model.Role}' does not exist.");
                    }

                    var roleResult = await _userManager.AddToRoleAsync(newUser, model.Role);
                    if (!roleResult.Succeeded)
                    {
                        var roleErrors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                        throw new InvalidOperationException($"Failed to assign role: {roleErrors}");
                    }
                }

                // Generate email confirmation token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                // Create confirmation link
                var confirmationLink = $"{_configuration["App:ClientBaseUrl"]}/confirm-email?email={Uri.EscapeDataString(model.EmailAddress)}&token={Uri.EscapeDataString(token)}";
                // Send confirmation email
                await _emailSender.SendEmailAsync(model.EmailAddress, "Confirm your email",
              $"Please confirm your email by clicking this link: <a href='{confirmationLink}'>Confirm Email</a>");

                return newUser;
            }
            else if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"User creation failed: {errors}");
            }

            return newUser;
        }

        public Task<string> GeneratePasswordAsync(int length = 12)
        {
            if (length < 8)
                throw new ArgumentException("Password length must be at least 8 characters.");

            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%^&*()-_=+[]{};:,.<>?";

            string allChars = upper + lower + digits + special;

            // Ensure at least one of each category
            char[] password = new char[length];
            var random = RandomNumberGenerator.Create();

            password[0] = GetRandomChar(upper, random);
            password[1] = GetRandomChar(lower, random);
            password[2] = GetRandomChar(digits, random);
            password[3] = GetRandomChar(special, random);

            for (int i = 4; i < length; i++)
            {
                password[i] = GetRandomChar(allChars, random);
            }
            string pwd = new string(password.OrderBy(_ => Guid.NewGuid()).ToArray());
            // Shuffle to remove predictable character positions
            return Task.FromResult(pwd);
        }

        private static char GetRandomChar(string chars, RandomNumberGenerator rng)
        {
            byte[] buffer = new byte[1];
            char resultChar;
            do
            {
                rng.GetBytes(buffer);
                resultChar = chars[buffer[0] % chars.Length];
            }
            while (!chars.Contains(resultChar));

            return resultChar;
        }
    }
}
