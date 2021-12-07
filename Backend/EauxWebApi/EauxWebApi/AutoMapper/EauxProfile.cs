using AutoMapper;
using Business.Models;
using EauxWebApi.ViewModels;

namespace EauxWebApi.AutoMapper
{
    public class EauxProfile : Profile
    {
        public EauxProfile()
        {
            CreateMap<Project, ProjectViewModel>().ReverseMap();
            CreateMap<WorkItem, WorkItemViewModel>().ReverseMap();
        }
    }
}
