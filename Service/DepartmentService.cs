using AutoMapper;
using Domin;
using Domin.Entity;
using Domin.Interfaces;
using Domin.Interfaces.Repository;
using Domin.VeiwModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task<Response<bool>> AddAsyncDepartment(DepartmentVM departmentVM)
        {
            var result = new Response<bool>();
            var Dep = _mapper.Map<Department>(departmentVM);
            
            await _departmentRepository.AddAsync(Dep);
            var req = await _departmentRepository.SaveEntitiesAsync();
            if (!req)
            {
                result.Status = false;
                result.ErrorMassage = "We Can't add this entity";
                result.Data = false;
                return result;
            }
            result.Status = true;
            result.SuccessMassage = "this Department added successfully";
            result.Data = true;
            return result;
        }

        public async Task<Response<List<DepartmentVM>>> GetAllDepartment()
        {
            var dep = await _departmentRepository.GetAllAsNoTrackingAsync();
            var result = new Response<List<DepartmentVM>>();
            if (dep.Count == 0)
            {
                result.Status = false;
                result.ErrorMassage = "We Don't Have Data";
                result.Data = null;
                return result;
            }
            else
            {
                var EmpVm = _mapper.Map<List<DepartmentVM>>(dep);
                result.Data = EmpVm;
            }
            return result;
        }

        public async Task<Response<bool>> UpdateAsyncDepartment(DepartmentVM departmentVM)
        {
            var result = new Response<bool>();
            var dep = _mapper.Map<Department>(departmentVM);
            var orgdep = _departmentRepository.GetById(dep.Id);
            orgdep.Name = departmentVM.Name;
            orgdep.ManegerName = departmentVM.ManegerName;
            orgdep.ManegerId =departmentVM.ManegerId;
            _departmentRepository.Update(orgdep);
            var Res = await _departmentRepository.SaveEntitiesAsync();
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
        public async Task<DepartmentVM> GetDepartmentVById(int id)
        {
            var Dep = await _departmentRepository.GetByIdAsync(id);
            var DepVm = _mapper.Map<DepartmentVM>(Dep);
            return DepVm;
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
            var employee = await _employeeRepository.GetAllAsNoTrackingAsync(e => e.DepartmentId  == id);
            if (employee.Count != 0)
            {
                result.Status = false;
                result.ErrorMassage = "Is Maneger For Department num :" + employee?.FirstOrDefault()?.Id;
                result.Data = false;
                return result;
            }

            await _departmentRepository.DeleteWhere(e => e.Id == id);
            var req = await _employeeRepository.SaveEntitiesAsync();
            if (!req)
            {
                result.Status = false;
                result.ErrorMassage = "something wrong can't delete this department";
                result.Data = false;
                return result;
            }
            result.Status = true;
            result.SuccessMassage = "this department deleted successfully";
            result.Data = true;
            return result;

        }
        public async Task<SelectList> GetDepartmentAsSelectList()
        {
            var Dep = await _departmentRepository.GetAllAsNoTrackingAsync();
            var SelectDep = Dep.Select(c => new
            {
                id = c.Id,
                name = c.Id +" _ "+  c.Name,

            }).ToList();
            return new SelectList(SelectDep, "id", "name");
        }
    }
}
