using DoctorAppointmentBookingSystem.Context;
using DoctorAppointmentBookingSystem.Dto;
using DoctorAppointmentBookingSystem.Entity;
using DoctorAppointmentBookingSystem.Implementation.Interface;
using DoctorAppointmentBookingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentBookingSystem.Implementation.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;
        public PatientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<PatientDto>> CreatePatient(PatientDto createPatient)
        {
            var response = new ResponseModel<PatientDto>();

            try
            {
                var patient = new Patient
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "Admin",
                    IsDeleted = false,
                    UserName = createPatient.UserName,
                    Password = createPatient.Password,
                    FullName = createPatient.FullName,
                    Adress = createPatient.Adress,
                    Email = createPatient.Email,
                    PhoneNumber = createPatient.PhoneNumber,
                    Gender = createPatient.Gender,
                    MedicalHistory = createPatient.MedicalHistory,
                    DateOfBirth = createPatient.DateOfBirth,
                    EmergencyContactName = createPatient.EmergencyContactName,
                    EmergencyContactEmail = createPatient.EmergencyContactEmail,
                    EmergencyContactRelationship = createPatient.EmergencyContactRelationship
                };

                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();

                createPatient.Id = patient.Id;
                response.Success = true;
                response.Message = "Patient registered successfully";
                response.Data = createPatient;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while registering the patient";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel<bool>> DeletePatient(Guid Id)
        {
            var response = new ResponseModel<bool>();

            try
            {
                var patient = await _context.Patients.FindAsync(Id);
                if (patient == null)
                {
                    response.Success = false;
                    response.Message = "Patient not found";
                    response.Data = false;
                    return response;
                }

                patient.IsDeleted = true;
                _context.Patients.Update(patient);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Patient deleted successfully";
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while deleting the patient";
                response.Errors.Add(ex.Message);
                response.Data = false;
            }

            return response;
        }

        public async Task<ResponseModel<PatientDto>> EditPatient(PatientDto editPatient)
        {
            var response = new ResponseModel<PatientDto>();

            try
            {
                var patient = await _context.Patients.FindAsync(editPatient.Id);
                if (patient == null)
                {
                    response.Success = false;
                    response.Message = "Patient not found";
                    return response;
                }

                patient.FullName = editPatient.FullName;
                patient.PhoneNumber = editPatient.PhoneNumber;
                patient.DateOfBirth = editPatient.DateOfBirth;
                patient.EmergencyContactName = editPatient.EmergencyContactName;
                patient.EmergencyContactEmail = editPatient.EmergencyContactEmail;
                patient.EmergencyContactRelationship = editPatient.EmergencyContactRelationship;
                patient.Gender = editPatient.Gender;
                patient.Email = editPatient.Email;
                  
                patient.ModifiedOn = DateTime.UtcNow;
                patient.ModifiedBy = "Admin";

                _context.Patients.Update(patient);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "patient updated successfully";
                response.Data = editPatient;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while updating the patient";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public Patient GetPatientById(Guid id)
        {
            return _context.Patients.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
        }

        public async Task<PatientDto> GetPatientDetail(Guid id)
        {
            return await _context.Patients
              .Where(x => x.Id == id)
              .Select(x => new PatientDto
              {
                    UserName = x.UserName,
                    Email = x.Email,
                    FullName = x.FullName,
                    PhoneNumber = x.PhoneNumber,
                    Gender = x.Gender,
                    DateOfBirth = x.DateOfBirth,
                    EmergencyContactName = x.EmergencyContactName,
                    Adress = x.Adress,
                    EmergencyContactEmail = x.EmergencyContactEmail,
                    EmergencyContactRelationship = x.EmergencyContactEmail,
                    MedicalHistory = x.MedicalHistory,

              }).FirstOrDefaultAsync();
        }

        public async Task<List<PatientDto>> GetPatientList()
        {
            return await _context.Patients
                 .Where(x => x.IsDeleted == false)
                 .Select(x => new PatientDto
                 {
                     Id = x.Id,
                     UserName = x.UserName,
                     FullName = x.FullName,
                     Email = x.Email,
                     PhoneNumber = x.PhoneNumber,
                     EmergencyContactName = x.EmergencyContactName,
                     Gender = x.Gender,
                     
                 }).ToListAsync();
        }

        public async Task<Status> PatientLogin(LoginModel login)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(d => d.UserName == login.Username);
            if (patient == null)
            {
                return new Status { Success = false, Message = "Doctor not found" };
            }

            var passwordHasher = new PasswordHasher<Patient>();
            var result = passwordHasher.VerifyHashedPassword(patient, patient.Password, login.Password);
            if (result == PasswordVerificationResult.Success)
            {
                return new Status { Success = true, Message = "Login successful" };
            }
            else
            {
                return new Status { Success = false, Message = "Invalid username or password" };
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetPatientSelectList()
        {
            var patients = await GetPatientList();
            var doctopatientListrList = patients.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.FullName 
            });

            return new SelectList(doctopatientListrList, "Value", "Text");
        }
    }
}