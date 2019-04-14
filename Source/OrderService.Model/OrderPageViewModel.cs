using System;
using OrderService.Model.Entities;

namespace OrderService.Model
{
    public class OrderPageViewModel
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public string Location { get; set; }

        public decimal? Price { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PhotoPath { get; set; }

        public string WorkTypeName { get; set; }

        public OrderStatus Status { get; set; }
    }
}
