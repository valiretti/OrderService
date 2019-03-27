using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DataProvider.Repositories
{
    public interface ICommitProvider
    {
        Task SaveAsync();
    }
}
