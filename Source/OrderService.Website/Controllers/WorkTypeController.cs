using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderService.Logic.Services;
using OrderService.Model;

namespace OrderService.Website.Controllers
{
    [Route("api/works")]
    [ApiController]
    public class WorkTypeController : ControllerBase
    {
        private readonly IWorkService _service;

        public WorkTypeController(IWorkService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WorkTypeViewModel request)
        {
            await _service.Create(request);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] WorkTypeViewModel request)
        {
            await _service.Update(request);

            return NoContent();
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _service.Get();

            return orders?.Count() > 0 ? (IActionResult)Ok(orders) : NotFound();
        }
    }
}