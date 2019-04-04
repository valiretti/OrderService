using System.Threading.Tasks;

namespace OrderService.DataProvider.Repositories
{
    public interface ICommitProvider
    {
        Task SaveAsync();
    }
}
