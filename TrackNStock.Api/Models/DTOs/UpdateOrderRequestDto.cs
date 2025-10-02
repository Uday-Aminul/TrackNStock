using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackNStock.Api.Models.DTOs
{
    public class UpdateOrderRequestDto
    {
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }

        public int ShopOwnerId { get; set; }
        public int ProductId { get; set; }
    }
}