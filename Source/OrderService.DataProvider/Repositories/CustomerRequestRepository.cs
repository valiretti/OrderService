using System;
using System.Collections.Generic;
using System.Text;
using OrderService.Model.Entities;

namespace OrderService.DataProvider.Repositories
{
    public class CustomerRequestRepository: Repository<CustomerRequest>
    {
        public CustomerRequestRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
