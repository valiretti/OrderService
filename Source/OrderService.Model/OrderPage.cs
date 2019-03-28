﻿using System;
using System.Collections.Generic;

namespace OrderService.Model
{
    public class OrderPage
    {
        public IEnumerable<OrderPageViewModel> Orders { get; set; }

        public int TotalCount { get; set; }
    }
}
