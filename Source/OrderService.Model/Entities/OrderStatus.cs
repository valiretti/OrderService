
namespace OrderService.Model.Entities
{
   public enum OrderStatus : byte
    {
        Active = 0,
        Confirmed = 1,
        Completed = 2,
        Unfulfilled = 3
    }
}
