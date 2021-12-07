using Business.Interfaces.Repositories;
using Business.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(EauxDbContext context) : base(context) { }

        public async Task<Project> GetProjectWorkItems(Guid id)
        {
            return await _dbContext.Projects.AsNoTracking()
                .Include(projects => projects.WorkItems)
                .FirstAsync(project => project.Id == id);
        }
    }
}
