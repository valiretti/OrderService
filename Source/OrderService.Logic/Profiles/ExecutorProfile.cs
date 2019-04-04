using System.Linq;
using AutoMapper;
using OrderService.Model;
using OrderService.Model.Entities;

namespace OrderService.Logic.Profiles
{
    public class ExecutorProfile : Profile
    {
        public ExecutorProfile()
        {
            CreateMap<Executor, ExecutorPageViewModel>()
                .ForMember(x => x.PhotoPath, opt => opt.MapFrom(x => x.Photos.Select(p => p.PhotoPath).FirstOrDefault()))
                .ForMember(x => x.WorkTypeName, opt => opt.MapFrom(x => x.WorkType.Name));

            CreateMap<Executor, ExecutorViewModel>()
                .ForMember(x => x.WorkTypeName, opt => opt.MapFrom(x => x.WorkType.Name))
                .ForMember(x => x.PhotoPaths, opt => opt.MapFrom(x => x.Photos.Select(p => p.PhotoPath)));

            CreateMap<CreateExecutorModel, Executor>()
                .ForMember(x => x.Photos, opt => opt.Ignore());

            CreateMap<UpdateExecutorModel, Executor>()
                .ForMember(x => x.Photos, opt => opt.Ignore());
        }
    }
}
