namespace OrderService.Model.Entities
{
    public class Photo
    {
        public int Id { get; set; }

        public string PhotoPath { get; set; }
        
        public int? ExecutorId { get; set; }
        public Executor Executor { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }
    }
}
