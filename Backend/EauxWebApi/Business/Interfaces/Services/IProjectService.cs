using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Services
{
    public interface IProjectService
    {
        public Task Insert(Project project);
        public Task Update(Project project);
        public Task Delete(Guid id);
    }
}
