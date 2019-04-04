using AutoMapper;
using OrderService.Model;
using OrderService.Model.Entities;

namespace OrderService.Logic.Profiles
{
    public class WorkTypeProfile: Profile
    {
        public WorkTypeProfile()
        {
            CreateMap<WorkType, WorkTypeViewModel>().ReverseMap();
        }
    }
}
