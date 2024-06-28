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
        [Required(ErrorMessage = "StartTimeAndDate is required")]
        public TimeSpan StartTimeAndDate { get; set; }
        [Required(ErrorMessage = "EndTimeAndDate is required")]
        public TimeSpan EndTimeAndDate { get; set; }
        public DayOfWeek DayOfWeek { get; set; } 
        public Guid DoctorId { get; set; }
        public string DoctorFullName { get; set; } 
    }
}
