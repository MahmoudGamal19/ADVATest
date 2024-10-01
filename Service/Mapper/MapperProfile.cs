using AutoMapper;
using Domin;
using Domin.Entity;
using Domin.VeiwMode;

namespace Service.Mapper
{
    public class MapperProfile :Profile
    {
        public MapperProfile() { 
        CreateMap<EmployeeVM, Employee > ()
                  .ForMember(c => c.Department, option => option.Ignore())
                .ReverseMap() 
                ;
        CreateMap< GetAllEmployeeVM, Employee>()
                  .ForMember(c => c.Department, option => option.Ignore()).ReverseMap();
        CreateMap< DepartmentVM, Department>()
                  .ForMember(c => c.Employee, option => option.Ignore()).ReverseMap();

        
        }
    }
}
