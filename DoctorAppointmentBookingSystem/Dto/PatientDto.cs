using DoctorAppointmentBookingSystem.Enum;
using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentBookingSystem.Dto
{
    public class PatientDto
    {
        public Guid Id { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; } = default!;
        [Required(ErrorMessage = "Email is required")]
        public  string Email { get; set; } = default!;
        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; } = default!;
        [Required(ErrorMessage = "Adress is required")]
        public string Adress { get; set; }
        public string? MedicalHistory { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactEmail { get; set; }
        public string? EmergencyContactRelationship { get; set; }
    }
}
