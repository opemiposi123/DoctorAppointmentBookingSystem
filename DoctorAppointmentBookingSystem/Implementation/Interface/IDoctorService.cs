using DoctorAppointmentBookingSystem.Dto;
using DoctorAppointmentBookingSystem.Entity;
using DoctorAppointmentBookingSystem.Models;

namespace DoctorAppointmentBookingSystem.Implementation.Interface
{
    public interface IDoctorService
    {
        Task<ResponseModel<DoctorDto>> CreateDoctor(DoctorDto createDoctor);
        Task<ResponseModel<DoctorDto>> EditDoctor(DoctorDto editDoctor);
        Task<ResponseModel<DoctorDto>> DeleteDoctor(Guid Id);
        Task<List<DoctorDto>> GetDoctorList();
        Task<List<DoctorDto>> GetDoctorListByDepartment(Guid departmentId); 
        Task<DoctorDto> GetDoctorDetail(Guid Id);
        Doctor GetDoctorById(Guid id);
        Task<Status> DoctorLogin(LoginModel login);

    }
}
