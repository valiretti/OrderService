using OrderService.Model.Entities;

namespace OrderService.DataProvider.Repositories
{
   public class ExecutorRepository: Repository<Executor>
    {
        public ExecutorRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
