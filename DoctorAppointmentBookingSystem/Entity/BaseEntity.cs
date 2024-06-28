namespace DoctorAppointmentBookingSystem.Entity
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string? CreatedBy { get; set;}
        public string?  ModifiedBy{ get; set;}
        public bool IsDeleted { get; set; } = false;
    }
}
