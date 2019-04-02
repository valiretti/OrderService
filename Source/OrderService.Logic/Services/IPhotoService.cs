using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OrderService.DataProvider.Entities;
using OrderService.Model;

namespace OrderService.Logic.Services
{
    public interface IPhotoService
    {
        Task<IEnumerable<int>> Create(CreatePhotoModel item);

        Task<IEnumerable<Photo>> GetByIds(IEnumerable<int> ids);
    }
}
