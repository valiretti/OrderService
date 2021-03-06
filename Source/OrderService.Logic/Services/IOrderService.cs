﻿using System.Threading.Tasks;
using OrderService.Model;

namespace OrderService.Logic.Services
{
    public interface IOrderService
    {
        Task<OrderViewModel> Create(CreateOrderModel item);

        Task Update(UpdateOrderModel order);

        Task<OrderPage> GetPage(int pageNumber, int pageSize);

        Task<OrderViewModel> Get(int id);

        Task<OrderPage> GetPageByCustomerId(int pageNumber, int pageSize, string customerId);

        Task Delete(int id);
    }
}
