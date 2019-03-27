using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DataProvider.Repositories
{
    public class CommitProvider : ICommitProvider
    {
        private readonly ApplicationContext _context;

        public CommitProvider(ApplicationContext context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
