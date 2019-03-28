using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace OrderService.Model
{
    public class CreateOrderModel
    {
        public int? WorkTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFileCollection Photos { get; set; }

        public string Location { get; set; }

        public DateTime? FinishDate { get; set; }

        public decimal? Price { get; set; }
        
        public string CustomerPhoneNumber { get; set; }

        public string CustomerUserId { get; set; }
    }
}
