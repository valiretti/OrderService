using System;
using System.Collections.Generic;
using System.Text;
using OrderService.DataProvider.Entities;

namespace OrderService.Model
{
    public class ExecutorViewModel
    {
        public int Id { get; set; }

        public string OrganizationName { get; set; }

        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public string WorkTypeName { get; set; }
    }
}
