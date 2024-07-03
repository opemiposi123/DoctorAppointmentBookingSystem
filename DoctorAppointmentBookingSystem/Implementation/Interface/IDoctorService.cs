using DoctorAppointmentBookingSystem.Dto;
using DoctorAppointmentBookingSystem.Entity;
using DoctorAppointmentBookingSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoctorAppointmentBookingSystem.Implementation.Interface
{
    public interface IDoctorService
    {
        Task<ResponseModel<DoctorDto>> CreateDoctor(DoctorDto createDoctor);
        Task<ResponseModel<DoctorDto>> EditDoctor(DoctorDto editDoctor);
        Task<ResponseModel<DoctorDto>> DeleteDoctor(Guid Id);
        Task<List<DoctorDto>> GetDoctorList();

        Task<DoctorDto> GetDoctorDetail(Guid Id);
        Doctor GetDoctorById(Guid id); 
        Task<Status> DoctorLogin(LoginModel login);
        Task<IEnumerable<SelectListItem>> GetDoctorSelectList();
    }
}
