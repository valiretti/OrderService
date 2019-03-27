using System;
using System.Collections.Generic;
using System.Text;
using OrderService.DataProvider.Entities;

namespace OrderService.DataProvider.Repositories
{
   public class ExecutorRepository: Repository<Executor>
    {
        public ExecutorRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
