using DoctorAppointmentBookingSystem.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DoctorAppointmentBookingSystem.Entity
{
    public class Doctor : BaseEntity
    {
        public UserRole UserRole { get; set; }
        public string UserName { get; set; } 
        public string Password { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required  string PhoneNumber { get; set; }
        public string? Address { get; set; } 
        public DateTime DateOBirth { get; set; } 
        public Gender Gender { get; set; } 
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<DoctorSchedule> DoctorSchedule { get; set; } = new HashSet<DoctorSchedule>();
        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}
