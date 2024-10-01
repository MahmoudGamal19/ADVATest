using Domin.Entity;
using Domin.Interfaces.Repository;

namespace Infrastructure.Repository
{
    public class EmployeeRepository : GenericRepository<Employee, DBContext>, IEmployeeRepository
    {
        public EmployeeRepository(DBContext context) : base(context)
        {
        }
    }
}
