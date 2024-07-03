using DoctorAppointmentBookingSystem.Entity;
using DoctorAppointmentBookingSystem.Enum;
using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentBookingSystem.Dto
{

    public class DoctorDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string? Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Guid DepartmentId { get; set; } 
        public string? DepartmentName { get; set; }
        public List<DoctorSchedule> DoctorSchedules { get; set; } 
        public List<Appointment> Appointments { get; set; } 

    }

}
