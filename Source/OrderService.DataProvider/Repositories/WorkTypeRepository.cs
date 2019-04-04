using OrderService.Model.Entities;

namespace OrderService.DataProvider.Repositories
{
    public class WorkTypeRepository : Repository<WorkType>
    {
        public WorkTypeRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
