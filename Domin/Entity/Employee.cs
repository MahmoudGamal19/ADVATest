namespace Domin.Entity
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public int ? DepartmentId { get; set; }
        public virtual Department ? Department { get; set; }
    }
}
