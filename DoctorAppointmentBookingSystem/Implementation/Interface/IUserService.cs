using DoctorAppointmentBookingSystem.Dto;
using DoctorAppointmentBookingSystem.Entity;
using DoctorAppointmentBookingSystem.Models;

namespace DoctorAppointmentBookingSystem.Implementation.Interface
{
    public interface IUserService
    {
        Task<Status> Login(LoginModel login); 
        Task LogoutAsync();
        Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username);
    }
}
