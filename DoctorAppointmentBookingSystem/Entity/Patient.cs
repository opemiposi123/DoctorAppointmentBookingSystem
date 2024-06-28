using DoctorAppointmentBookingSystem.Enum;

namespace DoctorAppointmentBookingSystem.Entity
{
    public class Patient : BaseEntity
    {
        public UserRole UserRole { get; set; }
        public required string UserName { get; set; }
        public string Password { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; } 
        public required string PhoneNumber { get; set; }
        public required string Adress { get; set; }
        public string? MedicalHistory { get; set; } 
        public DateOnly DateOfBirth { get; set; } 
        public Gender Gender { get; set; }
        public string? EmergencyContactName { get; set; } 
        public string? EmergencyContactEmail { get; set; } 
        public string? EmergencyContactRelationship { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}
