
namespace OrderService.Model
{
    public class RequestViewModel
    {
        public int Id { get; set; }
        public int ExecutorId { get; set; }
        public string ExecutorName { get; set; }
        public string CustomerName { get; set; }
        public int OrderId { get; set; }
        public string Message { get; set; }
    }
}
