using System.Collections.Generic;

namespace OrderService.Model.Entities
{
    public class WorkType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Executor> Executors { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
