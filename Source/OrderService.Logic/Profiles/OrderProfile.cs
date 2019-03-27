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
            CreateMap<Order, OrderViewModel>()
                .ForMember(x => x.Photo, opt => opt.MapFrom(x => x.Photos.FirstOrDefault()))
                .ForMember(x => x.WorkTypeName, opt => opt.MapFrom(x => x.WorkType.Name))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
