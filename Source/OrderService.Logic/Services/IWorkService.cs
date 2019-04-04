using System.Collections.Generic;
using System.Threading.Tasks;
using OrderService.Model;

namespace OrderService.Logic.Services
{
    public interface IWorkService
    {
        Task Create(WorkTypeViewModel workType);

        Task Update(WorkTypeViewModel workType);

        Task<IEnumerable<WorkTypeViewModel>> Get();

        Task Delete(int id);
    }
}
