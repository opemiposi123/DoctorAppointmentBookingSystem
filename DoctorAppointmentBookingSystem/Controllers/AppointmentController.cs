using DoctorAppointmentBookingSystem.Dto;
using DoctorAppointmentBookingSystem.Implementation.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentBookingSystem.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;

        public AppointmentController(IAppointmentService appointmentService, IDoctorService doctorService, IPatientService patientService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _patientService = patientService;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentService.GetAppointmentList();
            return View(appointments);
        }

        public async Task<IActionResult> AppointmentDetail(Guid id)
        {
            var appointment = await _appointmentService.GetAppointmentDetail(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        public async Task<IActionResult> BookAppointment()
        {
            ViewBag.Doctors = await _doctorService.GetDoctorSelectList();
            ViewBag.Patients = await _patientService.GetPatientSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookAppointment(AppointmentDto model)
        {
            var result = await _appointmentService.BookAppointment(model);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, result.Message);

            ViewBag.Doctors = await _doctorService.GetDoctorSelectList();
            ViewBag.Patients = await _patientService.GetPatientSelectList();
            return View(model);
        }

        public async Task<IActionResult> EditAppointment(Guid id)
        {
            var appointment = await _appointmentService.GetAppointmentDetail(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAppointment(Guid id, AppointmentDto model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await _appointmentService.EditAppointment(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            return View(model);
        }

        [HttpGet, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelAppointment(Guid id, AppointmentDto model)
        {
            var result = await _appointmentService.CancellAppointment(model);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, result.Message);
            return View(model);
        }

        [HttpGet, ActionName("Confirm")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmAppointment(Guid id, AppointmentDto model)
        {
            var result = await _appointmentService.ConfirmAppointment(model);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, result.Message);
            return View(model);
        }


        [HttpGet, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _appointmentService.DeleteAppointment(id);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, result.Message);
            var appointment = await _appointmentService.GetAppointmentDetail(id);
            return View(appointment);
        }
    }
}
