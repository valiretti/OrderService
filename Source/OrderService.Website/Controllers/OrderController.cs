using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using OrderService.Logic.Services;
using OrderService.Model;

namespace OrderService.Website.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderModel request)
        {
            request.CustomerUserId = User.GetSubjectId();
            var order = await _orderService.Create(request);

            return Ok(order);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOrderModel request)
        {
            request.CustomerUserId = User.GetSubjectId();
            await _orderService.Update(request);

            return NoContent();
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.Delete(id);

            return NoContent();
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _orderService.Get(id);

            return order != null ? (IActionResult)Ok(order) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] int page = 0, [FromQuery] int count = 10)
        {
            if (!CheckPageParameters(page, count, out var actionResult)) return actionResult;

            var orders = await _orderService.GetPage(page, count);

            return Ok(orders);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetOrdersByCustomerId([FromQuery] int page = 0, [FromQuery] int count = 10)
        {
            if (!CheckPageParameters(page, count, out var actionResult)) return actionResult;

            var orders = await _orderService.GetPageByCustomerId(page, count, User.GetSubjectId());

            return Ok(orders);
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