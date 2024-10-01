using Domin.VeiwMode;
using Domin.VeiwModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domin.Interfaces
{
    public interface IEmployeeService
    {
        Task<Response<List<GetAllEmployeeVM>>> GetAllEmployee();
        Task<Response<bool>> AddAsyncEmployee(EmployeeVM employeeVM);
        Task<Response<bool>> UpdateAsyncEmployee(EmployeeVM employeeVM);
        Task<SelectList> GetEmployeeAsSelectList();
        Task<EmployeeVM> GetEmployeeById(int id);
        Task<Response<bool>> DeleteAsync(int id);
    }
}
