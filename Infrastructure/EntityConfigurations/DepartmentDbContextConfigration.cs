using Domin.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class DepartmentDbContextConfigration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Department");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired();
            builder.Property(e => e.ManegerId).HasColumnName("Maneger_Id");
            builder.Property(e => e.ManegerName).HasColumnName("Maneger_Name");
        }
    }
}
