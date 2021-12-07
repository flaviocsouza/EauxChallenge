using AutoMapper;
using Business.Interfaces.Notificatios;
using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.Models;
using EauxWebApi.Controllers;
using EauxWebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Eaux.Api.Controllers
{
    [Route("WorkItem")]
    public class WorkItemController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IWorkItemService _workItemService;
        private readonly IWorkItemRepository _workItemRepository;

        public WorkItemController(
            INotificator notificator, 
            IMapper mapper,
            IWorkItemService workItemService,
            IWorkItemRepository workItemRepository
        ) : base(notificator)
        {
            _mapper = mapper;
            _workItemService = workItemService;
            _workItemRepository = workItemRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<WorkItemViewModel>> GetAll() =>
            _mapper.Map<IEnumerable<WorkItemViewModel>>(await _workItemRepository.List());
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<WorkItemViewModel>> GetById(Guid id)
        {
            var workItem = await GetWorkItemById(id);
            if (workItem == null) return NotFound();

            return workItem;
        }

        [HttpPost]
        public async Task<ActionResult<WorkItemViewModel>> Insert(WorkItemViewModel workItem)
        {
            if(!ModelState.IsValid) return CustonResponse(ModelState);
            await _workItemService.Insert(_mapper.Map<WorkItem>(workItem));
            return CustonResponse(workItem);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<WorkItemViewModel>> Update(Guid id, WorkItemViewModel workItem)
        {
            if (id != workItem.Id)
            {
                NotificateError("Inconsistent Id");
                return CustonResponse(workItem);
            }

            if (!ModelState.IsValid) return CustonResponse(ModelState);

            var checkWorkItem = await GetWorkItemById(id);
            if (checkWorkItem == null) return NotFound();

            await _workItemService.Update(_mapper.Map<WorkItem>(workItem));

            return CustonResponse(workItem);
        }


        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<WorkItemViewModel>> Delete(Guid id)
        {
            var workItem = await GetWorkItemById(id);
            if (workItem == null) return NotFound();

            await _workItemService.Delete(id);

            return CustonResponse(workItem);
        }

        private async Task<WorkItemViewModel> GetWorkItemById(Guid id) =>
            _mapper.Map<WorkItemViewModel>(await _workItemRepository.FindById(id));
        
    }
}
