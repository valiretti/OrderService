using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace OrderService.Model
{
    public class CreateExecutorModel
    {
        public string UserId { get; set; }
     
        public string OrganizationName { get; set; }

        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        public int? WorkTypeId { get; set; }

        public int[] Photos { get; set; }
    }
}
