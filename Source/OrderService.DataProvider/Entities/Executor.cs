using System.Collections.Generic;

namespace OrderService.DataProvider.Entities
{
    public class Executor
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string OrganizationName { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Description { get; set; }
        
        public ICollection<Photo> Photos { get; set; }

        public int WorkTypeId { get; set; }
        public WorkType WorkType { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
