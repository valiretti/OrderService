using System.Linq;
using System.Threading.Tasks;

namespace OrderService.DataProvider.Repositories
{
    public interface IRepository<T> where T: class
    {
        Task Create(T item);

        void Update(T item);

        Task Delete(int id);

        IQueryable<T> GetAll();
    }
}
