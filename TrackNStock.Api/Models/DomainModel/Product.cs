using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackNStock.Api.Models.DomainModel
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BoughtPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
        public bool Sold { get; set; } = false;
    }
}