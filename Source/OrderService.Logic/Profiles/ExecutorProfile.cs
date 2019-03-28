using System.Linq;
using AutoMapper;
using OrderService.DataProvider.Entities;
using OrderService.Model;

namespace OrderService.Logic.Profiles
{
    public class ExecutorProfile : Profile
    {
        public ExecutorProfile()
        {
            CreateMap<Executor, ExecutorPageViewModel>()
                .ForMember(x => x.Photo, opt => opt.MapFrom(x => x.Photos.FirstOrDefault()))
                .ForMember(x => x.WorkTypeName, opt => opt.MapFrom(x => x.WorkType.Name));

            CreateMap<Executor, ExecutorViewModel>()
                .ForMember(x => x.WorkTypeName, opt => opt.MapFrom(x => x.WorkType.Name));


            CreateMap<CreateExecutorModel, Executor>()
                .ForMember(x => x.Photos, opt => opt.Ignore());

            CreateMap<UpdateExecutorModel, Executor>()
                .ForMember(x => x.Photos, opt => opt.Ignore());
        }
    }
}
