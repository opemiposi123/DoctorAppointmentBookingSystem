using DoctorAppointmentBookingSystem.Context;
using DoctorAppointmentBookingSystem.Dto;
using DoctorAppointmentBookingSystem.Entity;
using DoctorAppointmentBookingSystem.Enum;
using DoctorAppointmentBookingSystem.Implementation.Interface;
using DoctorAppointmentBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentBookingSystem.Implementation.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<AppointmentDto>> BookAppointment(AppointmentDto bookAppointment)
        {
            var response = new ResponseModel<AppointmentDto>();

            try
            {
                var appointment = new Appointment
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    DoctorId = bookAppointment.DoctorId,
                    PatientId = bookAppointment.PatientId,
                    DateAndTime = bookAppointment.DateAndTime,
                    ReasonForVisit = bookAppointment.ReasonForVisit,
                    Status = AppointmentStatus.Pending,
                    AppointmentType = bookAppointment.AppointmentType
                };

                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();

                bookAppointment.Id = appointment.Id;
                response.Success = true;
                response.Message = "Appointment booked successfully";
                response.Data = bookAppointment;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while booking the appointment";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel<AppointmentDto>> CancellAppointment(AppointmentDto confirmAppointment)
        {
            var response = new ResponseModel<AppointmentDto>();

            try
            {
                var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == confirmAppointment.Id && !a.IsDeleted);
                if (appointment == null)
                {
                    response.Success = false;
                    response.Message = "Appointment not found";
                    return response;
                }
                
                appointment.ModifiedOn = DateTime.UtcNow;
                appointment.DoctorId = confirmAppointment.DoctorId;
                appointment.Status = AppointmentStatus.Cancelled;

                _context.Appointments.Update(appointment); 
                await _context.SaveChangesAsync(); 

                response.Success = true;
                response.Message = "Appointment cancelled successfully";
                response.Data = confirmAppointment;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while cancelling the appointment";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel<AppointmentDto>> ConfirmAppointment(AppointmentDto cancellAppointment)
        {
            var response = new ResponseModel<AppointmentDto>();

            try
            {
                var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == cancellAppointment.Id && !a.IsDeleted);
                if (appointment == null)
                {
                    response.Success = false;
                    response.Message = "Appointment not found";
                    return response;
                }

                appointment.ModifiedOn = DateTime.UtcNow;
                appointment.DoctorId = cancellAppointment.DoctorId;
                appointment.Status = AppointmentStatus.Confirmed;

                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Appointment confirmed successfully";
                response.Data = cancellAppointment;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while confirming the appointment";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel<bool>> DeleteAppointment(Guid id)
        {
            var response = new ResponseModel<bool>();

            try
            {
                var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
                if (appointment == null)
                {
                    response.Success = false;
                    response.Message = "Appointment not found";
                    return response;
                }

                appointment.IsDeleted = true;
                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Appointment deleted successfully";
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while deleting the appointment";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel<AppointmentDto>> EditAppointment(AppointmentDto editAppointment)
        {
            var response = new ResponseModel<AppointmentDto>();

            try
            {
                var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == editAppointment.Id && !a.IsDeleted);
                if (appointment == null)
                {
                    response.Success = false;
                    response.Message = "Appointment not found";
                    return response;
                }
                if (editAppointment.Status == AppointmentStatus.Confirmed || editAppointment.Status == AppointmentStatus.Cancelled)
                {
                    response.Success = false;
                    response.Message = "Appointment not found";
                    return response;
                }

                appointment.ModifiedOn = DateTime.UtcNow;
                appointment.DoctorId = editAppointment.DoctorId;
                appointment.DateAndTime = editAppointment.DateAndTime;
                appointment.ReasonForVisit = editAppointment.ReasonForVisit;
                appointment.Status = AppointmentStatus.Pending;
                appointment.AppointmentType = editAppointment.AppointmentType;

                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Appointment updated successfully";
                response.Data = editAppointment;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while updating the appointment";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public Appointment GetAppointmentById(Guid id)
        {
            return _context.Appointments.FirstOrDefault(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<AppointmentDto> GetAppointmentDetail(Guid id)
        {
            var appointment = await _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

            if (appointment == null)
            {
                return null;
            }

            var appointmentDto = new AppointmentDto
            {
                Id = appointment.Id,
                PatientName = appointment.Patient.FullName,
                DoctorName = appointment.Doctor.FullName,
                DateAndTime = appointment.DateAndTime,
                ReasonForVisit = appointment.ReasonForVisit,
                Status = appointment.Status,
                AppointmentType = appointment.AppointmentType,
                CreatedDate = appointment.CreatedDate,
            };

            return appointmentDto;
        }

        public async Task<List<AppointmentDto>> GetAppointmentList()
        {
            var appointments = await _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient)
                .Where(a => !a.IsDeleted)
                .Select(a => new AppointmentDto 
                {
                    Id = a.Id,
                    PatientName = a.Patient.FullName,
                    DoctorName = a.Doctor.FullName,
                    DateAndTime = a.DateAndTime,
                    Status = a.Status,
                    AppointmentType = a.AppointmentType
                }).ToListAsync();

            return appointments;
        }

        public async Task<List<AppointmentDto>> GetDoctorAppointmentList(Guid doctorId)
        {
            var appointments = await _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient)
                .Where(a => !a.IsDeleted && a.DoctorId == doctorId)
                .Select(a => new AppointmentDto
                {
                    Id = a.Id,
                    PatientName = a.Patient.FullName,
                    DoctorName = a.Doctor.FullName,
                    DateAndTime = a.DateAndTime,
                    ReasonForVisit = a.ReasonForVisit,
                    Status = a.Status,
                    AppointmentType = a.AppointmentType
                }).ToListAsync();

            return appointments;
        }

        public async Task<List<AppointmentDto>> GetPatientAppointmentList(Guid patientId)
        {
            var appointments = await _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient)
                .Where(a => !a.IsDeleted && a.PatientId == patientId)
                .Select(a => new AppointmentDto
                {
                    Id = a.Id,
                    PatientName = a.Patient.FullName,
                    DoctorName = a.Doctor.FullName,
                    DateAndTime = a.DateAndTime,
                    ReasonForVisit = a.ReasonForVisit,
                    Status = a.Status,
                    AppointmentType = a.AppointmentType
                }).ToListAsync();

            return appointments;
        } 
    }

}
