namespace Domin
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public int ? DepartmentId { get; set; }
    }
}
