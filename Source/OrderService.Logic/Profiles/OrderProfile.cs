using System.Linq;
using AutoMapper;
using OrderService.Model;
using OrderService.Model.Entities;

namespace OrderService.Logic.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderPageViewModel>()
                .ForMember(x => x.PhotoPath, opt => opt.MapFrom(x => x.Photos.Select(p => p.PhotoPath).FirstOrDefault()))
                .ForMember(x => x.WorkTypeName, opt => opt.MapFrom(x => x.WorkType.Name));

            CreateMap<Order, OrderViewModel>()
                .ForMember(x => x.ExecutorName, opt => opt.MapFrom(x => x.Executor.OrganizationName))
                .ForMember(x => x.WorkTypeName, opt => opt.MapFrom(x => x.WorkType.Name))
                .ForMember(x => x.PhotoPaths, opt => opt.MapFrom(x => x.Photos.Select(p => p.PhotoPath)))
                .ForMember(x => x.CustomerName, opt => opt.MapFrom(x => $"{x.Customer.FirstName} {x.Customer.LastName}"));

            CreateMap<CreateOrderModel, Order>()
                .ForMember(x => x.Photos, opt => opt.Ignore());

            CreateMap<UpdateOrderModel, Order>()
                .ForMember(x => x.Photos, opt => opt.Ignore());
        }
    }
}
