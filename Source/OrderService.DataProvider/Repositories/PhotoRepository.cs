using OrderService.Model.Entities;

namespace OrderService.DataProvider.Repositories
{
    public class PhotoRepository : Repository<Photo>
    {
        public PhotoRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
