using DoctorAppointmentBookingSystem.Dto;
using DoctorAppointmentBookingSystem.Implementation.Interface;
using DoctorAppointmentBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentBookingSystem.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // GET: Patient
        public async Task<IActionResult> Index()
        {
            var patients = await _patientService.GetPatientList();
            return View(patients);
        }

        // GET: Patient/Details/5
        public async Task<IActionResult> PatientDetail(Guid id)
        {
            var patient = await _patientService.GetPatientDetail(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // GET: Patient/Create
        public IActionResult RegisterPatient()
        { 
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterPatient(PatientDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _patientService.CreatePatient(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            return View(model);
        }

        // GET: Patient/Edit/5
        public async Task<IActionResult> EditPatient(Guid id)
        {
            var patient = await _patientService.GetPatientDetail(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPatient(Guid id, PatientDto model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await _patientService.EditPatient(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            return View(model);
        }

        [HttpGet, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            var result = await _patientService.DeletePatient(id);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, result.Message);
            var patient = await _patientService.GetPatientDetail(id);
            return View(patient);
        }

        // GET: Patient/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Patient/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _patientService.PatientLogin(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            return View(model);
        }
    }
}
