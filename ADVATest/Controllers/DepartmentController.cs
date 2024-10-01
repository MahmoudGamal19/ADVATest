using Domin;
using Domin.Entity;
using Domin.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ADVATest.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> GetAll()
        {
            var Result = await _departmentService.GetAllDepartment();
            if (!Result.Status)
            {
                ViewBag.info = Result.ErrorMassage;
                return View("../Department/GetAll", null);
            }
            ViewBag.success = TempData["success"];
            ViewBag.error = TempData["error"];
            return View("../Department/GetAll", Result.Data);
        }
        [HttpGet("Create-Department")]
        public async Task<IActionResult> Create()
        {
              ViewBag.Employee = await _employeeService.GetEmployeeAsSelectList();
            ViewBag.success = TempData["success"];
            return View("../Department/Create");
        }
        [HttpPost("Create-Department")]
        public async Task<IActionResult> Create(DepartmentVM departmentVM)
        {
            ViewBag.Employee = await _employeeService.GetEmployeeAsSelectList();
            if (!ModelState.IsValid)
            {
                ViewBag.error = "Model Not Valid";
                return View("../Department/Create", departmentVM);
            }
            var Result = await _departmentService.AddAsyncDepartment(departmentVM);
            if (!Result.Status)
            {
                ViewBag.error = Result.ErrorMassage;
                return View("../Department/Create", departmentVM);
            }
            TempData["success"] = Result.SuccessMassage;
            return RedirectToAction(nameof(Create));
        }
        [HttpGet("Edit-Department")]
        public async Task<IActionResult> Edit(int departmentId)
        {
            ViewBag.Employee = await _employeeService.GetEmployeeAsSelectList();

            var result = await _departmentService.GetDepartmentVById(departmentId);

            return View("../Department/Edit", result);
        }
        [HttpPost("Edit-Department")]
        public async Task<IActionResult> Edit(DepartmentVM departmentVM)
        {
            ViewBag.Employee = await _employeeService.GetEmployeeAsSelectList();
            if (!ModelState.IsValid)
            {
                ViewBag.error = "Model Not Valid";
                return View("../Department/Edit", departmentVM);
            }
            var Result = await _departmentService.UpdateAsyncDepartment(departmentVM);
            if (!Result.Status)
            {
                ViewBag.error = Result.ErrorMassage;
                return View("../Department/Edit", departmentVM);
            }
            TempData["success"] = Result.SuccessMassage;
            return RedirectToAction(nameof(GetAll));
        }
        public async Task<IActionResult> Delete(int departmentId)
        {
            var res = await _departmentService.DeleteAsync(departmentId);
            if (res.Status)
                TempData["success"] = res.SuccessMassage;
            else TempData["error"] = res.ErrorMassage;
            return RedirectToAction(nameof(GetAll));
        }
    }
}
