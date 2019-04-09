using System;
using System.Collections.Generic;
using System.Text;
using OrderService.Model.Entities;

namespace OrderService.DataProvider.Repositories
{
    public class ExecutorRequestRepository: Repository<ExecutorRequest>
    {
        public ExecutorRequestRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
