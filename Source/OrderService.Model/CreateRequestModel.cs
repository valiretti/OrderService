using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Model
{
    public class CreateRequestModel
    {
        public int ExecutorId { get; set; }

        public int OrderId { get; set; }

        public string Message { get; set; }

        public string UserId { get; set; }
    }
}
