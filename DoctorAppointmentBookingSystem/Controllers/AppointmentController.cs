using DoctorAppointmentBookingSystem.Dto;
using DoctorAppointmentBookingSystem.Implementation.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentBookingSystem.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // GET: Appointment
        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentService.GetAppointmentList();
            return View(appointments);
        }

        // GET: Appointment/Details/5
        public async Task<IActionResult> GetAppointmentDetail(Guid id)
        {
            var appointment = await _appointmentService.GetAppointmentDetail(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // GET: Appointment/Create
        public IActionResult BookAppointment() 
        {
            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookAppointment(AppointmentDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _appointmentService.BookAppointment(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            return View(model);
        }

        // GET: Appointment/Edit/5
        public async Task<IActionResult> EditAppointment(Guid id)
        {
            var appointment = await _appointmentService.GetAppointmentDetail(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointment/Edit/5
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

        // GET: Appointment/Cancel/5
        public async Task<IActionResult> CancelAppointment(Guid id)
        {
            var appointment = await _appointmentService.GetAppointmentDetail(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment); 
        }

        // POST: Appointment/Cancel/5
        [HttpPost, ActionName("Cancel")]
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

        // GET: Appointment/Confirm/5
        public async Task<IActionResult> ConfirmAppointment(Guid id) 
        {
            var appointment = await _appointmentService.GetAppointmentDetail(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointment/Confirm/5
        [HttpPost, ActionName("Confirm")]
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

        // GET: Appointment/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var appointment = await _appointmentService.GetAppointmentDetail(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
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
