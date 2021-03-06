﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
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

        public ExecutorController(IExecutorService executorService)
        {
            _executorService = executorService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExecutorModel request)
        {
            request.UserId = User.GetSubjectId();
            var executor = await _executorService.Create(request);

            return Ok(executor);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateExecutorModel request)
        {
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
            var executor = await _executorService.Get(id);

            return executor != null ? (IActionResult)Ok(executor) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetExecutors([FromQuery] int page = 0, [FromQuery] int count = 10)
        {
            if (!CheckPageParameters(page, count, out var actionResult)) return actionResult;

            var executors = await _executorService.GetPage(page, count);

            return Ok(executors);
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