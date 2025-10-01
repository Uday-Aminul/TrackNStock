using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackNStock.Api.Models.DomainModel
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }

        //Foreign key
        public int ShopOwnerId { get; set; }
        public int ProductId { get; set; }
        //Navigation Property
        public ShopOwner ShopOwner { get; set; }
        public Product Product { get; set; }
    }
}