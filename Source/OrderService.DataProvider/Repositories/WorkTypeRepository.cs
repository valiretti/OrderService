using System;
using System.Collections.Generic;
using System.Text;
using OrderService.DataProvider.Entities;

namespace OrderService.DataProvider.Repositories
{
    public class WorkTypeRepository : Repository<WorkType>
    {
        public WorkTypeRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
