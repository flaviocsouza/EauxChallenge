using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Services
{
    public interface IWorkItemService
    {
        public Task Insert(WorkItem project);
        public Task Update(WorkItem project);
        public Task Delete(Guid id);
    }
}
