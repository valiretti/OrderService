using System;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.DataProvider.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext _context;

        public Repository(ApplicationContext context)
        {
            _context = context;
        }
        
        public async Task Create(T item)
        {
            await _context.Set<T>().AddAsync(item);
        }

        public void Update(T item)
        {
            _context.Set<T>().Update(item);
        }

        public async Task Delete(int id)
        {
            var item = await _context.Set<T>().FindAsync(id);
            if (item != null)
            {
                _context.Set<T>().Remove(item);
            }
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }
    }
}
