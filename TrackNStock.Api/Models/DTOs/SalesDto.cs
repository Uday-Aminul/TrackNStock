using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackNStock.Api.Models.DTOs
{
    public class SalesDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public DateTime SalesDate { get; set; }

        public int OrderId { get; set; }

        public OrderDto Order { get; set; }
    }
}