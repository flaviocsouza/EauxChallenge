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
    public class WorkItemRepository : Repository<WorkItem>, IWorkItemRepository 
    {
        public WorkItemRepository(EauxDbContext context) : base(context) { }
    }
}
