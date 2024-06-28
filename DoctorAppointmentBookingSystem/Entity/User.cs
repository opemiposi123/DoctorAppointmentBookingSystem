using DoctorAppointmentBookingSystem.Enum;
using Microsoft.AspNetCore.Identity;

namespace DoctorAppointmentBookingSystem.Entity 
{
    public class User : IdentityUser
    {
        public UserRole UserRole { get; set; }
        public required string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; } 
        public string Adress { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
      
    }
}
