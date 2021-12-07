using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping
{
    public class ProjectMapping : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(project => project.Id);

            builder.Property(project => project.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(project => project.StartDate)
                .IsRequired();

            builder.Property(project => project.EndDate)
                .IsRequired();

            builder
                .HasMany(project => project.WorkItems)
                .WithOne(task => task.Project);
        }
    }
}
