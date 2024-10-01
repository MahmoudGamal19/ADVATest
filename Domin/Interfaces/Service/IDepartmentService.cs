using Domin.VeiwModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domin.Interfaces
{
    public interface IDepartmentService
    {
        Task<Response<List<DepartmentVM>>> GetAllDepartment();
        Task<Response<bool>> AddAsyncDepartment(DepartmentVM departmentVM);
        Task<Response<bool>> UpdateAsyncDepartment(DepartmentVM departmentVM);
        Task<SelectList> GetDepartmentAsSelectList();
        Task<DepartmentVM> GetDepartmentVById(int id);
        Task<Response<bool>> DeleteAsync(int id);
    }
}
