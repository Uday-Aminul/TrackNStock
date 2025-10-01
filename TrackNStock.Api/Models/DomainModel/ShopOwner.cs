using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackNStock.Api.Models.DomainModel
{
    public class ShopOwner
    {
        public int Id { get; set; }
        public string ShopName { get; set; }
        public string OwnerName { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
    }
}