using System;
using System.Collections.Generic;
using System.Text;
using OrderService.DataProvider.Entities;

namespace OrderService.DataProvider.Repositories
{
    public class PhotoRepository : Repository<Photo>
    {
        public PhotoRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
