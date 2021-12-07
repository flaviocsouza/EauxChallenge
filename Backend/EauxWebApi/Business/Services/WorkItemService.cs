using Business.Interfaces.Notificatios;
using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.Models;
using Business.Validations;

namespace Business.Services
{
    public class WorkItemService : BaseService, IDisposable, IWorkItemService
    {
        private readonly IWorkItemRepository _workItemRepository;
        public WorkItemService(
            IWorkItemRepository workItemRepository,
            INotificator notificator
        ) : base(notificator)
        {
            _workItemRepository = workItemRepository;
        }

        public async Task Insert(WorkItem workItem)
        {
            if (!ExecuteValidation(new WorkItemValidation(), workItem)) return;
            await _workItemRepository.Insert(workItem);
        }

        public async Task Update(WorkItem workItem)
        {
            if (!ExecuteValidation(new WorkItemValidation(), workItem)) return;
            await _workItemRepository.Update(workItem);
        }

        public async Task Delete(Guid id)
        {
            await _workItemRepository.Delete(id);
        }

        public void Dispose()
        {
            _workItemRepository.Dispose();
        }
    }
}
