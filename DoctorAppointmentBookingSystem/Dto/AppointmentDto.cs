using DoctorAppointmentBookingSystem.Enum;
using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentBookingSystem.Dto
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
         public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public string PatientName { get; set; } 
        public string DoctorName { get; set; } 
        public DateTime DateAndTime { get; set; }
        [Required(ErrorMessage = "ReasonForVisit is required")]
        public required string ReasonForVisit { get; set; }
        public AppointmentStatus Status { get; set; }
        public AppointmentType AppointmentType { get; set; }
    }
}
