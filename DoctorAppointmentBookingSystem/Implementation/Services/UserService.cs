using DoctorAppointmentBookingSystem.Implementation.Interface;
using DoctorAppointmentBookingSystem.Context;
using DoctorAppointmentBookingSystem.Entity;
using DoctorAppointmentBookingSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace DoctorAppointmentBookingSystem.Implementation.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(ApplicationDbContext context,
                          UserManager<User> userManager,
                          SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Status> Login(LoginModel login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);
            if (user == null)
            {
                return new Status { Success = false, Message = "User not found" };
            }

            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);
            if (result.Succeeded)
            {
                return new Status { Success = true, Message = "Login successful" };
            }
            else
            {
                return new Status { Success = false, Message = "Invalid username or password" };
            }
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return new Status { Success = false, Message = "User not found" };
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return new Status { Success = true, Message = "Password changed successfully" };
            }
            else
            {
                return new Status { Success = false, Message = "Error changing password"};
            }
        }
    }
}
