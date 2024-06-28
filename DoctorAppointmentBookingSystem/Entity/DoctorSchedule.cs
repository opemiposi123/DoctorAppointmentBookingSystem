using DoctorAppointmentBookingSystem.Enum;

namespace DoctorAppointmentBookingSystem.Entity
{
    public class DoctorSchedule : BaseEntity
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DayOfTheweek DayOfTheWeek { get; set; }
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
