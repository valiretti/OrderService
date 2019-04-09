using System.Threading.Tasks;
using OrderService.Model;

namespace OrderService.Logic.Services
{
    public interface IRequestService
    {
        Task<RequestViewModel> CreateExecutorRequest(CreateRequestModel request);
        Task<RequestViewModel> CreateCustomerRequest(CreateRequestModel request);

        Task Update(UpdateRequestModel request);

        Task<RequestPage> GetExecutorRequests(int pageNumber, int pageSize, int executorId);
        Task<RequestPage> GetCustomerRequests(int pageNumber, int pageSize, string customerId);
    }
}
