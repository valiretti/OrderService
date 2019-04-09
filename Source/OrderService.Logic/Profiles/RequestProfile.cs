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

            CreateMap<CustomerRequest, RequestViewModel>();

            CreateMap<ExecutorRequest, RequestViewModel>();



        }
    }
}
