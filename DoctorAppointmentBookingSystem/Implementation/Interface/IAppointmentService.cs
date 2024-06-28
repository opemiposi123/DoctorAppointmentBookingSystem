using DoctorAppointmentBookingSystem.Dto;
using DoctorAppointmentBookingSystem.Entity;
using DoctorAppointmentBookingSystem.Models;

namespace DoctorAppointmentBookingSystem.Implementation.Interface
{
    public interface IAppointmentService 
    {
        Task<ResponseModel<AppointmentDto>> BookAppointment(AppointmentDto bookAppointment);
        Task<ResponseModel<AppointmentDto>> ConfirmAppointment(AppointmentDto confirmAppointment);
        Task<ResponseModel<AppointmentDto>> CancellAppointment(AppointmentDto cancellAppointment); 
        Task<ResponseModel<AppointmentDto>> EditAppointment(AppointmentDto editAppointment);
        Task<ResponseModel<bool>> DeleteAppointment(Guid Id);
        Task<List<AppointmentDto>> GetAppointmentList();
        Task<List<AppointmentDto>> GetPatientAppointmentList(Guid patientId); 
        Task<List<AppointmentDto>> GetDoctorAppointmentList(Guid doctorId); 
        Task<AppointmentDto> GetAppointmentDetail(Guid Id);
        Appointment GetAppointmentById(Guid id); 
    }
}
