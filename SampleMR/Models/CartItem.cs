using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleMR.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }

        public CartItem(string item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public CartItem()
        {

        }
    }
}
