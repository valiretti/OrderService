using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Model
{
    public class RequestPage
    {
        public IEnumerable<RequestViewModel> Requests { get; set; }

        public int TotalCount { get; set; }
    }
}
