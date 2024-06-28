using DoctorAppointmentBookingSystem.Dto;
using DoctorAppointmentBookingSystem.Implementation.Interface;
using DoctorAppointmentBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentBookingSystem.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        // GET: Doctor
        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorService.GetDoctorList();
            return View(doctors);
        }

        // GET: Doctor/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var doctor = await _doctorService.GetDoctorDetail(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // GET: Doctor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _doctorService.CreateDoctor(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            return View(model);
        }

        // GET: Doctor/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var doctor = await _doctorService.GetDoctorDetail(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DoctorDto model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await _doctorService.EditDoctor(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            return View(model);
        }

        // GET: Doctor/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var doctor = await _doctorService.GetDoctorDetail(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _doctorService.DeleteDoctor(id);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, result.Message);
            var doctor = await _doctorService.GetDoctorDetail(id);
            return View(doctor);
        }

        // GET: Doctor/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Doctor/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _doctorService.DoctorLogin(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            return View(model);
        }

        // GET: Doctor/ListByDepartment/5
        public async Task<IActionResult> GetDoctorListByDepartment(Guid departmentId)
        {
            var doctors = await _doctorService.GetDoctorListByDepartment(departmentId);
            return View(doctors);
        }
    }
}
