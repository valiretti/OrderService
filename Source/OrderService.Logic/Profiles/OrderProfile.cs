using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using OrderService.DataProvider.Entities;
using OrderService.Model;

namespace OrderService.Logic.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderPageViewModel>()
                .ForMember(x => x.Photo, opt => opt.MapFrom(x => x.Photos.FirstOrDefault()))
                .ForMember(x => x.WorkTypeName, opt => opt.MapFrom(x => x.WorkType.Name));

            CreateMap<Order, OrderViewModel>()
                .ForMember(x => x.ExecutorName, opt => opt.MapFrom(x => x.Executor.OrganizationName))
                .ForMember(x => x.WorkTypeName, opt => opt.MapFrom(x => x.WorkType.Name))
                .ForMember(x => x.CustomerName, opt => opt.MapFrom(x => $"{x.Customer.FirstName} {x.Customer.LastName}"));

            CreateMap<CreateOrderModel, Order>()
                .ForMember(x => x.Photos, opt => opt.Ignore());

            CreateMap<UpdateOrderModel, Order>()
                .ForMember(x => x.Photos, opt => opt.Ignore());
        }
    }
}
