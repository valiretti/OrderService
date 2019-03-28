using Autofac;
using FluentValidation;
using OrderService.DataProvider.Entities;
using OrderService.Logic.Validators;
using OrderService.Model;

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
            builder.RegisterType<ExecutorValidator>().As<IValidator<Executor>>().InstancePerLifetimeScope();
            builder.RegisterType<RegistrationViewModelValidator>().As<IValidator<RegisterViewModel>>().InstancePerLifetimeScope();
            builder.RegisterType<WorkTypeValidator>().As<IValidator<WorkType>>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
