using System.Collections.Generic;
using System.Threading.Tasks;
using OrderService.Model;
using OrderService.Model.Entities;

namespace OrderService.Logic.Services
{
    public interface IPhotoService
    {
        Task<IEnumerable<int>> Create(CreatePhotoModel item);

        Task<IEnumerable<Photo>> GetByIds(IEnumerable<int> ids);
    }
}
