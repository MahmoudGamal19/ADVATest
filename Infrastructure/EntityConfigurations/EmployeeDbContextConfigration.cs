using Domin.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class EmployeeDbContextConfigration : IEntityTypeConfiguration<Employee>
    {

        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired();
           
            builder.Property(e => e.Salary).HasColumnName("Salary").IsRequired();
            builder.Property(e => e.DepartmentId).HasColumnName("Department_Id");

            //------------Relations---
            builder.HasOne(e => e.Department)
                   .WithMany(e => e.Employee)
                   .HasForeignKey(e => e.DepartmentId);

        }
    }
}
