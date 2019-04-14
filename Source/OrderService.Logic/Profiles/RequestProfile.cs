using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using OrderService.Model;
using OrderService.Model.Entities;

namespace OrderService.Logic.Profiles
{
    public class RequestProfile: Profile
    {
        public RequestProfile()
        {
            CreateMap<CreateRequestModel, CustomerRequest>();
            CreateMap<CreateRequestModel, ExecutorRequest>();

            CreateMap<CustomerRequest, RequestViewModel>()
                .ForMember(x => x.ExecutorName, opt => opt.MapFrom(x => x.Executor.OrganizationName))
                .ForMember(x => x.OrderName, opt => opt.MapFrom(x => x.Order.Name))
                .ForMember(x => x.CustomerName, opt => opt.MapFrom(x => $"{x.Order.Customer.FirstName} {x.Order.Customer.LastName}")); 
            CreateMap<ExecutorRequest, RequestViewModel>()
                .ForMember(x => x.ExecutorName, opt => opt.MapFrom(x => x.Executor.OrganizationName))
                .ForMember(x => x.OrderName, opt => opt.MapFrom(x => x.Order.Name));
        }
    }
}
