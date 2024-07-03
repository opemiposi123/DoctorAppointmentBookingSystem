using DoctorAppointmentBookingSystem.Enum;
using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentBookingSystem.Dto
{
    public class PatientDto
    {
        public Guid Id { get; set; } 
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; } 
        public  string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public string? MedicalHistory { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactEmail { get; set; }
        public string? EmergencyContactRelationship { get; set; }
    }
}
