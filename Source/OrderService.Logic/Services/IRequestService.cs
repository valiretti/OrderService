using System.Threading.Tasks;
using OrderService.Model;

namespace OrderService.Logic.Services
{
    public interface IRequestService
    {
        Task<RequestViewModel> CreateExecutorRequest(CreateRequestModel request);
        Task<RequestViewModel> CreateCustomerRequest(CreateRequestModel request);

        Task MarkExecutorRequestAccepted(int requestId, string customerId);
        Task MarkCustomerRequestAccepted(int requestId);

        Task RejectExecutorRequest(int requestId, string customerId);
        Task RejectCustomerRequest(int requestId);

        Task<RequestViewModel> GetExecutorRequest(int id);
        Task<RequestViewModel> GetCustomerRequest(int id);

        Task<RequestPage> GetNewExecutorRequests(int pageNumber, int pageSize, string userId);
        Task<RequestPage> GetNewCustomerRequests(int pageNumber, int pageSize, string userId);
    }
}
