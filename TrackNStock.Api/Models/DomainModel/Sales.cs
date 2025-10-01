using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackNStock.Api.Models.DomainModel
{
    public class Sales
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public DateTime SalesDate { get; set; }

        //Foreign Key
        public int ProductId { get; set; }
        public int ShopOwnerId { get; set; }
        public int OrderId { get; set; }

        //Navigation Property
        public Product Product { get; set; }
        public ShopOwner ShopOwner { get; set; }
        public Order Order { get; set; }
    }
}