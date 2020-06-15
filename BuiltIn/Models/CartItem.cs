using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuiltIn.Models
{
    public class CartItem
    {
        public string Item { get; }
        public int Quantity { get; }

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
