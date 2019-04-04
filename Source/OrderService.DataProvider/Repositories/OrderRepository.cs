using OrderService.Model.Entities;

namespace OrderService.DataProvider.Repositories
{
    public class OrderRepository: Repository<Order>
    {
        public OrderRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
