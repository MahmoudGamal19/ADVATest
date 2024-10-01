using Domin.Entity;
using Domin.Interfaces.Repository;

namespace Infrastructure.Repository
{
    public class DepartmentRepository : GenericRepository<Department, DBContext>, IDepartmentRepository
    {
        public DepartmentRepository(DBContext context) : base(context)
        {
        }
    }
}
