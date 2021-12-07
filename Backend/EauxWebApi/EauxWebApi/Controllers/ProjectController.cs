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
    [Route("project")]
    public class ProjectController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IProjectService _projectService;
        private readonly IProjectRepository _projectRepository;
        public ProjectController(
            INotificator notificator,
            IMapper mapper,
            IProjectRepository projectRepository,
            IProjectService projectService
        ) : base(notificator)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProjectViewModel>>(await _projectRepository.List());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProjectViewModel>> GetById(Guid id)
        {
            var project = await GetProjectByID(id);

            if (project == null) return NotFound();

            return project;
        }

        [HttpPost]
        public async Task<ActionResult<ProjectViewModel>> Insert(ProjectViewModel project)
        {
            if (!ModelState.IsValid) return CustonResponse(ModelState);
            await _projectService.Insert(_mapper.Map<Project>(project));
            return CustonResponse(project);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProjectViewModel>> Update(Guid id, ProjectViewModel project)
        {
            if(id != project.Id)
            {
                NotificateError("Inconsistent Id");
                return CustonResponse(project);
            }

            if (!ModelState.IsValid) return CustonResponse(ModelState);

            var checkProject = await GetProjectByID(id);
            if (checkProject == null) return NotFound();


            await _projectService.Update(_mapper.Map<Project>(project));

            return CustonResponse(project);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProjectViewModel>> Delete(Guid id)
        {

            var project = await GetProjectByID(id);
            if (project == null) return NotFound();
            
            await _projectService.Delete(id);
            
            return CustonResponse(project);
        }

        private async Task<ProjectViewModel>GetProjectByID(Guid id) => 
            _mapper.Map<ProjectViewModel>(await _projectRepository.GetProjectWorkItems(id));
    }
}
