namespace Domin.Entity
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ? ManegerId { get; set; }
        public string ? ManegerName { get; set; }
        public virtual Employee Manager { get; set; }
        public virtual ICollection<Employee> Employee { get;set; }
    }
}
