using Business.Interfaces.Notificatios;
using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.Models;
using Business.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ProjectService : BaseService, IDisposable, IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(
            IProjectRepository projectRepository,
            INotificator notificator
        ): base(notificator)
        {
            _projectRepository = projectRepository;
        }

        public async Task Insert(Project project)
        {
            if (!ExecuteValidation(new ProjectValidation(), project)) return;
            await _projectRepository.Insert(project);
        }

        public async Task Update(Project project)
        {
            if (!ExecuteValidation(new ProjectValidation(), project)) return;
            await _projectRepository.Update(project);
        }

        public async Task Delete(Guid id)
        {
            if (_projectRepository.GetProjectWorkItems(id).Result.WorkItems.Any())
                Notificate("The Project Has Work Itens, delete then before");

            await _projectRepository.Delete(id);
        }

        public void Dispose()
        {
            _projectRepository.Dispose();
        }
    }
}
