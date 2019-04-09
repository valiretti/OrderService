using Autofac;
using FluentValidation;
using OrderService.Logic.Validators;
using OrderService.Model;
using OrderService.Model.Entities;

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

            builder.RegisterType<OrderValidator>().As<IValidator<CreateOrderModel>>().InstancePerLifetimeScope();
            builder.RegisterType<ExecutorValidator>().As<IValidator<CreateExecutorModel>>().InstancePerLifetimeScope();
            builder.RegisterType<RegistrationViewModelValidator>().As<IValidator<RegisterViewModel>>().InstancePerLifetimeScope();
            builder.RegisterType<WorkTypeValidator>().As<IValidator<WorkTypeViewModel>>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
