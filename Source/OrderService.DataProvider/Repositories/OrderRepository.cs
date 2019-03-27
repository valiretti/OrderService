using System;
using System.Collections.Generic;
using System.Text;
using OrderService.DataProvider.Entities;

namespace OrderService.DataProvider.Repositories
{
    public class OrderRepository: Repository<Order>
    {
        public OrderRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
