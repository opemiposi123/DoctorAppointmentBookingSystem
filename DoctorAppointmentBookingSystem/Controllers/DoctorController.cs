using DoctorAppointmentBookingSystem.Dto;
using DoctorAppointmentBookingSystem.Implementation.Interface;
using DoctorAppointmentBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoctorAppointmentBookingSystem.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IDepartmentService _departmentService;

        public DoctorController(IDoctorService doctorService, IDepartmentService departmentService)
        {
            _doctorService = doctorService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorService.GetDoctorList();
            return View(doctors);
        }

        public async Task<IActionResult> DoctorDetail(Guid id)
        {
            var doctor = await _doctorService.GetDoctorDetail(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        public async Task<IActionResult> CreateDoctor()
        {
            ViewBag.Departments = await _departmentService.GetDepartmentsSelectList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDoctor(DoctorDto model)
        {

            var result = await _doctorService.CreateDoctor(model);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, result.Message);

            ViewBag.Departments = await _departmentService.GetDepartmentsSelectList();
            return View(model);
        }

        public async Task<IActionResult> EditDoctor(Guid id)
        {
            var doctor = await _doctorService.GetDoctorDetail(id);
            if (doctor == null)
            {
                return NotFound();
            }
            ViewBag.Departments = await _departmentService.GetDepartmentsSelectList();
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDoctor(Guid id, DoctorDto model)
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
            ViewBag.Departments = await _departmentService.GetDepartmentsSelectList();
            return View(model);
        }


        [HttpGet, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDoctor(Guid id)
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


    }
}
