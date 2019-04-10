using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Mvc;
using OrderService.Logic.Services;
using OrderService.Model;

namespace OrderService.Website.Controllers
{
    [Route("api/requests")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly IOrderService _orderService;

        public RequestController(IRequestService requestService, IOrderService orderService)
        {
            _requestService = requestService;
            _orderService = orderService;
        }

        [HttpPost("executor")]
        public async Task<IActionResult> CreateExecutorRequest([FromBody] CreateRequestModel request)
        {
            request.UserId = User.GetSubjectId();
            return Ok(await _requestService.CreateExecutorRequest(request));
        }

        [HttpPost("customer")]
        public async Task<IActionResult> CreateCustomerRequest([FromBody] CreateRequestModel request)
        {
            request.UserId = User.GetSubjectId();
            return Ok(await _requestService.CreateCustomerRequest(request));
        }

        [HttpGet("executor/new")]
        public async Task<IActionResult> GetNewExecutorRequests([FromQuery] int page = 0, [FromQuery] int count = 10)
        {
            if (!CheckPageParameters(page, count, out var actionResult)) return actionResult;
            var requests = await _requestService.GetNewExecutorRequests(page, count, User.GetSubjectId());
            return Ok(requests);
        }

        [HttpGet("customer/new")]
        public async Task<IActionResult> GetNewCustomerRequests([FromQuery] int page = 0, [FromQuery] int count = 10)
        {
            if (!CheckPageParameters(page, count, out var actionResult)) return actionResult;
            var requests = await _requestService.GetNewCustomerRequests(page, count, User.GetSubjectId());
            return Ok(requests);
        }

        [HttpPost("executor/{id:int:min(1)}/accept")]
        public async Task<IActionResult> AcceptExecutorRequest(int id)
        {
            await _requestService.MarkExecutorRequestAccepted(id, User.GetSubjectId());
            return NoContent();
        }

        [HttpPost("customer/{id:int:min(1)}/accept")]
        public async Task<IActionResult> AcceptCustomerRequest(int id)
        {
            await _requestService.MarkCustomerRequestAccepted(id);
            return NoContent();
        }

        [HttpGet("executor/{id:int:min(1)}")]
        public async Task<IActionResult> GetExecutorRequest(int id)
        {
            var request = await _requestService.GetExecutorRequest(id);
            return request != null ? (IActionResult)Ok(request) : NotFound();
        }

        [HttpGet("customer/{id:int:min(1)}")]
        public async Task<IActionResult> GetCustomerRequest(int id)
        {
            var request = await _requestService.GetCustomerRequest(id);
            return request != null ? (IActionResult)Ok(request) : NotFound();
        }

        private bool CheckPageParameters(int page, int count, out IActionResult actionResult)
        {
            if (page < 0)
            {
                actionResult = BadRequest("Invalid page number");
                return false;
            }

            if (count < 0)
            {
                actionResult = BadRequest("Invalid page size");
                return false;
            }

            actionResult = null;
            return true;
        }
    }
}