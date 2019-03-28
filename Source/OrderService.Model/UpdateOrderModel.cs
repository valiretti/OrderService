using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Model
{
    public class UpdateOrderModel : CreateOrderModel
    {
       public int Id { get; set; }
    }
}
