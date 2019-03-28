using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.DataProvider.Entities
{
    public class ExecutorRequest
    {
        public int Id { get; set; }

        public int ExecutorId { get; set; }
        public Executor Executor { get; set; }

        public string CustomerUserId { get; set; }

        public int OrderId { get; set; }

        public string Message { get; set; }
    }
}
