using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackNStock.Api.Models.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }

        public ShopOwnerDto ShopOwner { get; set; }
        public ProductDtoForPublic Product { get; set; }
    }
}