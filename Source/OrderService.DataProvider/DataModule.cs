using Autofac;
using OrderService.DataProvider.Repositories;

namespace OrderService.DataProvider
{
    public class DataModule : Module
    {
        private readonly string _connectionString;

        public DataModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<CommitProvider>()
                .As<ICommitProvider>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
