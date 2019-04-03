using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OrderService.DataProvider.Entities;
using OrderService.Model;

namespace OrderService.Logic.Services
{
    public interface IExecutorService
    {
        Task<ExecutorViewModel> Create(CreateExecutorModel item);

        Task Update(UpdateExecutorModel item);

        Task<ExecutorPage> GetPage(int pageNumber, int pageSize);

        Task<ExecutorViewModel> Get(int id);

        Task GetExecutorRequests(int executorId);

        Task Delete(int id);
    }
}
