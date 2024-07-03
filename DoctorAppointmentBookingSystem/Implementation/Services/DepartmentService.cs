using DoctorAppointmentBookingSystem.Context;
using DoctorAppointmentBookingSystem.Dto;
using DoctorAppointmentBookingSystem.Entity;
using DoctorAppointmentBookingSystem.Implementation.Interface;
using DoctorAppointmentBookingSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentBookingSystem.Implementation.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;
        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<DepartmentDto>> CreateDepartment(DepartmentDto createdepartment)
        {
            var response = new ResponseModel<DepartmentDto>();

            try
            {
                var department = new Department
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "Admin",
                    IsDeleted = false,
                    Name = createdepartment.Name,
                    Description = createdepartment.Description
                };

                _context.Departments.Add(department);
                await _context.SaveChangesAsync();

                createdepartment.Id = department.Id;
                response.Success = true;
                response.Message = "Department created successfully";
                response.Data = createdepartment;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while creating the department";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel<bool>> DeleteDepartment(Guid id)
        {
            var response = new ResponseModel<bool>();

            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null)
                {
                    response.Success = false;
                    response.Message = "Department not found";
                    response.Data = false;
                    return response;
                }

                department.IsDeleted = true; 
                _context.Departments.Update(department);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Department deleted successfully";
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while deleting the department";
                response.Errors.Add(ex.Message);
                response.Data = false;
            }

            return response;
        }

        public async Task<ResponseModel<DepartmentDto>> EditDepartment(DepartmentDto editdepartment)
        {
            var response = new ResponseModel<DepartmentDto>();

            try
            {
                var department = await _context.Departments.FindAsync(editdepartment.Id);
                if (department == null)
                {
                    response.Success = false;
                    response.Message = "Department not found";
                    return response;
                }

                department.Name = editdepartment.Name;
                department.Description = editdepartment.Description;
                department.ModifiedOn = DateTime.UtcNow;
                department.ModifiedBy = "Admin";

                _context.Departments.Update(department);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Department updated successfully";
                response.Data = editdepartment;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while updating the department";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public Department GetDepartmentById(Guid id)
        {
            return _context.Departments.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
        }

        public async Task<DepartmentDto> GetDepartmentDetail(Guid id)
        {
            return await _context.Departments
                .Where(x => x.Id == id)
                .Select(x => new DepartmentDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy

                }).FirstOrDefaultAsync();
        }

        public async Task<List<DepartmentDto>> GetDepartmentList()
        {
            return await _context.Departments
                .Where(x => x.IsDeleted == false)
                .Select(x => new DepartmentDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToListAsync();
        }
       
        public async Task<IEnumerable<SelectListItem>> GetDepartmentsSelectList()
        {
            var departments = await GetDepartmentList();
            var departmentList = departments.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            });

            return new SelectList(departmentList, "Value", "Text");
        }

        public async Task<List<DoctorDto>> GetDoctorListByDepartment(Guid departmentId)
        {
            return await _context.Doctors
                .Where(d => !d.IsDeleted && d.DepartmentId == departmentId)
                .Select(d => new DoctorDto
                {
                    Id = d.Id,
                    FullName = d.FullName,
                    Email = d.Email,
                    PhoneNumber = d.PhoneNumber,
                    Gender = d.Gender,
                    DepartmentName = d.Department.Name,
                }).ToListAsync();
        }
    }
} 
