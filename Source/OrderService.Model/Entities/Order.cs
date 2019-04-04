using System;
using System.Collections.Generic;

namespace OrderService.Model.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int? ExecutorId { get; set; }
        public Executor Executor { get; set; }

        public string CustomerUserId { get; set; }
        public User Customer { get; set; }

        public string CustomerPhoneNumber { get; set; }

        public Status Status { get; set; }
        
        public DateTime CreationDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public string Location { get; set; }

        public decimal? Price { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public int? WorkTypeId { get; set; }
        public WorkType WorkType { get; set; }

        public ICollection<ExecutorRequest> ExecutorRequests { get; set; }

        public ICollection<CustomerRequest> CustomerRequests { get; set; }
    }
}
