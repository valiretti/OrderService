using System;
using System.IO;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OrderService.Logic.Services;
using OrderService.Model;

namespace OrderService.Website.Controllers
{
    [Route("api/executors")]
    [ApiController]
    public class ExecutorController : ControllerBase
    {
        private readonly IExecutorService _executorService;
        private readonly IHostingEnvironment _appEnvironment;

        public ExecutorController(IExecutorService executorService, IHostingEnvironment appEnvironment)
        {
            _executorService = executorService;
            _appEnvironment = appEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExecutorModel request)
        {
            await AddPhotoPaths(request);

            request.UserId = User.GetSubjectId();
            var order = await _executorService.Create(request);

            return Ok(order);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateExecutorModel request)
        {
            await AddPhotoPaths(request);
            request.UserId = User.GetSubjectId();

            await _executorService.Update(request);

            return NoContent();
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _executorService.Delete(id);

            return NoContent();
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _executorService.Get(id);

            return order != null ? (IActionResult)Ok(order) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetExecutors([FromQuery] int page = 0, [FromQuery] int count = 10)
        {
            if (!CheckPageParameters(page, count, out var actionResult)) return actionResult;

            var executors = await _executorService.GetPage(page, count);

            return executors.TotalCount > 0 ? (IActionResult)Ok(executors) : NotFound();
        }

        private async Task AddPhotoPaths(CreateExecutorModel request)
        {
            if (request.Photos != null)
            {
                foreach (var photo in request.Photos)
                {
                    var path = $"{Guid.NewGuid():N}{Path.GetExtension(photo.FileName)}";
                    var imagePath = "/Files/" + path;

                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + imagePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(fileStream);
                    }

                    request.PhotoPaths.Add(imagePath);
                }
            }
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