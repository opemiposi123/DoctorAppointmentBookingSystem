namespace DoctorAppointmentBookingSystem.Entity
{
    public class Department : BaseEntity
    {
        public required string Name { get; set; }    
        public string? Description { get; set; }
    }
}
