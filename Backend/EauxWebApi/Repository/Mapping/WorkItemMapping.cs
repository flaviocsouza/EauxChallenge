using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mapping
{
    internal class WorkItemMapping : IEntityTypeConfiguration<WorkItem>
    {
        public void Configure(EntityTypeBuilder<WorkItem> builder)
        {
            builder.HasKey(task => task.Id);

            builder.Property(task => task.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(task => task.StartDate)
                .IsRequired();

            builder.Property(task => task.EndDate)
                .IsRequired();

            builder.HasOne(task => task.Project)
                .WithMany(project => project.WorkItems);
        }
    }
}
