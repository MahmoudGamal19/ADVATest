using Domin;
using Domin.Entity;
using Domin.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ADVATest.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> GetAll()
        {
            var Result = await _employeeService.GetAllEmployee();
            if (!Result.Status)
            {
                ViewBag.info = Result.ErrorMassage;
                return View("../Employee/GetAll", null);
            }
            ViewBag.success = TempData["success"];
            ViewBag.error = TempData["error"];
            return View("../Employee/GetAll", Result.Data);
        }
        [HttpGet("Create-Employee")]
        public async Task<IActionResult> Create()
        {
            ViewBag.department = await _departmentService.GetDepartmentAsSelectList();
            ViewBag.success = TempData["success"];
            return View("../Employee/Create");
        }
        [HttpPost("Create-Employee")]
        public async Task<IActionResult> Create(EmployeeVM employeeVM)
        {
            ViewBag.department = await _departmentService.GetDepartmentAsSelectList();
            if (!ModelState.IsValid)
            {
                ViewBag.error = "Model Not Valid";
                return View("../Employee/Create", employeeVM);
            }
            var Result = await _employeeService.AddAsyncEmployee(employeeVM);
            if (!Result.Status)
            {
                ViewBag.error = Result.ErrorMassage;
                return View("../Employee/Create", employeeVM);
            }
            TempData["success"] = Result.SuccessMassage;
            return RedirectToAction(nameof(Create));
        }
        [HttpGet("Edit-Employee")]
        public async Task<IActionResult> Edit(int employeeId)
        {
            ViewBag.department = await _departmentService.GetDepartmentAsSelectList();

            var result = await _employeeService.GetEmployeeById(employeeId);

            ViewBag.success = TempData["success"];
            return View("../Employee/Edit", result);
        }
        [HttpPost("Edit-Employee")]
        public async Task<IActionResult> Edit(EmployeeVM employeeVM)
        {
            ViewBag.department = await _departmentService.GetDepartmentAsSelectList();
            if (!ModelState.IsValid)
            {
                ViewBag.error = "Model Not Valid";
                return View("../Employee/Edit", employeeVM);
            }
            var Result = await _employeeService.UpdateAsyncEmployee(employeeVM);
            if (!Result.Status)
            {
                ViewBag.error = Result.ErrorMassage;
                return View("../Employee/Eidit", employeeVM);
            }
            TempData["success"] = Result.SuccessMassage;
            return RedirectToAction(nameof(GetAll));
        }
        public async Task< IActionResult> Delete(int employeeId)
        {
            var res = await _employeeService.DeleteAsync(employeeId);
            if (res.Status)
                TempData["success"] = res.SuccessMassage;
            else TempData["error"] = res.ErrorMassage;
            return RedirectToAction(nameof(GetAll));
        }
    }
}
