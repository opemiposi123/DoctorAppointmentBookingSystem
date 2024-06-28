using DoctorAppointmentBookingSystem.Enum;

namespace DoctorAppointmentBookingSystem.Entity
{
    public class Appointment : BaseEntity
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public DateTime DateAndTime { get; set; }
        public required string ReasonForVisit { get; set; }
        public AppointmentStatus Status { get; set; }
        public AppointmentType AppointmentType { get; set; }
    }
}
