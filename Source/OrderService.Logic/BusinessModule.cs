using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using FluentValidation;
using OrderService.DataProvider.Entities;
using OrderService.Logic.Validators;

namespace OrderService.Logic
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<OrderValidator>().As<IValidator<Order>>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
