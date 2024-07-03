using DoctorAppointmentBookingSystem.Context;
using DoctorAppointmentBookingSystem.Dto;
using DoctorAppointmentBookingSystem.Entity;
using DoctorAppointmentBookingSystem.Implementation.Interface;
using DoctorAppointmentBookingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentBookingSystem.Implementation.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationDbContext _context;

        public DoctorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<DoctorDto>> CreateDoctor(DoctorDto createDoctor)
        {
            var response = new ResponseModel<DoctorDto>();

            try
            {
                var doctor = new Doctor
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "Admin",
                    IsDeleted = false,
                    UserName = createDoctor.UserName,
                    Password = createDoctor.Password,
                    FullName = createDoctor.FullName,
                    Email = createDoctor.Email,
                    PhoneNumber = createDoctor.PhoneNumber,
                    Address = createDoctor.Address,
                    DateOBirth = createDoctor.DateOfBirth,
                    Gender = createDoctor.Gender,
                    DepartmentId = createDoctor.DepartmentId,
                };

                _context.Doctors.Add(doctor);

                if (createDoctor.DoctorSchedules != null && createDoctor.DoctorSchedules.Count > 0)
                {
                    foreach (var schedule in createDoctor.DoctorSchedules)
                    {
                        var doctorSchedule = new DoctorSchedule
                        {
                            Id = Guid.NewGuid(),
                            DoctorId = doctor.Id,
                            DayOfTheWeek = schedule.DayOfTheWeek,
                            StartTime = schedule.StartTime,
                            EndTime = schedule.EndTime,
                            IsDeleted = false,
                            CreatedDate = DateTime.UtcNow,
                            CreatedBy = "Admin",
                        };
                        if (doctorSchedule.StartTime <= doctorSchedule.EndTime)
                        {
                            response.Success = false;
                            response.Message = "Start time and end time cannot be equal";
                        }

                        _context.DoctorSchedules.Add(doctorSchedule);
                    }
                }

                await _context.SaveChangesAsync();

                createDoctor.Id = doctor.Id;
                response.Success = true;
                response.Message = "Doctor and schedule created successfully";
                response.Data = createDoctor;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while creating the doctor and schedule";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel<DoctorDto>> EditDoctor(DoctorDto editDoctor)
        {
            var response = new ResponseModel<DoctorDto>();

            try
            {
                var doctor = await _context.Doctors.Include(d => d.DoctorSchedule).FirstOrDefaultAsync(d => d.Id == editDoctor.Id);
                if (doctor == null)
                {
                    response.Success = false;
                    response.Message = "Doctor not found";
                    return response;
                }

                doctor.FullName = editDoctor.FullName;
                doctor.Email = editDoctor.Email;
                doctor.PhoneNumber = editDoctor.PhoneNumber;
                doctor.Address = editDoctor.Address;
                doctor.DateOBirth = editDoctor.DateOfBirth;
                doctor.Gender = editDoctor.Gender;
                doctor.DepartmentId = editDoctor.DepartmentId;
                doctor.ModifiedOn = DateTime.UtcNow;
                doctor.ModifiedBy = "Admin";

                // Update the doctor schedules if provided
                if (editDoctor.DoctorSchedules != null && editDoctor.DoctorSchedules.Count > 0)
                {
                    // Remove existing schedules
                    foreach (var schedule in doctor.DoctorSchedule)
                    {
                        _context.DoctorSchedules.Remove(schedule);
                    }

                    // Add new schedules
                    foreach (var scheduleDto in editDoctor.DoctorSchedules)
                    {
                        var doctorSchedule = new DoctorSchedule
                        {
                            Id = Guid.NewGuid(),
                            DoctorId = doctor.Id,
                            DayOfTheWeek = scheduleDto.DayOfTheWeek,
                            StartTime = scheduleDto.StartTime,
                            EndTime = scheduleDto.EndTime,
                            IsDeleted = false,
                            ModifiedOn = DateTime.UtcNow,
                            ModifiedBy = "Admin"
                        };

                        _context.DoctorSchedules.Add(doctorSchedule);
                    }
                }

                _context.Doctors.Update(doctor);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Doctor updated successfully";
                response.Data = editDoctor;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while updating the doctor";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel<DoctorDto>> DeleteDoctor(Guid id)
        {
            var response = new ResponseModel<DoctorDto>();

            try
            {
                var doctor = await _context.Doctors.Include(d => d.DoctorSchedule).FirstOrDefaultAsync(d => d.Id == id);
                if (doctor == null)
                {
                    response.Success = false;
                    response.Message = "Doctor not found";
                    return response;
                }

                // Mark the doctor as deleted
                doctor.IsDeleted = true;

                // Mark the associated schedules as deleted
                foreach (var schedule in doctor.DoctorSchedule)
                {
                    schedule.IsDeleted = true;
                    _context.DoctorSchedules.Update(schedule);
                }

                _context.Doctors.Update(doctor);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Doctor and associated schedules deleted successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while deleting the doctor and schedules";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<List<DoctorDto>> GetDoctorList()
        {
            return await _context.Doctors
                .Where(d => !d.IsDeleted)
                .Select(d => new DoctorDto
                {
                    Id = d.Id,
                    UserName = d.UserName,
                    FullName = d.FullName,
                    Email = d.Email,
                    PhoneNumber = d.PhoneNumber,
                    Gender = d.Gender,
                    DepartmentName = d.Department.Name,
                }).ToListAsync();
        }

     

        public async Task<DoctorDto> GetDoctorDetail(Guid id)
        {
            var doctor = await _context.Doctors
                .Where(d => !d.IsDeleted && d.Id == id)
                .Select(d => new DoctorDto
                {
                    Id = d.Id,
                    FullName = d.FullName,
                    Email = d.Email,
                    PhoneNumber = d.PhoneNumber,
                    Address = d.Address,
                    DateOfBirth = d.DateOBirth,
                    Gender = d.Gender,
                    DepartmentName = d.Department.Name,
                    DoctorSchedules = d.DoctorSchedule.ToList(),
                    Appointments = d.Appointments.ToList()
                }).FirstOrDefaultAsync();

            return doctor;
        }

        public Doctor GetDoctorById(Guid id)
        {
            return _context.Doctors.FirstOrDefault(d => d.Id == id && !d.IsDeleted);
        }

        public async Task<Status> DoctorLogin(LoginModel login)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserName == login.Username);
            if (doctor == null)
            {
                return new Status { Success = false, Message = "Doctor not found" };
            }

            var passwordHasher = new PasswordHasher<Doctor>();
            var result = passwordHasher.VerifyHashedPassword(doctor, doctor.Password, login.Password);
            if (result == PasswordVerificationResult.Success)
            {
                return new Status { Success = true, Message = "Login successful" };
            }
            else
            {
                return new Status { Success = false, Message = "Invalid username or password" }; 
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetDoctorSelectList()
        {
            var doctors = await GetDoctorList();
            var doctorList = doctors.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.FullName
            });

            return new SelectList(doctorList, "Value", "Text");
        }
    }

}
