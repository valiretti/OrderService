using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderService.DataProvider.Entities;
using OrderService.Model;

namespace OrderService.Logic.Services
{
    public interface IOrderService
    {
        Task<Order> Create(Order order);

        Task Update(Order order);

        Task<OrderPage> GetPage(int pageNumber, int pageSize);

        Task Delete(int id);
    }
}
