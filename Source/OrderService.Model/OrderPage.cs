using System;
using System.Collections.Generic;

namespace OrderService.Model
{
    public class OrderPage
    {
        public IEnumerable<OrderViewModel> Orders { get; set; }

        public int TotalCount { get; set; }
    }
}
