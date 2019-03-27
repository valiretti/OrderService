using System;
using System.Collections.Generic;
using System.Text;
using OrderService.DataProvider.Entities;

namespace OrderService.Model
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }

        public DateTime FinishDate { get; set; }

        public string Location { get; set; }

        public decimal? Price { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Photo Photo { get; set; }

        public string WorkTypeName { get; set; }
    }
}
