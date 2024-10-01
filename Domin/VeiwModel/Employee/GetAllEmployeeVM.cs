namespace Domin.VeiwMode
{
    public class GetAllEmployeeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public int? ManegerId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string? ManegerName { get; set; }
    }
}
