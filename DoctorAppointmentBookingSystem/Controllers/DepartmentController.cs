using DoctorAppointmentBookingSystem.Dto;
using DoctorAppointmentBookingSystem.Entity;
using DoctorAppointmentBookingSystem.Implementation.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentBookingSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetDepartmentList();
            return View(departments);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartmentDetail(Guid id) 
        {
            var department = await _departmentService.GetDepartmentDetail(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctorsDepartment(Guid departmentId) 
        {
            var department = await _departmentService.GetDoctorListByDepartment(departmentId); 
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpGet]
        public IActionResult CreateDepartment() => 
         View();

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromForm] DepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _departmentService.CreateDepartment(departmentDto);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    // Log the error messages
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View(departmentDto);
        }

        public async Task<IActionResult> EditDepartment(Guid id)
        {
            var department = await _departmentService.GetDepartmentDetail(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDepartment(Guid id, DepartmentDto departmentDto)
        {
            if (id != departmentDto.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await _departmentService.EditDepartment(departmentDto);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            return View(departmentDto);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDepartment([FromRoute]Guid id)
        { 
            var result = await _departmentService.DeleteDepartment(id);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(result);
        }
    }
}
