﻿using System;
using System.Collections.Generic;
using System.Text;
using OrderService.Model.Entities;

namespace OrderService.Model
{
    public class UpdateOrderModel : CreateOrderModel
    {
       public int Id { get; set; }

       public OrderStatus OrderStatus { get; set; }

       public string[] ExistingPhotos { get; set; }
    }
}
