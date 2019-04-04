using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OrderService.Model;

namespace OrderService.Logic.Services
{
    public interface IUserService
    {
        Task<string> Register(RegisterViewModel model);

        Task SetExecutorRole(string id);
    }
}
