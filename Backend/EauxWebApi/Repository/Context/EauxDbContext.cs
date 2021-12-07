using Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Context
{
    public class EauxDbContext : DbContext
    {
        public EauxDbContext(DbContextOptions<EauxDbContext> options) : base(options) { }
        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkItem> Tasks { get; set; }
    }
}
