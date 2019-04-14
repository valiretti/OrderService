
using System;
using OrderService.Model.Entities;

namespace OrderService.Model
{
    public class RequestViewModel
    {
        public int Id { get; set; }
        public int ExecutorId { get; set; }
        public string ExecutorName { get; set; }
        public string CustomerName { get; set; }
        public string OrderName { get; set; }
        public int OrderId { get; set; }
        public string Message { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
