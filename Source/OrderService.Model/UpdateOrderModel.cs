using System;
using System.Collections.Generic;
using System.Text;
using OrderService.DataProvider.Entities;

namespace OrderService.Model
{
    public class UpdateOrderModel : CreateOrderModel
    {
       public int Id { get; set; }

       public Status Status { get; set; }
    }
}
