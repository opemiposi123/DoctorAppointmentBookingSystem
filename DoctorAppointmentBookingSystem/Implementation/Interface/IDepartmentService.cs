using DoctorAppointmentBookingSystem.Dto;
using DoctorAppointmentBookingSystem.Entity;
using DoctorAppointmentBookingSystem.Models;

namespace DoctorAppointmentBookingSystem.Implementation.Interface
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetDepartmentList();
        Department GetDepartmentById(Guid id);
        Task<DepartmentDto> GetDepartmentDetail(Guid id); 
        Task<ResponseModel<DepartmentDto>> CreateDepartment(DepartmentDto createdepartment);
        Task<ResponseModel<DepartmentDto>> EditDepartment(DepartmentDto editdepartment);
        Task<ResponseModel<bool>> DeleteDepartment(Guid Id);
       
    }
}
