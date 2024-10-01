namespace Domin
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ? ManegerId { get; set; }
        public string ? ManegerName { get; set; }
    }
}
