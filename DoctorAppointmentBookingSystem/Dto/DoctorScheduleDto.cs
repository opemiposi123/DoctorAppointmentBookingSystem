using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentBookingSystem.Dto
{
    public class DoctorScheduleDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DayOfWeek DayOfTheWeek { get; set; } 
        public Guid DoctorId { get; set; }
        public string DoctorFullName { get; set; } 
    }
}
