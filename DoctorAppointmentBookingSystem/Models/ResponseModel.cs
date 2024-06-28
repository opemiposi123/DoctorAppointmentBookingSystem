namespace DoctorAppointmentBookingSystem.Models
{
    public class ResponseModel<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public ResponseModel()
        {
            Success = false;
            Errors = new List<string>();
        }
    }

}
