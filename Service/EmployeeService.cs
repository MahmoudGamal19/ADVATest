using AutoMapper;
using Domin.Entity;
using Domin.Interfaces;
using Domin.Interfaces.Repository;
using Domin.VeiwModel;
using Domin.VeiwMode;
using Domin;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Service
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IEmployeeRepository _employeeRepository;
		private readonly IDepartmentRepository _departmentRepository;
		private readonly IMapper _mapper;

		public EmployeeService(
			IEmployeeRepository employeeRepository,
			IMapper mapper,
			IDepartmentRepository departmentRepository)
		{
			_employeeRepository = employeeRepository;
			_mapper = mapper;
			_departmentRepository = departmentRepository;
		}

		public async Task<Response<bool>> AddAsyncEmployee(EmployeeVM employeeVM)
		{
			var result = new Response<bool>();
			var emp = _mapper.Map<Employee>(employeeVM);
			var ValidRequest = ValidateEntity(emp);
			if (!ValidRequest)
			{
				result.Status = false;
				result.ErrorMassage = "Salary Or Name Not Valid";
				result.Data = false;
				return result;
			}
			await _employeeRepository.AddAsync(emp);
			var req = await _employeeRepository.SaveEntitiesAsync();
			if (!req)
			{
				result.Status = false;
				result.ErrorMassage = "We Can't add this entity";
				result.Data = false;
				return result;
			}
			result.Status = true;
			result.SuccessMassage = "this employee added successfully";
			result.Data = true;
			return result;
		}

		public async Task<Response<List<GetAllEmployeeVM>>> GetAllEmployee()
		{
			var Emp = await _employeeRepository.GetAllAsNoTrackingAsync();
			var result = new Response<List<GetAllEmployeeVM>>();
			if (Emp.Count == 0)
			{
				result.Status = false;
				result.ErrorMassage = "We Don't Have Data";
				result.Data = null;
				return result;
			}
			else
			{
				var EmpVm = _mapper.Map<List<GetAllEmployeeVM>>(Emp);
				foreach (var item in EmpVm)
				{
					var Dep = new Department();
					var Maniger = new Employee();

					if (item.DepartmentId != null && item.DepartmentId != 0)
						Dep = await _departmentRepository.GetByIdAsync(item.DepartmentId);
						if (Dep?.ManegerId != null && Dep.ManegerId != item.Id)
						{
							Maniger = await _employeeRepository.GetByIdAsync(Dep.ManegerId);
							item.DepartmentName = Dep?.Name ?? null;
							item.ManegerName = Maniger?.Name ?? null;
							item.ManegerId = Dep?.ManegerId ?? null;
						}
				}
				result.Data = EmpVm;
			}
			return result;
		}

		public async Task<Response<bool>> UpdateAsyncEmployee(EmployeeVM employeeVM)
		{
			var result = new Response<bool>();
			var Emp = _mapper.Map<Employee>(employeeVM);
			var OrgEmp = await _employeeRepository.GetByIdAsync(employeeVM.Id);
            OrgEmp.Name= employeeVM.Name;
            OrgEmp.Salary= employeeVM.Salary;
            OrgEmp.DepartmentId = employeeVM.DepartmentId;

            _employeeRepository.Update(OrgEmp);
			var Res = await _employeeRepository.SaveEntitiesAsync();
			if (!Res)
			{
				result.Status = false;
				result.Data = false;
				result.ErrorMassage = "We Can't add this entity";
				return result;
			}
			result.Status = true;
			result.SuccessMassage = "this employee added successfully";
			result.Data = true;
			return result;
		}

		public async Task<SelectList> GetEmployeeAsSelectList()
		{
			var Emp = await _employeeRepository.GetAllAsNoTrackingAsync();
			var SelectEmp = Emp.Select(c => new
			{
				id = c.Id,
				name = c.Name,

			}).ToList();
			return new SelectList(SelectEmp, "id", "name");
		}

		public async Task<EmployeeVM> GetEmployeeById(int id)
		{
			var emp = await _employeeRepository.GetByIdAsync(id);
			var EmpVm = _mapper.Map<EmployeeVM>(emp);
			return EmpVm;
		}

		public async Task<Response<bool>> DeleteAsync(int id)
        {
            var result = new Response<bool>();
            if (id == null)
			{
                result.Status = false;
                result.ErrorMassage = "We don't have id";
                result.Data = false;
                return result;
            }
			var department = await _departmentRepository.GetAllAsNoTrackingAsync(e => e.ManegerId == id);
			if (department.Count != 0)
			{
                result.Status = false;
                result.ErrorMassage = "Is Maneger For Department num :" + department.FirstOrDefault().Id;
                result.Data = false;
                return result;
            }

			await _employeeRepository.DeleteWhere(e => e.Id == id);
            var req = await _employeeRepository.SaveEntitiesAsync();
            if (!req)
            {
                result.Status = false;
                result.ErrorMassage = "something wrong can't delete this employee";
                result.Data = false;
                return result;
            }
            result.Status = true;
            result.SuccessMassage = "this employee deleted successfully";
            result.Data = true;
			return result;

        }
		#region Healper
		private bool ValidateEntity(Employee emp)
		{
			if (emp.Salary == null)
				return false;
			if (string.IsNullOrEmpty(emp.Name))
				return false;
			return true;
		}
		#endregion
	}
}
