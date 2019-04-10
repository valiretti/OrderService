using System.Threading.Tasks;
using OrderService.Model;

namespace OrderService.Logic.Services
{
    public interface IRequestService
    {
        Task<RequestViewModel> CreateExecutorRequest(CreateRequestModel request);
        Task<RequestViewModel> CreateCustomerRequest(CreateRequestModel request);

        Task Update(UpdateRequestModel request);

        Task<RequestPage> GetNewExecutorRequests(int pageNumber, int pageSize, string userId);
        Task<RequestPage> GetCustomerRequests(int pageNumber, int pageSize, string userId);
    }
}
