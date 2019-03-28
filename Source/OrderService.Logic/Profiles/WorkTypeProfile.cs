using AutoMapper;
using OrderService.DataProvider.Entities;
using OrderService.Model;

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
