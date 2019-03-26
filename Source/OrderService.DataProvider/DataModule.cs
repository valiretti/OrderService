using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

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
        }
    }
}
