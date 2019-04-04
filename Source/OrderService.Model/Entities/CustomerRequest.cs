namespace OrderService.Model.Entities
{
    public class CustomerRequest
    {
        public int Id { get; set; }

        public int ExecutorId { get; set; }
        public  Executor Executor { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public string Message { get; set; }
    }
}
