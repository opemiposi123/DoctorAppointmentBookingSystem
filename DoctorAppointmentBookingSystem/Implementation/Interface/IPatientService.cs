using DoctorAppointmentBookingSystem.Dto;
using DoctorAppointmentBookingSystem.Entity;
using DoctorAppointmentBookingSystem.Models;

namespace DoctorAppointmentBookingSystem.Implementation.Interface
{
    public interface IPatientService
    {
        Task<ResponseModel<PatientDto>> CreatePatient(PatientDto createPatient);
        Task<ResponseModel<PatientDto>> EditPatient(PatientDto editPatient);
        Task<ResponseModel<bool>> DeletePatient(Guid Id);
        Task<List<PatientDto>> GetPatientList();
        Task<PatientDto> GetPatientDetail(Guid Id);
        Patient GetPatientById(Guid id);
        Task<Status> PatientLogin(LoginModel login);

    }
}
 